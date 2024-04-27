import os
import re

input_file_name = "all.txt"

def analyse(txt):

    def split_to_rows(txt):
        regex = r"<tr>(.+?)<\/tr>"
        matches = re.finditer(regex, txt, re.MULTILINE)

        ret = []
        for match in matches:
            grp = match.group(1)
            ret.append(grp)

        return ret

    def remove_tags(txt):
        CLEANR = re.compile('<.*?>') 
        if txt is None:
            ret = None
        else:
            ret = re.sub(CLEANR, '', txt)
        return ret

    def split_to_blocks(rows):
        regex3 = r"<td.*?>(.+?)<\/td>.+(?:<td.*?>(.+?)<\/td>).+<td.*?>(.+?)<\/td>"
        regex2 = r"<td.*?>(.+?)</td>.+(?:<td.*?>(.+?)</td>)?.+<td.*?>(.+?)</td>"
        ret = []

        for row in rows:
            blck = {}
            matches = re.finditer(regex3, row, re.MULTILINE)
            for match in matches:
                if "deprecated" in match.group(1):
                    blck["deprecated"] = True
                blck["id"] = remove_tags(match.group(1)).strip()
                blck["params"] = remove_tags(match.group(2)).strip()
                blck["description"] = remove_tags(match.group(3)).strip()
                break
            if len(blck) == 0:
                matches = re.finditer(regex2, row, re.MULTILINE)
                for match in matches:
                    if "deprecated" in match.group(1):
                        blck["deprecated"] = True
                    blck["id"] = remove_tags(match.group(1)).strip()
                    blck["params"] = None
                    blck["description"] = remove_tags(match.group(3)).strip()
                    break
            if len(blck) > 0:
                if "deprecated" not in blck.keys():
                    blck["deprecated"] = False
                ret.append(blck)

        return ret

    def expand_multidef_blocks(blocks):
        ret = []

        for block in blocks:
            pts = block["id"].split()
            if len(pts) > 1:
                for pt in pts:
                    new = {"id" : pt, "params" : block["params"], "description" : block["description"], "deprecated":block["deprecated"]}
                    ret.append(new)
            else:
                ret.append(block)

        return ret

    def decode_topic(txt):
        regex = r"<h2 id=\"(.+?)\""
        ret = None
        matches = re.finditer(regex, txt, re.MULTILINE)
        for match in matches:
            ret = match.group(1)
            break
        if ret is None:
            raise Exception("Topic not found!")
        return ret

    def decode_areas(txt):
        ret = []

        regex = r"<h[34] id=\"(.+?)\">.+?<\/h[34]>"
        matches = re.finditer(regex, txt, re.MULTILINE)
        for match in matches:
            area = {}
            area["id"] = match.group(1)
            area["start_index"] = match.start()
            if len(ret) > 0:
                ret[len(ret)-1]["end_index"] = match.start() - 1
            ret.append(area)

        ret[len(ret)-1]["end_index"] = len(txt)

        for area in ret:
            si = area["start_index"]
            ei = area["end_index"]
            area["text"] = txt[si:ei]

        return ret

    def decode_param(par:str):
        ret = []
        if par is not None:
            par = par.strip()
            if len(par) != 0 and par is not "N/A":
                regex = r"\[(\d+)\]:([^\[]+)"
                matches = re.finditer(regex, par, re.MULTILINE)
                for match in matches:
                    p = { 
                        "index": match.group(1),
                        "description":match.group(2).replace("&nbsp;", " ").strip()
                    }
                    ret.append(p)
        return ret

    def decode_params(blocks):
        for block in blocks:
            par = decode_param(block["params"])    
            block["params"] = par

    def clean_out_analysis_data(areas):
        for area in areas:
            del area["start_index"]
            del area["end_index"]
            del area["text"]

    topic = decode_topic(txt)
    areas = decode_areas(txt)
    for area in areas:
        rows = split_to_rows(area["text"])
        blocks = split_to_blocks(rows)
        blocks = expand_multidef_blocks(blocks)
        decode_params(blocks)
        area["blocks"] = blocks
    clean_out_analysis_data(areas)  
    
    return topic, areas

def print_result(topic, areas):
    print ("TOPIC " + topic)
    for area in areas:
        print("\t" + area["id"])
        for block in area["blocks"]:
            print("\t\t", block["id"])
            sh = block["description"]
            if len(sh) > 64:
                sh = sh[:64] + "..."
            print("\t\t\t", sh)
            if len(block["params"]) > 0:
                print("\t\t\t", block["params"])
            # print("\t\t", block[1])
            # print("\t\t\t\t", block[2]) 

def save_result(topic, areas):
    output_file_name = f"{topic}.json"
    import json
    json_data = json.dumps(areas, indent=2)
    with open(output_file_name, "w") as f:
        f.write(json_data)        
        
def generate_cs_client_events(topic, areas):
    def do_pascal(txt):
        ret = txt
        ret = ret.replace("-", "_")
        ret = ret.replace('_',' ').title()
        ret = ret.replace(' ','')
        return ret

    class_name = do_pascal(topic)
    ret = f"public static class {class_name}" + "{\n"

    for area in areas:
        class_name = do_pascal(area["id"])
        ret += f" public static class {class_name}" + "{\n"
        
        for block in area["blocks"]:
            if block["deprecated"]:
                ret += f"  [Deprecated]\n"
            for par in block["params"]:
                ret += f"  [Param({par['index']},\"{par['description']}\")]\n"
            ret += f"  public const string {block['id']} = \"{block['id']}\";\n\n"
        
        ret += " }\n"
    
    ret += "}\n"
    
    output_file_name = f"{topic}.cs"
    with open(output_file_name, "w") as f:
        f.write(ret)

# LOAD
with open(input_file_name) as f:
    txt = f.read()
txt = txt.replace("\n", " ")

# ANALYSIS
topic, areas = analyse(txt)  

# PRINTING RESULTS
print_result(topic, areas)

# SAVING RESULTS
save_result(topic, areas)

# generate CS
generate_cs_client_events(topic, areas)
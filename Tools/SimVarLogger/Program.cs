using ESimConnect;
using ESimConnect.Definitions;
using ESimConnect.Extenders;
using Microsoft.Win32;

string outDir = ".\\data";
bool markSecondsInData = false;
List<SimVarDef> simVars = new()
{
  new ("PLANE ALT ABOVE GROUND"),
  new ("SIM ON GROUND"),
  new ("SIM ON GROUND", Period: SimConnectPeriod.SECOND, File:"SIM_ON_GROUND_SECOND.txt"),
  new ("VERTICAL SPEED",Unit:"feet/minute"),
  new ("VERTICAL SPEED", Unit:"feet/minute", Period:SimConnectPeriod.SECOND, File:"VERTICAL_SPEED_SECOND.txt"),
  new ("ACCELERATION BODY Y"),
  new ("GEAR IS ON GROUND:0"),
  new ("GEAR IS ON GROUND:1"),
  new ("GEAR IS ON GROUND:2")
};

ESimConnect.ESimConnect simCon = new();

Console.WriteLine($"Output path: \t'{outDir}'");
Directory.CreateDirectory(outDir);

Console.WriteLine("Collected SimVars:");
simVars.ForEach(q => Console.WriteLine($"\t{q}"));

Console.WriteLine("Opening");
OpenSimCon(simCon);

Console.WriteLine("Registering");
Dictionary<RequestId, SimVarDef> requests = new();
Dictionary<RequestId, List<double>> values = new();
RegisterSimVars(simVars, simCon, requests, values);

ListeForSecondIfRequired(markSecondsInData, simCon, values);

Console.WriteLine("All ready, press any key to start, then later any key to exit.");

Console.ReadKey();
simCon.DataReceived += SimCon_DataReceived;

Console.WriteLine("Running");
Console.ReadKey();

simCon.DataReceived -= SimCon_DataReceived;
simCon.Close();

Console.WriteLine("Saving");

foreach (var requestId in requests.Keys)
{
  var simVar = requests[requestId];
  var datas = values[requestId];

  Save(simVar, datas);
}

// functions
void SimTimeExtender_SimSecondElapsed()
{
  foreach (var key in values.Keys)
  {
    var lst = values[key];
    lock (lst)
    {
      lst.Add(double.NaN);
    }
  }
}

void Save(SimVarDef simVar, List<double> datas)
{
  string fileName = simVar.File ?? System.IO.Path.Combine(outDir, $"{simVar.Name.Replace(" ", "_").Replace(":", "-")}.txt");
  fileName = System.IO.Path.GetFullPath(fileName);
  Console.WriteLine($"Storing {simVar} with {datas.Count} records to {fileName}.");
  var lines = string.Join("\n", datas);
  System.IO.File.WriteAllText(fileName, lines);
}

void SimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (values.TryGetValue(e.RequestId, out List<double>? lst))
  {
    lock (lst)
    {
      lst.Add((double)e.Data);
    }
  }
}

static void RegisterSimVars(List<SimVarDef> simVars, ESimConnect.ESimConnect simCon, Dictionary<RequestId, SimVarDef> requests, Dictionary<RequestId, List<double>> values)
{
  foreach (var simVar in simVars)
  {
    string unit;
    string sv;
    TypeId typeId = simCon.Values.Register<double>(sv = simVar.Name, unit = simVar.Unit);
    RequestId requestId = simCon.Values.RequestRepeatedly(typeId, simVar.Period, false);
    requests[requestId] = simVar;
    values[requestId] = new List<double>();
  }
}

static void OpenSimCon(ESimConnect.ESimConnect simCon)
{
  simCon.Open();
  Thread.Sleep(1000);
}

void ListeForSecondIfRequired(bool markSecondsInData, ESimConnect.ESimConnect simCon, Dictionary<RequestId, List<double>> values)
{
  if (markSecondsInData)
  {
    ESimConnect.Extenders.SimTimeExtender extTime = new(simCon, false);
    extTime.SimSecondElapsed += SimTimeExtender_SimSecondElapsed;
  }
}

record SimVarDef(string Name, string Unit = "Number", SimConnectPeriod Period = SimConnectPeriod.SIM_FRAME, string File = "");
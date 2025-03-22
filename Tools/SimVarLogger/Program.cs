// See https://aka.ms/new-console-template for more information

using ESimConnect;
using Microsoft.Win32;

string outDir = ".\\data";
List<string> simVars = new List<string>()
{
  "PLANE ALT ABOVE GROUND",
  "SIM ON GROUND",
  "VERTICAL SPEED;feet/minute",
  "ACCELERATION BODY Y",
  "GEAR IS ON GROUND:0",
  "GEAR IS ON GROUND:1",
  "GEAR IS ON GROUND:2"
};

var simCon = new ESimConnect.ESimConnect();

Console.WriteLine($"Output path: \t'{outDir}'");
Directory.CreateDirectory(outDir);

Console.WriteLine("Collected simvars:");
simVars.ForEach(q => Console.WriteLine($"\t{q}"));

Console.WriteLine("Opening");
simCon.Open();
Thread.Sleep(1000);

Console.WriteLine("Registering");
Dictionary<RequestId, string> requests = new();
Dictionary<RequestId, List<double>> values = new();
foreach (var simVar in simVars)
{
  string unit;
  string sv;
  if (simVar.Contains(";"))
  {
    string[] pts = simVar.Split(";");
    sv = pts[0];
    unit = pts[1];
  }
  else
  {
    sv = simVar;
    unit = "Number";
  }
  TypeId typeId = simCon.Values.Register<double>(sv, unit);
  RequestId requestId = simCon.Values.RequestRepeatedly(typeId, SimConnectPeriod.SIM_FRAME, false);
  requests[requestId] = sv;
  values[requestId] = new List<double>();
}

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

void Save(string simVar, List<double> datas)
{
  string fileName = System.IO.Path.Combine(outDir, $"{simVar.Replace(" ", "_").Replace(":", "-")}.txt");
  fileName = System.IO.Path.GetFullPath(fileName);
  Console.WriteLine($"Storing {simVar} with {datas.Count} records to {fileName}.");
  var lines = string.Join("\n", datas);
  System.IO.File.WriteAllText(fileName, lines);
}

void SimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (values.TryGetValue(e.RequestId, out List<double>? lst))
  {
    lst.Add((double)e.Data);
  }
}
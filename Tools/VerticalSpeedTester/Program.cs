using ESimConnect;
using Microsoft.Win32;
using System.Windows.Xps.Serialization;

ESimConnect.ESimConnect simCon = new();

Console.WriteLine("Opening connection");
OpenSimCon(simCon);

Console.WriteLine("Registering");
ESimConnect.Extenders.VerticalSpeedExtender vse = new(simCon);
vse.TouchdownDetected += (sender) => Console.WriteLine("Touchdown detected");
vse.TouchdownEvaluated += (sender, e) => Console.WriteLine($"Touchdown evaluated: {e:N3} ft/min");

vse.Start();
Console.WriteLine("Running");

while (true)
{
  double gvs = vse.GetCurrentGroundVerticalSpeed();
  double pvs = vse.GetCurrentPlaneVerticalSpeed();
  Console.WriteLine($"Plane VS: {pvs:N3} ft/min, Ground VS: {gvs:N3} ft/min");
  Thread.Sleep(500);
}

static void OpenSimCon(ESimConnect.ESimConnect simCon)
{
  simCon.Open();
  Thread.Sleep(1000);
}



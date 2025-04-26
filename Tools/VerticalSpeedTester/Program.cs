using ESimConnect;
using Microsoft.Win32;
using System.Windows.Xps.Serialization;

ESimConnect.ESimConnect simCon = new();

Console.WriteLine("Opening connection");
OpenSimCon(simCon);

Console.WriteLine("Registering");
ESimConnect.Extenders.VerticalSpeedExtender vse = new(simCon);
vse.OnTouchdownDetected += (sender) => Console.WriteLine("Touchdown detected");
vse.OnTouchdownEvaluated += (sender, e) => Console.WriteLine($"Touchdown evaluated: {e:N3} ft/min");

Console.WriteLine("Running");

while (true)
{
  double gvs = vse.GetGroundVerticalSpeed();
  double pvs = vse.GetPlaneVerticalSpeed();
  Console.WriteLine($"Plane VS: {pvs:N3} ft/min, Ground VS: {gvs:N3} ft/min");
  Thread.Sleep(500);
}

static void OpenSimCon(ESimConnect.ESimConnect simCon)
{
  simCon.Open();
  Thread.Sleep(1000);
}



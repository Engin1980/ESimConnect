// This example shows the basic connection to SimConnect and registering to read SimVar value
// Note project must be build for Windows OS version 7.0

using ESimConnect;

// this variable will later hold the id of registered variable
TypeId? typeId = null;
RequestId? requestId = null;

// create instance & open the connection
ESimConnect.ESimConnect simCon = new();
simCon.Open();

// listen to incoming value changes
simCon.DataReceived += SimCon_DataReceived;

// register let FS knows what will we ask for - here for airplane altitude
// for all SimVars see ESimConnect.Definitions.SimVars
// or documentation at: https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm
typeId = simCon.Values.Register<double>(ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.PLANE_ALTITUDE);

bool readOnce = false;
if (readOnce)
{
  requestId = simCon.Values.Request(typeId.Value);
}
else
{
  requestId = simCon.Values.RequestRepeatedly(typeId.Value, SimConnectPeriod.SECOND, true);
}


// this function will be invoked on every incoming data
void SimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (e.RequestId == requestId)
  {
    double data = (double)e.Data;
    Console.WriteLine("New data are: " + data);
  }
}
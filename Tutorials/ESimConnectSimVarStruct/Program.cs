// This example shows the basic connection to SimConnect and registering complex struct to get multiple values at once
// Note project must be build for Windows OS version 7.0

using ESimConnect;
using ESimConnectSimVarStruct;

// this variable will later hold the id of registered variable
TypeId? typeId = null;
RequestId? requestId = null;

// create instance & open the connection
ESimConnect.ESimConnect simCon = new();
simCon.Open();

// listen to incoming value changes
simCon.DataReceived += SimCon_DataReceived;

// open and note how structure is defined in ESimConnectSimVarStruct.CommonDataStruct
// for all definitions see ESimConnect.Definitions.SimVars
// or documentation at: https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm
typeId = simCon.Structs.Register<CommonDataStruct>();

bool readOnce = false;
if (readOnce)
{
  requestId = simCon.Structs.Request<CommonDataStruct>();
}
else
{
  requestId = simCon.Structs.RequestRepeatedly<CommonDataStruct>(SimConnectPeriod.SECOND, true);
}

// this function will be invoked on new incoming data
void SimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (e.RequestId == requestId)
  {
    CommonDataStruct data = (CommonDataStruct)e.Data;
    Console.WriteLine("New data are: " + data.ToString());
  }
}
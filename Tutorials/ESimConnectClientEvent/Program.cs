// This example shows the basic connection to SimConnect and invoking SimConnect client event
// Note project must be build for Windows OS version 7.0

using ESimConnect;

// create instance & open the connection
ESimConnect.ESimConnect simCon = new();
simCon.Open();

//string clientEventName = "PARKING_BRAKES"; //Y
string clientEventName = "TOGGLE_ELECTRICAL_FAILURE"; //N
//string clientEventName = "PITOT_HEAT_TOGGLE"; //Y
//string clientEventName = "TOGGLE_ENGINE1_FAILURE"; //N
//string clientEventName = "TOGGLE_PITOT_BLOCKAGE"; //y
//string clientEventName = "TOGGLE_HYDRAULIC_FAILURE"; //N
//string clientEventName = "TOGGLE_VACUUM_FAILURE"; // Y
//string clientEventName = "TOGGLE_TOTAL_BRAKE_FAILURE"; //Y
//string clientEventName = "TOGGLE_STATIC_PORT_BLOCKAGE"; // Y
//string clientEventName = "TOGGLE_RIGHT_BRAKE_FAILURE"; // Y


while (true)
{
  simCon.ClientEvents.Invoke(clientEventName);
  Console.WriteLine("Sent, press a key..");
  Console.ReadKey();
}


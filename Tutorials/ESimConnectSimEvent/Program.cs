// This example shows the basic connection to SimConnect and registering to Sim-System-Event
// Note project must be build for Windows OS version 7.0

using ESimConnect;

// this variable will later hold the id of registered event
EventId? registeredEventId = null;

// create instance & open the connection
ESimConnect.ESimConnect simCon = new();
simCon.Open();

// listen to incoming system events
simCon.SystemEventInvoked += SimCon_SystemEventInvoked;

// register to the event invoked every sim second
// for all events see ESimConnect.Definitions.SimEvents
// or documentation at: https://docs.flightsimulator.com/html/Programming_Tools/Event_IDs/Event_IDs.htm
registeredEventId = simCon.SystemEvents.Register(ESimConnect.Definitions.SimEvents.System._1sec, false);


// this function will be invoked on every incoming system event
void SimCon_SystemEventInvoked(
  ESimConnect.ESimConnect sender,
  ESimConnect.ESimConnect.ESimConnectSystemEventInvokedEventArgs e)
{
  if (e.EventId == registeredEventId)
  {
    Console.WriteLine("One second event invoked");
  }
}
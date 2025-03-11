# ESimConnect Tutorial - Listening for Sim Events

Sim Events are events invoked by Flight Simulator, informing that same action is happening or did happen. The common example are sim events reporting that Sim has been paused or unpaused, that the simulation has started, or that a second has elapsed.

The full list of available Sim Events are available in [the documentation](https://docs.flightsimulator.com/html/Programming_Tools/Event_IDs/Event_IDs.htm).

## Project Set Up

To use this feature in your project, your project must meet following criteria:
* Project is targeted at .NET 6.0
* Project is targeted at operating system Windows, version 7.0
* Project has a `ESimConnect.dll` in the dependencies.
* Project has MS SimConnect libraries - ``  and ``  - in the output folder

## Example of usage

> For the full code, see `Program.cs` file content.

Firstly, the imports must be done:
```
using ESimConnect;
```

Then, a variable of type `EventId` must be declared. This variable is holding a unique Id representing the registered event. If you register listening to multiple events, the invoked event will be determined by its `EventId`:

```
EventId? registeredEventId = null;
``` 

As next, `ESimConnect` instance is created and opened - opening creates a communication channel between FS and the ESimConnect:
``` 
ESimConnect.ESimConnect simCon = new();
simCon.Open();
``` 

As next, we will register an event listener, what will inform us that some _System Event_ has been invoked - the content of the method handler `SimCon_SystemEventInvoked()` will be explained later:

```
simCon.SystemEventInvoked += SimCon_SystemEventInvoked;
``` 

Finally, we will tell ESimCon that we would like to be informed about the _System Event_ every second:

```
registeredEventId = simCon.SystemEvents.Register(
      ESimConnect.Definitions.SimEvents.System._1sec, 
      false);
```

Note that `ESimConnect.Definitions.SimEvents`  contains the string definitions (names) for all SimEvents w.r.t. the documentation. To see full list of events and their explanation see https://docs.flightsimulator.com/html/Programming_Tools/Event_IDs/Event_IDs.htm.

Now, lets explain the code of the listener invoked, when _System Event_ is reported:

```
void SimCon_SystemEventInvoked(
  ESimConnect.ESimConnect sender,
  ESimConnect.ESimConnect.SimConnectSystemEventInvokedEventArgs e)
{
  if (e.EventId == registeredEventId)
  {
    Console.WriteLine("One second event invoked");
  }
}
```

Note that if you are registered to more _System Events_, you must do the check for the corresponding `EventId` to be sure that the reported _System Event_ is the required one.
# ESimConnect Tutorial - Simple value read from FS

In this tutorial, how to read a simple value from FS is explained.

## Introduction

### What is SimVar and L-Var

In Microsoft Flight Simulator (FS), **SimVar** (short for Simulation Variable) is a system used to retrieve and manipulate various data points related to the aircraft, environment, and simulation state. These variables allow developers, modders, and script writers to access real-time information about the aircraft's speed, altitude, engine status, weather conditions, and more.

Every _SimVar_ has its name. You must know the correct _SimVar_ name to register and obtain its value. The list of available _SimVars_ is available in the documentation:

> https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm

In addition, a custom defined SimVars, **L-Vars** (Local Variables) in Microsoft Flight Simulator (MSFS) are used primarily by aircraft developers and add-on creators to store and manipulate values specific to their aircraft systems. Unlike built-in SimVars, which are globally available for all aircraft, L-Vars are unique to a particular aircraft or add-on. They are often used for custom autopilot modes, cockpit switches, animations, and complex aircraft systems. L-Vars are stored within the aircraft and reset when switching aircraft or restarting the sim. _L-Var_ names are airplane/developer specific and are typically available on product development pages. For example, vars for FBW A320 are available in their documentation:
> https://github.com/flybywiresim/aircraft/blob/master/fbw-a32nx/docs/a320-simvars.md

### What to know about SimVar before reading value

In the followign text, we will use _SimVar_ as a term for both _SimVar_ and _L-Var_.

To read out _SimVar_ value, you need to know:
* _SimVar_ name
* _SimVar_ unit - defining in which unit you would like to receive results. Default is `"number"`, for full listing see https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variable_Units.htm
* _SimVar_ type - defining the datatype in which the result is returned. Default is `"FLOAT64"`, for full listing see https://docs.flightsimulator.com/html/Programming_Tools/SimConnect/API_Reference/Structures_And_Enumerations/SIMCONNECT_DATATYPE.htm

For repetitive _SimVar_ value reading, you also need to know:
* Interval - describing how often should the value be returned from the sim. The common and default value is `SECOND` returning value returned every second. Other options are avialable at: https://docs.flightsimulator.com/html/Programming_Tools/SimConnect/API_Reference/Structures_And_Enumerations/SIMCONNECT_PERIOD.htm

## Project Set Up

To use this feature in your project, your project must meet following criteria:
* Project is targeted at .NET 6.0
* Project is targeted at operating system Windows, version 7.0
* Project has a `ESimConnect.dll` in the dependencies.
* Project has MS SimConnect libraries - ``  and ``  - in the output folder

## Opening connection to the Sim

In a pure way, to establish a connection to FS using ESimConnect, you would use the following sequence:

```;
ESimConnect.ESimConnect simCon = new();
simCon.Open();
```

However, the `Open()` method may fail with exception, e.g., if the sim is not ready yet.

You can deal with this using custom implementation based on exception handling and repetitive calls, or use custom extender called `OpenInBackgroundExtender`. This extender provides a method starting open attempts; once the open is successfull, the event is invoked. Moreover, a custom callback can be directly passed as a method parameter to invoke on successfull open.

```
ESimConnect.ESimConnect simCon = new();

ESimConnect.Extenders.OpenInBackgroundExtender 
  openInBackgroundExtender = new(simCon);

openInBackgroundExtender.OpenInBackground(
    onOpenedCallback);
```

Capturing Sim-Second-Elapsed events and Game Pause/Unpause

Similarly, you can use extender, which provides events invoked when a simulation second is elapsed; or when the simulation is being paused or unpaused.

To do so, use the appropriate extender and join its events:
```
ESimConnect.ESimConnect simCon = new();

ESimConnect.Extenders.SimTimeExtender 
  simTimeExtender = new(simCon, true);

simTimeExtender.SimSecondElapsed += 
  SimTimeExtender_SimSecondElapsed;
```

## Example

> For the full code, see `Program.cs` file content.

Firstly, import the namespace:
```
using ESimConnect;
```

Then, declare required variables:
```
TypeId? typeId = null;
RequestId? requestId = null;
```

`TypeId` will contain unique Id when the SimVar is registered. It is useful if you ask for the same SimVar multiple times with different properties, e.g., different unit. Every registration is represented by its `TypeId`.

`RequestId` identifies the appropriate data request. Every single or repeated request has its custom unique `RequestId`. Once data has arrived, you can check for the `RequestId` to see what exactly did you get.

Now, lets open the connection to FS:
```
ESimConnect.ESimConnect simCon = new();
simCon.Open();
```

As next, let's get informed once any data arrives - then handler will be explained below:
```
simCon.DataReceived += SimCon_DataReceived;
```

As next, register a _SimVar_ as a type to get it's `TypeId`:

```
typeId = simCon.Values.Register<double>(
    ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.PLANE_ALTITUDE);
```
Note that we have used predefined constants from `ESimConnect. Definitions.SimVars`.

If you would like to obtain the value only once, you can simply ask for:
```
requestId = simCon.Values.Request(typeId.Value);
```
For repetitive requests, apply:
```
requestId = simCon.Values.RequestRepeatedly(
    typeId.Value, 
    SimConnectPeriod.SECOND, 
    true);
```

The third parameter means if the value should be updated even if the sim is paused.

In both cases, we obtained `RequestId`.

Finally, let's explain the handler. If there are incoming data, we look if the data are from our registered requests. If so, data are printed:
```
void SimCon_DataReceived(
    ESimConnect.ESimConnect sender, 
    ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (e.RequestId == requestId)
  {
    double data = (double)e.Data;
    Console.WriteLine("New data are: " + data);
  }
}
```
# ESimConnect Tutorial - Multiple vlaues read from FS at once

In this tutorial, how to read a multiple values at once from FS is explained.

Note that **this technique is a bit more complex** and you may have some programming experience to implement it correctly.

## Introduction

Firstly, please read the Introduction section in `ESimConnectSimVarRead` project, as basics are explained there.

### Simple value reading
As explained in the above mentioned tutorial, _SimVars_ are read as single values per request in the predefined datatype. That means, FS sends the value of a registered _SimVar_ on every data update.

As you may need to read multiple values, it's better to read them all at once instead one by one.

### Multiple values reading - Struct definition

To read multiple values at once, you need to define your custom struct datatype in C# in the appropriate format:
* The struct must have predefined struct layout: `[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]`
* The struct must have public fields, where every field has its own `[DataDefinition(...)] attribute assigning a _SimVar_ to the field.
* If `string` should be read out, additional `MarshalAs(...)` attribute is required. The size in the attribute defines the expected length of the string.

An example of a simple struct:

```
using ESimConnect.Definitions;
using ESimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnectSimVarStruct
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
  internal struct DataStruct
  {
    [DataDefinition(SimVars.Aircraft.RadioAndNavigation.ATC_ID)]
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string callsign;

    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "1")]
    public int engineOneCombustion;

    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "2")]
    public int engineTwoCombustion;

    [DataDefinition(SimVars.Aircraft.BrakesAndLandingGear.BRAKE_PARKING_POSITION)]
    public int parkingBrake;

    [DataDefinition(SimVars.Services.PUSHBACK_ATTACHED)]
    public int pushbackTugConnected;
  }
}
```

## Project Set Up

To use this feature in your project, your project must meet following criteria:
* Project is targeted at .NET 6.0
* Project is targeted at operating system Windows, version 7.0
* Project has a `ESimConnect.dll` in the dependencies.
* Project has MS SimConnect libraries - ``  and ``  - in the output folder

## Example

> For the full code, see `Program.cs` file content.

Firstly, import the namespace:
```
using ESimConnect;
```

Then, declare required variable:
```
RequestId? requestId = null;
```
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

Next, register the structure type:
```
simCon.Structs.Register<DataStruct>();    
```

If you would like to obtain the value only once, you can simply ask for:
```
requestId = simCon.Structs.Request<DataStruct>();
```
For repetitive requests, apply:
```
  requestId = simCon.Structs.RequestRepeatedly<DataStruct>(SimConnectPeriod.SECOND, true);
```

The third parameter means if the value should be updated even if the sim is paused.

In both cases, we obtained `RequestId`.

Finally, let's explain the handler. If there are incoming data, we look if the data are from our registered requests. If so, data are printed:
```
void SimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  if (e.RequestId == requestId)
  {
    CommonDataStruct data = (CommonDataStruct)e.Data;
    Console.WriteLine("New data are: " + data.ToString());
  }
}
```
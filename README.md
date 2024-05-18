# ESimConnect

A simple library to connect and work with SimConnect, a library allowing to communicate with Flight Simulators (e.g., FS2020).

The aim of this library is to provide a simple interface to work with FS2020 over .NET/C# without no deep knowledge of SimConnect. On the other side, only the basic operations are supported. This library is not covering all the possibilities of original SimConnect library.

## Requirements

The library needs Windows .NET Core 6.0 runtime to be installed (it is included in Windows 10+ instalation by default).

## Instalation

Just unpack the required release to a target project run folder and link ESimConnect.dll + other provided libraries to your project.
Note: To work, a file `SimConnect.dll` must be also included in your project output directory. This file will not be referenced in your .NET project, but must be present to be loaded for library usage.

## Simple testing application

A simple example application __ESimConnectDemo__ is available in the project. 

When running FS2020, start `ESimConnectDemo.exe`. In the window, select `Connect/Disconnect` button. In `SimVars` tab, enter SimVar name and press `Add`. You should immediatelly see the value of the SimVar in the window.

Note: For specific SimVar names, look to [FS202 SimVar documentation](https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm). At the beginning, you can try to enter `PLANE ALTITUDE` [SimVar](https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Aircraft_SimVars/Aircraft_Misc_Variables.htm#PLANE_ALTITUDE).

## Usage in your project

### Referencing libraries

1. Create a new project based on .NET 6.
2. In your project, add references to assemblies `ESimConnect.dll` and all the assemblies from the `DLLs` folder except `SimConnect.dll`. However, file `SimConnect.dll` must be present in the output folder, otherwise error on the startup will be raised.

### Example of a simple usage

For a simple SimVar readout (e.g., `PLANE ALTITUDE`), you need to:
```

// create ESimConnect instance
ESimConnect.ESimConnect eSimCon = new();

// register as a listener for incoming data messages
eSimCon.DataReceived += ESimCon_DataReceived;  // see below for the definition

// open a connection to FS2020
eSimCon.Open();

// register a SimVar - tell FS2020 that you are interested in this SimVar
var typeId = eSimCon.Values.Register<double>("PLANE ALTITUDE");

// and request the value once
RequestId requestId = eSimCon.Values.Request(typeId);

// or request value repeatedly every second, only when value has changed
RequestId repeatedRequestId = eSimCon.Values.RequestRepeatedly(typeId, SimConnectPeriod.SECOND, true);

// now you are ready
// once some data has arrived, the following handler is invoked:
private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
{
  Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, simVar={relatedSimVar}, type={e.Type}, data={e.Data}");
}
```

For more detailed info, see [ESimConnectDemo](https://github.com/Engin1980/ESimConnect/tree/master/ESimConnectDemo) or [ESimConnectTest](https://github.com/Engin1980/ESimConnect/blob/master/ESimConnectTest/Tests/ValuesTest.cs) projects for inspiration.


## Issues

If anything does not work, feel free to report it as an [issue](https://github.com/Engin1980/ESimConnect/issues). Please provide as many details as possible.

## FAQ

(nothing yet)

## License

See [LICENSE](https://github.com/Engin1980/ESimConnect/blob/master/LICENSE) file.

## Credits

Thanks to [RandFailuresFS2020](https://github.com/kanaron/RandFailuresFS2020) original repo for being an initial motivation and a study source.

Thanks to [George Barlow](https://github.com/GeorgeBarlow) for his help with resolving unregistering issues.

## Contact

Marek Vajgl
https://github.com/Engin1980/ESimConnect

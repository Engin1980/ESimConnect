# ESimConnect

A simple library to connect and work with SimConnect, a library allowing to communicate with Flight Simulators (e.g., FS2020).

The aim of this library is to provide a simple interface to work with FS2020 over .NET/C# without no deep knowledge of SimConnect. On the other side, only the basic operations are supported. This library is not covering all the possibilities of original SimConnect library.

## Requirements

The library needs Windows .NET 6.0 runtime to be installed (it is included in Windows 10+ instalation by default).

## Instalation & Usage in your project

Download the release and unpack it to obtain .DLL and other files.

To add library to your project:
* Ensure project is .NET 6.0 (or higher)
* Ensure project target OS is Windows and version 7.0
* Add dependencies:
  * `Microsoft.FlightSimulator.SimConnect.dll` 
  * `ESimConnect.dll`
  * `ESystem.dll`.
* Ensure `SimConnect.dll` is copied in the output/startup directory and **not** referenced as a dependency.

If unsure, just check any example project in this repository.

## Examples

### Simple testing application

A simple example application __ESimConnectDemo__ is available in the project. 

When running FS2020, start `ESimConnectDemo.exe`. In the window, select `Connect/Disconnect` button. In `SimVars` tab, enter SimVar name and press `Add`. You should immediatelly see the value of the SimVar in the window.

Note: For specific SimVar names, look to [FS202 SimVar documentation](https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm). At the beginning, you can try to enter `PLANE ALTITUDE` [SimVar](https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Aircraft_SimVars/Aircraft_Misc_Variables.htm#PLANE_ALTITUDE).

For more detailed info, see [ESimConnectDemo](https://github.com/Engin1980/ESimConnect/tree/master/ESimConnectDemo) or [ESimConnectTest](https://github.com/Engin1980/ESimConnect/blob/master/ESimConnectTest/Tests/ValuesTest.cs) projects for inspiration.

### Other Tutorials
There are other tutorials in this repository:
* How to listen to SimEvents - see 'Tutorials/ESimConnectSimEvent' project and [tutorial](Tutorials/ESimConnectSimEvent/readme.md).
* How to read out _SimVar_ value - see 'Tutorials/ESimConnectSimVarRead' project and [tutorial](Tutorials/ESimConnectSimVarRead/readme.md).
* How to read out multiple _SimVars_ via struct at once - see 'Tutorials/ESimConnectSimVarStruct' project and [tutorial](Tutorials/ESimConnectSimVarStruct/readme.md).
* How to use extenders:
  * How to open connection in async way - 'Tutorials/OpenAndSimTimeExtendersExample' project and [tutorial](Tutorials/OpenAndSimTimeExtendersExample/readme.md).
  * How to get notified when sim second elapses - 'Tutorials/OpenAndSimTimeExtendersExample' project and [tutorial](Tutorials/OpenAndSimTimeExtendersExample/readme.md).
  * How to cache _SimVar_ value to read it from different modules - 'Tutorials/ValueCacheExtenderExample' project and [tutorial](Tutorials/ValueCacheExtenderExample/readme.md).
  * How to cache multiple _SimVar_ values in an object to ready them at once - 'Tutorials/TypeCacheExtenderExample' project and [tutorial](Tutorials/TypeCacheExtenderExample/readme.md).


## Issues

If anything does not work, feel free to report it as an [issue](https://github.com/Engin1980/ESimConnect/issues). Please provide as many details as possible.

## FAQ

Q: On startup, an error occurs: `System.IO.FileNotFoundException: 'Could not load file or assembly 'Microsoft.FlightSimulator.SimConnect, Version=11.0.62651.3, Culture=neutral, PublicKeyToken=baf445ffb3a06b5c'.`

A: There are either 'Microsoft.FlightSimulator.SimConnect.dll' or 
'SimConnect.dll' file(s) missing in the working directory.

## License

See [LICENSE](https://github.com/Engin1980/ESimConnect/blob/master/LICENSE) file.

## Credits

Thanks to [RandFailuresFS2020](https://github.com/kanaron/RandFailuresFS2020) original repo for being an initial motivation and a study source.

Thanks to [George Barlow](https://github.com/GeorgeBarlow) for his help with resolving unregistering issues.

## Contact

Marek Vajgl
https://github.com/Engin1980/ESimConnect

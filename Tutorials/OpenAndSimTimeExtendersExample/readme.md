# ESimConnect Tutorial - Asynchronous Opening and listening to Simulation Seconds

There are two tasks covered in this tutorial:
* how to open connection to Flight Simulator in repetitive cycles until success;
* how to be informed about every second elapsed in the sim.

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

## Example of usabe for both cases

> For the full code, see `Program.cs` file content.

Firstly, the ESimCon and extenders are instantiated:
```
// create instance of simCon and extenders
ESimConnect.ESimConnect simCon = new();

// this extender is good for async opening
ESimConnect.Extenders.OpenInBackgroundExtender openInBackgroundExtender = new(simCon);

// this extender is good for monitoring elapsed time in sim and pausing
ESimConnect.Extenders.SimTimeExtender simTimeExtender = new(simCon, true);

```

Then, sim-second-elapsed event is subscribed:
```
simTimeExtender.SimSecondElapsed += SimTimeExtender_SimSecondElapsed;
```

Then, the sim is connected/opened - the callback `onConnected()` is explained below:

```
// open the connection
// once connected, invoke 'onConnected()' function
openInBackgroundExtender.OpenInBackground(onConnected);
``` 

Finally, an app in held alive for 30 seconds:
```
// let the app run for some time
Thread.Sleep(30000);
```

Finally, the code of simple event listeners:
```
// this will run once the connection is established
void onConnected()
{
  Console.WriteLine("Started");
}

// this function will be invoked on every sim second elapsed
void SimTimeExtender_SimSecondElapsed()
{
  Console.WriteLine("Sim-Second elapsed");
}
```
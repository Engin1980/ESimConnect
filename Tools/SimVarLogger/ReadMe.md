# Sim Var Logger

A simple tool to read out SimVars from FS and store them into the text files.

## Usage

> There is no settings/config file. All settings must be done in source code and the code must be recompiled.

### Set up what to monitor

At the beginning of `Program.cs`,  write the monitored SimVars, units, refresh interval and file-name, like:
```
List<SimVarDef> simVars = new()
{
  new ("PLANE ALT ABOVE GROUND"),
  new ("SIM ON GROUND"),
  new ("SIM ON GROUND", Period: SimConnectPeriod.SECOND, File:"SIM_ON_GROUND_SECOND.txt"),
  new ("VERTICAL SPEED",Unit:"feet/minute"),
  new ("VERTICAL SPEED", Unit:"feet/minute", Period:SimConnectPeriod.SECOND, File:"VERTICAL_SPEED_SECOND.txt"),
  new ("ACCELERATION BODY Y"),
  new ("GEAR IS ON GROUND:0"),
  new ("GEAR IS ON GROUND:1"),
  new ("GEAR IS ON GROUND:2")
};
```

Parameters:
* **Name** is mandatory name of SimVar
* **Unit** is optional SimConnect unit name (string), default is `Number`
* **Period** is optional SimConnect period (SimConnectPeriod), default is `SIM_FRAME`
* **File** is optional file name. If not specified (empty or `null`), file name is derived from `Name`. Note if you have multiple times the same variable defined (e.g. with different unit or interval), the file name should be set, otherwise the output file will be same for every declaration and the content will be rewritten.

### Set up if to mark "elapsed second"

If

```
bool markSecondsInData = true;
```

is true, at every sim second value `NaN` will be inserted into the data.

### Set up output

Set the target output diretorory via:

```
string outDir = ".\\data";
```

Note that the files will be overwritten if necessary.

## Run

Just exectue the app. It should get to the state:

```
All ready, press any key to start, then later any key to exit.
```

Once a key is pressed, the recording will start.

Once **again** a key is pressed, the recording is stopped and data are written into the output files.
# ESimConnect Tutorial - Caching multiple FS SimVar Values  

# Motivation

Sometimes you would like to obtain several _SimVars_ in one object, but you to not have enough experience or will to play with _SimVar structs_. 

Then, you can create simple class with properties, annotate a property with simple attributes and then just obtain the instance of such class at any moment, containing current values.

To do so, there is `TypeCacheExtender`.

# Principle

> To continue here, we expect you are familiar with `ESimConnectSimVarRead` tutorial.

Principle:
1. Create your own class with properties, annotate each property with `SimProperty` attribute.

```
  class SimDataSnapshot
  {
    [SimProperty(
        SimVars.Aircraft.Miscelaneous.PLANE_ALT_ABOVE_GROUND, 
        SimUnits.Length.FOOT)]
    public int Height { get; set; }
    
    [SimProperty(
        SimVars.Aircraft.Miscelaneous.AIRSPEED_INDICATED, 
        SimUnits.Speed.KNOT)]
    public int IndicatedSpeed { get; set; }
  }
```

2. Register this type using `TypeCacheExtender`.
```
typeCache.Register<SimDataSnapshot>();
```

3. Get the snapshot with the current values anytime using `GetSnapshot()` function: 
```
SimDataSnapshot snapshot = typeCache.GetSnapshot<SimDataSnapshot>();
```

## Project Set Up

To use this feature in your project, your project must meet following criteria:
* Project is targeted at .NET 6.0
* Project is targeted at operating system Windows, version 7.0
* Project has a `ESimConnect.dll` in the dependencies.
* Project has MS SimConnect libraries - ``  and ``  - in the output folder

## Example

> For the full code, see `Program.cs` file content.

Firstly, import required namespaces:
```
using ESimConnect;
using TypeCacheExtenderExample;
```

As next, create `ESimConnect` and extenders:
```
ESimConnect.ESimConnect simCon 
  = new();
ESimConnect.Extenders.ValueCacheExtender valueCache 
  = new(simCon, SimConnectPeriod.SECOND);
ESimConnect.Extenders.TypeCacheExtender typeCache 
  = new(valueCache);
```

Open the FS SimConnect:
```
simCon.Open();
```

As next, register the type to `TypeCacheExtender`:
```
typeCache.Register<SimDataSnapshot>();
```

Finally, read out the data snapshot anytime when required:
```
while (true){
  Thread.Sleep(1000);
  SimDataSnapshot snapshot = 
    typeCache.GetSnapshot<SimDataSnapshot>();
  Console.WriteLine("New data are: " + snapshot.ToString());  
}
```
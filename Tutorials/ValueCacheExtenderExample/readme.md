# ESimConnect Tutorial - Reading FS Value from multiple consumers

# Motivation
If you have larger project of modules, you probably get to the situation where each module registers for the same _SimVar_ for repeated requests in the same interval. This may (slightly) decrease the performance. Therefore, it may be useful to have some _cache_ which will store and provide the value on request. 

To do so, there is the `ValueCacheExtender`. This class is an extender over `ESimConnect` and provides the possibility to register and read out the required _SimVar_. If the same registration is done multiple times, it caches the value for all other data requests.

# Principle

> To continue here, we expect you are familiar with `ESimConnectSimVarRead` tutorial.

Idea:
* 1The point is - do not register for values directly over `ESimConnect`, use `ValueCacheExtender` instead.
* You can register mutliple times for the same _SimVar_ using `ValueCacheExtender`. However, under the hood, only one registration is done into the underlying `ESimConnect`.
* You have possibility to subscribe to the event informing about the value change - same as for `ESimConnect`.
* You can ask for the current _SimVar_ value by calling the appropriate method anytime.

Principle:

1. Register the _SimVar_ to obtain `TypeId`.
```
TypeId typeId = valueCache.Register(
    ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.AIRSPEED_INDICATED, 
    "Knots");
```

2. Subscribe to the appropriate event to be notified about value changes (optionally):
```
valueCache.ValueChanged += ValueCache_ValueChanged;
```

3. Or get the value directly by calling the method (optionally):
```
double value = valueCache.GetValue(typeId);
```


## Project Set Up

To use this feature in your project, your project must meet following criteria:
* Project is targeted at .NET 6.0
* Project is targeted at operating system Windows, version 7.0
* Project has a `ESimConnect.dll` in the dependencies.
* Project has MS SimConnect libraries - ``  and ``  - in the output folder

## Example

> For the full code, see `Program.cs` file content.

Firstly, import namespace:
```
using ESimConnect;
```

```
// this variable will later hold the id of registered variable
TypeId? typeId = null;
```

Next, create the instances of `ESimConnect` and `ValueCachextender`:
```
ESimConnect.ESimConnect simCon = new();
ESimConnect.Extenders.ValueCacheExtender valueCache = new(simCon, SimConnectPeriod.SECOND);
```

Now, subscribe to the event invoked on value change:
```
valueCache.ValueChanged += ValueCache_ValueChanged;
```

Then, open the connection:
```
simCon.Open();
```

Register so FS starts sending info about the _SimVar_ value:
```
typeId = valueCache.Register(
    ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.AIRSPEED_INDICATED, 
    "Knots", 
    SimConnectSimTypeName.FLOAT64);
```

Now, you can ask for the value anytime:
```
while (true)
{
  double value = valueCache.GetValue(typeId.Value);
  Console.WriteLine("Current IAS is: " + value);
  Thread.Sleep(1000);
}

```
Note, that `typeId.Value` is there cos `typeId` is `Nullable<TypeId>`.



Finally, the code invoked once value has changed:
```
void ValueCache_ValueChanged(
    ESimConnect.Extenders.ValueCacheExtender.ValueChangeEventArgs e)
{
  if (e.TypeId == typeId)
  {
    Console.WriteLine("New data of IAS captured in the event are: " + e.Value);
  }
}
```
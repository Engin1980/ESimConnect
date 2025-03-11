// This example shows the usage of extenders for caching values.
// The example registers the IAS value and then reads it every second.
// It also listens to incoming value changes and prints them out. 

// Note project must be build for Windows OS version 7.0

using ESimConnect;
using TypeCacheExtenderExample;

// create instance of simCon and extenders
ESimConnect.ESimConnect simCon = new();
// this extender is good for caching simple values
ESimConnect.Extenders.ValueCacheExtender valueCache = new(simCon, SimConnectPeriod.SECOND);
// this extender is good for caching multiple values of object properties, note ValueCacheExtender is the param!
ESimConnect.Extenders.TypeCacheExtender typeCache = new(valueCache);
// this extender is good for monitoring elapsed time in sim and pausing
ESimConnect.Extenders.SimTimeExtender simTimeExtender = new(simCon, true);

// Connect to FS
simCon.Open();

// register the type with properties to obtain, see SimDataSnapshot.cs
typeCache.Register<SimDataSnapshot>();

// listen for every sim-elapsed second
simTimeExtender.SimSecondElapsed += SimTimeExtender_SimSecondElapsed;

// invoked on every sim-second
void SimTimeExtender_SimSecondElapsed()
{
  // get & print the current data
  SimDataSnapshot snapshot = typeCache.GetSnapshot<SimDataSnapshot>();
  Console.WriteLine("New data are: " + snapshot.ToString());
}
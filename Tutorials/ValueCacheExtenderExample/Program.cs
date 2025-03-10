// This example shows the usage of extenders for caching values.
// The example registers the IAS value and then reads it every second.
// It also listens to incoming value changes and prints them out. 

// Note project must be build for Windows OS version 7.0

using ESimConnect;

// this variable will later hold the id of registered variable
TypeId? typeId = null;

// create instance of simCon and extenders
ESimConnect.ESimConnect simCon = new();
// this extender is good for async opening
ESimConnect.Extenders.OpenInBackgroundExtender openInBackgroundExtender = new(simCon);
// this extender is good for caching simple values
ESimConnect.Extenders.ValueCacheExtender valueCache = new(simCon, SimConnectPeriod.SECOND);

// listen to incoming value changes (if you need to be notified about changes)
valueCache.ValueChanged += ValueCache_ValueChanged;

// open the connection; once connected, invoke 'onConnected()' function
openInBackgroundExtender.OpenInBackground(onConnected);


// this will run once the connection is established
void onConnected()
{
  // register the variable for caching
  typeId = valueCache.Register(
    ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.AIRSPEED_INDICATED, 
    "Knots", 
    SimConnectSimTypeName.FLOAT64);

  // endless loop to read and print the value
  while (true)
  {
    double value = valueCache.GetValue(typeId.Value);
    Console.WriteLine("Current IAS is: " + value);
    Thread.Sleep(1000);
  }
}

// wait for some time
Thread.Sleep(30000);

// this function will be invoked on new incoming data
void ValueCache_ValueChanged(ESimConnect.Extenders.ValueCacheExtender.ValueChangeEventArgs e)
{
  if (e.TypeId == typeId)
  {
    Console.WriteLine("New data of IAS captured in the event are: " + e.Value);
  }
}
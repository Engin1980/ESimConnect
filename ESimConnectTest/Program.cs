using ESimConnectTest;
using System.Windows.Threading;

//PrimitivesTest.Run();
//TypesTest.Run();
ClientEventsTest.Run();
//SystemEventsTest.Run();

//TODO test mixing of primitive and type requests - I think that every request will have its custom
//  request counter, so when received, there will be conflict to distinquish, which 
//  data has been received (when invoked out of ESimConnect via only one event


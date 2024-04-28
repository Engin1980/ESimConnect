using ESimConnectTest.Tests;
using System.Windows.Threading;
using ELogging;
using System.Runtime.CompilerServices;

string LOG_FILE_NAME = "_log.txt";
SetUpLogging();

//ValuesTest.Run();
//StructsTest.Run();
//ClientEventsTest.Run();
SystemEventsTest.Run();

//TODO test mixing of primitive and type requests - I think that every request will have its custom
//  request counter, so when received, there will be conflict to distinquish, which 
//  data has been received (when invoked out of ESimConnect via only one event

void SetUpLogging()
{
  if (File.Exists(LOG_FILE_NAME))
    File.Delete(LOG_FILE_NAME);

  Logger.RegisterLogAction(q =>
  {
    string s = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}  {q.Sender,-25}  {q.Message}\n";
    File.AppendAllText(LOG_FILE_NAME, s);
  },
  new List<LogRule>() { new(".+", LogLevel.TRACE) });
}


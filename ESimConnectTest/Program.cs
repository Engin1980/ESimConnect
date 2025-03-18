using ESimConnectTest.Tests;
using System.Windows.Threading;
using ESystem.Logging;
using System.Runtime.CompilerServices;
using ESimConnectTest.Tests.ValueCacheExtender;

bool useLogging = true;
string LOG_FILE_NAME = "_log.txt";
SetUpLogging();

useLogging = true;

//ValuesTest.Run();
//StructsTest.Run();
//ClientEventsTest.Run();
//SystemEventsTest.Run();
//IgnoreForeignRegistrationTest.Run();
StringTest.Run();

useLogging = false;

//RegisterUnregisterTest.Run();

void SetUpLogging()
{
  if (File.Exists(LOG_FILE_NAME))
    File.Delete(LOG_FILE_NAME);

  Logger.RegisterLogAction(q =>
  {
    if (!useLogging) return;
    string s = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}  {q.Sender,-25}  {q.Message}\n";
    Console.Write(s);
    File.AppendAllText(LOG_FILE_NAME, s);
  },
  new List<LogRule>() { new(".+", LogLevel.TRACE) });
}


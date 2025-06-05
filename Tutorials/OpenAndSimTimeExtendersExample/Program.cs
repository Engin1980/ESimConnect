// This example shows the usage of extenders for opening and detecting elapsed second

// Note project must be build for Windows OS version 7.0

// create instance of simCon and extenders
ESimConnect.ESimConnect simCon = new();
// this extender is good for async opening
ESimConnect.Extenders.OpenInBackgroundExtender openInBackgroundExtender = new(simCon);
// this extender is good for monitoring elapsed time in sim and pausing
ESimConnect.Extenders.SimTimeExtender simTimeExtender = new(simCon, true);

simTimeExtender.SimSecondElapsed += SimTimeExtender_SimSecondElapsed;

// open the connection; once connected, invoke 'onConnected()' function
openInBackgroundExtender.OpenRepeatedlyUntilSuccess(onConnected, ESimConnect.Extenders.OpenInBackgroundExtender.OnOpenActionRepeatMode.FirstOnly);

// let the app run for some time; needed as code is started in 'onConnected()'
Thread.Sleep(30000);

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
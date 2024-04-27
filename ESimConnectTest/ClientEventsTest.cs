using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest
{
  internal class ClientEventsTest
  {
    private static readonly ESimConnect.ESimConnect eSimCon = new();
    private static readonly string eventNoParamsName = "AP_MASTER";
    private static readonly string eventOneParamName = "GEAR_SET";
    private static int eventOneParamValue = 0;

    public static void Run()
    {
      Console.WriteLine("Starting non-WPF");
      Open(eSimCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);
      Sleep(500);
      for (int i = 0; i < 5; i++)
      {
        InvokeNoParamsEvent();
        Sleep(1000);
      }
      for (int i = 0; i < 5; i++)
      {
        InvokeOneParamEvent();
        Sleep(1000);
      }
      Close(eSimCon);
      Sleep(1000);
      Console.WriteLine("Done");
    }

    private static void InvokeOneParamEvent()
    {
      eventOneParamValue = (eventOneParamValue + 1) % 2;
      Console.WriteLine("Invoking one-param event " + eventOneParamName + " with value " + eventOneParamValue);
      eSimCon.ClientEvents.SendClientEvent(eventOneParamName, new uint[] { (uint)eventOneParamValue });
    }

    private static void InvokeNoParamsEvent()
    {
      Console.WriteLine("Invoking no-params event " + eventNoParamsName);
      eSimCon.ClientEvents.SendClientEvent(eventNoParamsName);

    }
  }
}

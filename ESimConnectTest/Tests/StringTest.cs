using ESimConnect;
using ESimConnect.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest.Tests
{
  internal class StringTest
  {
    private static readonly ESimConnect.ESimConnect eSimCon = new();
    private static readonly string[] simVars = new string[]
    {
      "TITLE", "ATC ID", "ATC MODEL", "ATC TYPE", "ATC FLIGHT NUMBER"
    };
    private static readonly Dictionary<string, TypeId> simVarIds = new();
    private static readonly Dictionary<RequestId, string> onceRequestId = new();

    public static void Run()
    {
      Console.WriteLine("Starting non-WPF");
      Open(eSimCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);
      Sleep(500);
      Register();
      Sleep(500);
      RequestOnce();
      Sleep(3000);
      Close(eSimCon);
      Sleep(1000);
      Console.WriteLine("Done");
    }

    private static void RequestOnce()
    {
      Console.WriteLine("Request once");
      foreach (var simVar in simVars)
      {
        Console.WriteLine("\t" + simVar);
        TypeId typeId = simVarIds[simVar];
        RequestId rid = eSimCon.Strings.Request(typeId);
        onceRequestId[rid] = simVar;
      }
    }

    private static void Register()
    {
      Console.WriteLine("Registering types");
      foreach (var simVar in simVars)
      {
        Console.WriteLine("\t" + simVar);
        simVarIds[simVar] = eSimCon.Strings.Register(simVar, ESimConnect.ESimConnect.StringsHandler.StringLength._128);
      }
    }

    private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      string relatedSimVar;
      if (onceRequestId.TryGetValue(e.RequestId, out relatedSimVar!) == false)
        relatedSimVar = "??-unrecognized-simvar-??";

      Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, simVar={relatedSimVar}, type={e.Type}, data={e.Data}");
    }
  }
}

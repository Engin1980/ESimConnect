using ESimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;

namespace ESimConnectTest
{
  internal class PrimitivesTest
  {
    private static ESimConnect.ESimConnect eSimCon = new();
    private static string[] simVars = new string[]
    {
      "PLANE LATITUDE",
      "PLANE LONGITUDE",
      "PLANE ALTITUDE"
    };
    private static Dictionary<string, int> simVarIds = new();
    private static Dictionary<int, string> onceRequestId = new();
    private static Dictionary<int, string> repeatedRequestId = new();

    public static void Run()
    {
      Console.WriteLine("Starting non-WPF");
      Open();
      Sleep(500);
      Register();
      RequestOnce();
      Sleep(3000);
      RequestRepeatedly();
      Sleep(3000);
      DeleteFirstRepeated();
      Sleep(3000);
      DeleteAllRepeated();
      Sleep(10000);
      Close();
      Sleep(1000);
      Console.WriteLine("Done");
    }

    private static void Close()
    {
      Console.WriteLine("Closing");
      eSimCon.Close();
    }

    private static void DeleteAllRepeated()
    {
      Console.WriteLine("Deleting all (except first&last) repeated");
      foreach (var simVar in simVars)
      {
        if (simVars[0] == simVar) continue;
        if (simVars.Last() == simVar) continue;
        Console.WriteLine("\t" + simVars[0]);
        int typeId = simVarIds[simVars[0]];
        eSimCon.Primitives.UnregisterPrimitive(typeId);
      }
    }

    private static void DeleteFirstRepeated()
    {
      Console.WriteLine("Deleting first repeated");
      Console.WriteLine("\t" + simVars[0]);
      int typeId = simVarIds[simVars[0]];
      eSimCon.Primitives.UnregisterPrimitive(typeId);
    }

    private static void RequestRepeatedly()
    {
      Console.WriteLine("Request repeatedly");
      foreach (var simVar in simVars)
      {
        Console.WriteLine("\t" + simVar);
        int typeId = simVarIds[simVar];
        eSimCon.Primitives.RequestPrimitiveRepeatedly(typeId, out int rid, SimConnectPeriod.SECOND, true);
        repeatedRequestId[rid] = simVar;
      }
    }

    private static void Sleep(int ms)
    {
      Console.WriteLine("Sleeping for " + ms + " ms");
      Thread.Sleep(ms);
    }

    private static void RequestOnce()
    {
      Console.WriteLine("Request once");
      foreach (var simVar in simVars)
      {
        Console.WriteLine("\t" + simVar);
        int typeId = simVarIds[simVar];
        eSimCon.Primitives.RequestPrimitive(typeId, out int rid);
        onceRequestId[rid] = simVar;
      }
    }

    private static void Register()
    {
      Console.WriteLine("Registering types");
      foreach (var simVar in simVars)
      {
        Console.WriteLine("\t" + simVar);
        simVarIds[simVar] = eSimCon.Primitives.RegisterPrimitive<double>(simVar);
      }
    }

    private static void Open()
    {
      Console.WriteLine("Opening");

      eSimCon.Connected += ESimCon_Connected;
      eSimCon.EventInvoked += ESimCon_EventInvoked;
      eSimCon.DataReceived += ESimCon_DataReceived;
      eSimCon.Disconnected += ESimCon_Disconnected;
      eSimCon.ThrowsException += ESimCon_ThrowsException;

      while (eSimCon.IsOpened == false)
      {
        try
        {
          eSimCon.Open();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          Sleep(500);
        }
      }
    }

    private static void ESimCon_ThrowsException(ESimConnect.ESimConnect sender, SimConnectException ex)
    {
      Console.WriteLine("ESimCon - ThrowsException - " + ex.ToString());
    }

    private static void ESimCon_Disconnected(ESimConnect.ESimConnect sender)
    {
      Console.WriteLine("ESimCon - Disconnected");
    }

    private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      string relatedSimVar;
      if (e.RequestId != null)
      {
        if (onceRequestId.TryGetValue(e.RequestId.Value, out relatedSimVar) == false)
          if (repeatedRequestId.TryGetValue(e.RequestId.Value, out relatedSimVar) == false)
            relatedSimVar = "??-unrecognized-simvar-??";
      }
      else
        relatedSimVar = "??-non-request-id-simvar-??";
      Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, simVar={relatedSimVar}, type={e.Type}, data={e.Data}");
    }

    private static void ESimCon_EventInvoked(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectEventInvokedEventArgs e)
    {
      Console.WriteLine($"ESimCon - Event invoked - event={e.Event}, requestId={e.RequestId}, value={e.Value}");
    }

    private static void ESimCon_Connected(ESimConnect.ESimConnect sender)
    {
      Console.WriteLine("ESimCon - Connected");
    }
  }
}

using ESimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnectTest
{
  internal class SharedFunctions
  {
    internal static void Sleep(int ms)
    {
      Console.WriteLine("Sleeping for " + ms + " ms");
      Thread.Sleep(ms);
    }
    internal static void Open(ESimConnect.ESimConnect eSimCon,
      ESimConnect.ESimConnect.ESimConnectDelegate connectDelegate,
      ESimConnect.ESimConnect.ESimConnectEventInvokedDelegate eventDelegate,
      ESimConnect.ESimConnect.ESimConnectDataReceivedDelegate dataDelegate,
      ESimConnect.ESimConnect.ESimConnectDelegate disconnectDelegate,
      ESimConnect.ESimConnect.ESimConnectExceptionDelegate exceptionDelegate)
    {
      Console.WriteLine("Opening");

      eSimCon.Connected += connectDelegate;
      eSimCon.EventInvoked += eventDelegate;
      eSimCon.DataReceived += dataDelegate;
      eSimCon.Disconnected += disconnectDelegate;
      eSimCon.ThrowsException += exceptionDelegate;

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

    internal static void Close(ESimConnect.ESimConnect eSimCon)
    {
      Console.WriteLine("Closing");
      eSimCon.Close();
    }

    internal static void ESimCon_ThrowsException(ESimConnect.ESimConnect _, SimConnectException ex)
    {
      Console.WriteLine("ESimCon - ThrowsException - " + ex.ToString());
    }

    internal static void ESimCon_Disconnected(ESimConnect.ESimConnect _)
    {
      Console.WriteLine("ESimCon - Disconnected");
    }

    internal static void ESimCon_EventInvoked(ESimConnect.ESimConnect _, ESimConnect.ESimConnect.ESimConnectEventInvokedEventArgs e)
    {
      Console.WriteLine($"ESimCon - Event invoked - event={e.Event}, requestId={e.RequestId}, value={e.Value}");
    }

    internal static void ESimCon_Connected(ESimConnect.ESimConnect _)
    {
      Console.WriteLine("ESimCon - Connected");
    }

    internal static void ESimCon_DataReceived(ESimConnect.ESimConnect _, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, type={e.Type}, data={e.Data}");
    }
  }
}

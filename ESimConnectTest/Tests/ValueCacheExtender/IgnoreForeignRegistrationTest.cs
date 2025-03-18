using ESimConnect;
using ESystem.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnectTest.Tests.ValueCacheExtender
{
  internal static class IgnoreForeignRegistrationTest
  {
    private static ESimConnect.ESimConnect eSimCon;
    private static ESimConnect.Extenders.OpenInBackgroundExtender openInBackgroundExtender;
    private static ESimConnect.Extenders.ValueCacheExtender valueCacheExtender;
    private static readonly Logger logger = Logger.Create(nameof(IgnoreForeignRegistrationTest));
    private static TypeId typeId;
    private static RequestId requestId;
    private static int dataCounter;
    private const int MAX_DATA_COUNTER = 10;
    internal static void Run()
    {
      logger.Log(LogLevel.INFO, "Starting");
      eSimCon = new ESimConnect.ESimConnect();
      eSimCon.DataReceived += ESimCon_DataReceived;

      openInBackgroundExtender = new(eSimCon);
      openInBackgroundExtender.Opened += OpenInBackgroundExtender_Opened;

      valueCacheExtender = new(eSimCon);
      valueCacheExtender.ValueChanged += ValueCacheExtender_ValueChanged;

      logger.Log(LogLevel.INFO, "Opening in background");
      openInBackgroundExtender.OpenInBackground();
      logger.Log(LogLevel.INFO, "Waiting for end.");

      while (dataCounter < MAX_DATA_COUNTER)
      {
        Thread.Sleep(1000);
      }
      logger.Log(LogLevel.INFO, "End");
    }

    private static void ValueCacheExtender_ValueChanged(ESimConnect.Extenders.ValueCacheExtender.ValueChangeEventArgs obj)
    {
      logger.Log(LogLevel.WARNING, "This should never be called as there is no value registerd over cache");
    }

    private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      if (e.RequestId == requestId)
      {
        logger.Log(LogLevel.INFO, "Correctly got data from eSimCon with value " + e.Data);
      }
      else
      {
        logger.Log(LogLevel.INFO, "Got data from foreign source with value " + e.Data);
      }
      dataCounter++;

      if (dataCounter > MAX_DATA_COUNTER)
      {
        logger.Log(LogLevel.INFO, "If not crashed, working fine.");
        logger.Log(LogLevel.INFO, "Closing");
        eSimCon.Close();
      }
    }

    private static void OpenInBackgroundExtender_Opened()
    {
      logger.Log(LogLevel.INFO, "Opened");

      typeId = eSimCon.Values.Register<double>(ESimConnect.Definitions.SimVars.Aircraft.Miscelaneous.ACCELERATION_BODY_X);
      requestId = eSimCon.Values.RequestRepeatedly(typeId, SimConnectPeriod.SECOND, sendOnlyOnChange:false);
    }
  }
}

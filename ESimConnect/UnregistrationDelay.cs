using ESystem.Exceptions;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
  public static class UnregistrationDelay
  {
    public static uint DefaultVisualFrameDelay { get; set; } = 50;
    public static uint DefaultSimFrameDelay { get; set; } = 50;
    public static uint DefaultNeverDelay { get; set; } = 0;
    public static uint DefaultOnceDelay { get; set; } = 50;
    public static uint DefaultSecondDelay { get; set; } = 1150;


    public static int Get(IEnumerable<SIMCONNECT_PERIOD> periods)
    {
      int ret = periods
        .Select(q => Get(q))
        .Select(q => (int?)q) // to return something when periods is empty 
        .Max() ?? 0;
      return ret;
    }

    public static int Get(SIMCONNECT_PERIOD period)
    {
      int ret = period switch
      {
        SIMCONNECT_PERIOD.VISUAL_FRAME => (int)DefaultVisualFrameDelay,
        SIMCONNECT_PERIOD.SIM_FRAME => (int)DefaultSimFrameDelay,
        SIMCONNECT_PERIOD.NEVER => (int)DefaultNeverDelay,
        SIMCONNECT_PERIOD.ONCE => (int)DefaultOnceDelay,
        SIMCONNECT_PERIOD.SECOND => (int)DefaultSecondDelay,
        _ => throw new UnexpectedEnumValueException(period),
      };
      return ret;
    }
  }
}

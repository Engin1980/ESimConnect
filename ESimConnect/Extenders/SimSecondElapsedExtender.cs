using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  public class SimSecondElapsedExtender : AbstractExtender
  {
    public bool IsSimPaused { get; set; }
    public event Action? SimSecondElapsed;
    private readonly bool invokeSimSecondEventsOnPause;

    public SimSecondElapsedExtender(ESimConnect eSimCon, bool invokeSimSecondEventsOnPause) : base(eSimCon)
    {
      EAssert.Argument.IsNotNull(eSimCon, nameof(eSimCon));
      base.eSimCon.SystemEventInvoked += SimCon_EventInvoked;
      this.invokeSimSecondEventsOnPause = invokeSimSecondEventsOnPause;
      if (eSimCon.IsOpened) RegisterEvents();
      else
        eSimCon.Connected += _ => RegisterEvents();
    }

    private void RegisterEvents()
    {
      eSimCon.SystemEvents.Register(Definitions.SimEvents.System.Pause);
      eSimCon.SystemEvents.Register(Definitions.SimEvents.System._1sec);
    }

    private void SimCon_EventInvoked(ESimConnect sender, ESimConnect.ESimConnectSystemEventInvokedEventArgs e)
    {
      if (e.Event == Definitions.SimEvents.System.Pause)
      {
        IsSimPaused = e.Value != 0;
      }
      else if (e.Event == Definitions.SimEvents.System._1sec && (!IsSimPaused || invokeSimSecondEventsOnPause))
      {
        SimSecondElapsed?.Invoke();
      }
    }
  }
}

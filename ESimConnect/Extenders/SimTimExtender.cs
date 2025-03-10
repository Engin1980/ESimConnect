using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  /// <summary>
  /// Provides event and states for Sim-Second-Elapsed and Paused.
  /// </summary>
  public class SimTimExtender : AbstractExtender
  {
    /// <summary>
    /// Returns True if sim is paused, false otherwise.
    /// </summary>
    public bool IsSimPaused { get; set; }
    /// <summary>
    /// Invoked on every Sim Second Elapsed.
    /// </summary>
    public event Action? SimSecondElapsed;

    /// <summary>
    /// Invoked on pause state change.
    /// </summary>
    public event Action<bool>? PauseChanged;

    private readonly bool invokeSimSecondEventsOnPause;

    /// <summary>
    /// Creates new instance
    /// </summary>
    /// <param name="eSimConnect">Underlying eSimConnect object.</param>
    /// <param name="invokeSimSecondEventsOnPause">True if SimSecond should be invoked on pause, false otherwise.</param>
    public SimTimExtender(ESimConnect eSimConnect, bool invokeSimSecondEventsOnPause) : base(eSimConnect)
    {
      EAssert.Argument.IsNotNull(eSimConnect, nameof(eSimConnect));
      base.eSimCon.SystemEventInvoked += SimCon_EventInvoked;
      this.invokeSimSecondEventsOnPause = invokeSimSecondEventsOnPause;
      if (eSimConnect.IsOpened) RegisterEvents();
      else
        eSimConnect.Connected += _ => RegisterEvents();
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
        PauseChanged?.Invoke(IsSimPaused);
      }
      else if (e.Event == Definitions.SimEvents.System._1sec && (!IsSimPaused || invokeSimSecondEventsOnPause))
      {
        SimSecondElapsed?.Invoke();
      }
    }
  }
}

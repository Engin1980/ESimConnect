using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ESimConnect.Extenders
{
  public class OpenInBackgroundExtender : AbstractExtender
  {
    private const int INITIAL_CONNECTION_DELAY = 1000;
    private const int REPEATED_CONNECTION_DELAY = 5000;

    private readonly int initialDelay;
    private readonly int repeatedDelay;
    private readonly Timer connectionTimer;

    public event Action? Opened = null!;
    public event Action<Exception>? OpeningAttemptFailed = null!;

    public bool IsOpened { get; private set; } = false;
    public bool IsOpening { get; private set; } = false;

    public OpenInBackgroundExtender(ESimConnect eSimCon, int initialDelayInMs = INITIAL_CONNECTION_DELAY, int repeatedAttemptDelayInMs = REPEATED_CONNECTION_DELAY) : base(eSimCon)
    {
      EAssert.Argument.IsTrue(initialDelayInMs >= 0, nameof(initialDelayInMs), "Must be non-negative int.");
      EAssert.Argument.IsTrue(repeatedAttemptDelayInMs > 0, nameof(repeatedAttemptDelayInMs), "Must be positive int.");

      this.initialDelay = initialDelayInMs;
      this.repeatedDelay = repeatedAttemptDelayInMs;
      connectionTimer = new()
      {
        AutoReset = false,
        Enabled = false
      };
      connectionTimer.Elapsed += ConnectionTimer_Elapsed;
    }

    private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
      try
      {
        this.eSimCon.Open();
        this.IsOpening = false;
        this.IsOpened = true;
      }
      catch (ESimConnectException ex)
      {
        OpeningAttemptFailed?.Invoke(ex);
        connectionTimer.Interval = this.repeatedDelay; // also resets timer countdown
        connectionTimer.Start();
      }
      catch (Exception ex)
      {
        var tmp = new ApplicationException("Unexpected exception when starting simcon on background.", ex);
        OpeningAttemptFailed?.Invoke(tmp);
        connectionTimer.Interval = this.repeatedDelay; // also resets timer countdown
        connectionTimer.Start();
      }

      if (this.IsOpened)
        Opened?.Invoke();
    }

    public void OpenInBackground()
    {
      if (this.IsOpened) return;
      lock (this)
      {
        if (this.IsOpening) return;
        this.IsOpening = true;
      }
      connectionTimer.Interval = this.initialDelay;
      connectionTimer.Start();
    }
  }
}

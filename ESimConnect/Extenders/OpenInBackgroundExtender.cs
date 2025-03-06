using ESystem.Asserting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ESimConnect.Extenders
{
  public class OpenInBackgroundExtender : AbstractExtender
  {
    private const int OPENING_STATE_UNSET = 0;
    private const int OPENING_STATE_OPENING = 1;
    private const int OPENING_STATE_OPENED = 2;
    private const int INITIAL_CONNECTION_DELAY = 1000;
    private const int REPEATED_CONNECTION_DELAY = 5000;

    private readonly int initialDelay;
    private readonly int repeatedDelay;
    private readonly Timer connectionTimer;
    private readonly HashSet<Action> onStartedActions = new();

    public event Action? Opened = null!;
    public event Action<Exception>? OpeningAttemptFailed = null!;

    private int openingStateFlag = OPENING_STATE_UNSET;
    public bool IsOpened { get => openingStateFlag == OPENING_STATE_OPENED; }
    public bool IsOpening { get => openingStateFlag == OPENING_STATE_OPENING; }

    public OpenInBackgroundExtender(
      ESimConnect eSimCon,
      int initialDelayInMs = INITIAL_CONNECTION_DELAY,
      int repeatedAttemptDelayInMs = REPEATED_CONNECTION_DELAY) : base(eSimCon)
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
        System.Threading.Interlocked.Exchange(ref this.openingStateFlag, OPENING_STATE_OPENED);
        this.eSimCon.Open();
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
      {
        lock (onStartedActions)
        {
          foreach (var item in onStartedActions)
          {
            Task t = new(item);
            t.Start();
          }
        }
        Opened?.Invoke();
      }
    }

    public void OpenInBackground(Action? onStarted)
    {
      AddOnStartedIfRequired(onStarted);

      if (this.openingStateFlag == OPENING_STATE_OPENED) return;

      int currentIsOpeningFlag = System.Threading.Interlocked.Exchange(ref this.openingStateFlag, OPENING_STATE_OPENING);
      if (currentIsOpeningFlag == OPENING_STATE_UNSET)
      {
        connectionTimer.Interval = this.initialDelay;
        connectionTimer.Start();
      }
    }

    private void AddOnStartedIfRequired(Action? onStarted)
    {
      if (onStarted == null) return;

      lock (this.onStartedActions)
      {
        if (IsOpened)
        {
          Task t = new(onStarted);
          t.Start();
        }
        else
          onStartedActions.Add(onStarted);
      }
    }
  }
}

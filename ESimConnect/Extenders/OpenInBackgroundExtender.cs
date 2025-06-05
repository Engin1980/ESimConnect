using ESystem.Asserting;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ESimConnect.Extenders
{
  /// <summary>
  /// Opens ESimConnect in background (in repeated attempts), invokes event once the opening is successfull.
  /// </summary>
  public class OpenInBackgroundExtender : AbstractExtender
  {
    /// <summary>
    /// Defines how the action is invoked when ESimConnect is opened.
    /// </summary>
    public enum OnOpenActionRepeatMode
    {
      /// <summary>
      /// Specifies whether the action should be invoked only once when ESimConnect is opened/connected for the first time.
      /// </summary>
      FirstOnly,
      /// <summary>
      /// Specifies whether the action should be invoked always when ESimConnect is opened/connected..
      /// </summary>
      Always
    }

    private class ThreadSafeHashSet<T> : IEnumerable<T>
    {
      private readonly HashSet<T> set = new HashSet<T>();
      private readonly object lck = new();
      public void Add(T item)
      {
        lock (lck)
        {
          set.Add(item);
        }
      }

      public void Clear()
      {
        lock (lck)
        {
          set.Clear();
        }
      }

      public IEnumerator<T> GetEnumerator()
      {
        HashSet<T> copy;
        
        lock(lck)
        {
          copy = new HashSet<T>(set);
        }

        return copy.GetEnumerator();
      }

      public void Remove(T item)
      {
        lock (lck)
        {
          set.Remove(item);
        }
      }

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private const int OPENING_STATE_UNSET = 0;
    private const int OPENING_STATE_OPENING = 1;
    private const int OPENING_STATE_OPENED = 2;
    private const int INITIAL_CONNECTION_DELAY = 1000;
    private const int REPEATED_CONNECTION_DELAY = 5000;

    private readonly int initialDelay;
    private readonly int repeatedDelay;
    private readonly Timer connectionTimer;
    private readonly ThreadSafeHashSet<Action> onConnectedAlwaysActions = new();
    private readonly ThreadSafeHashSet<Action> onConnectedOnceActions = new();

    /// <summary>
    /// Invoked when ESimConnect is opened.
    /// </summary>
    public event Action? Opened = null!;

    /// <summary>
    /// Invoked when one opening attempt did fail (will try to continue with other attempts.)
    /// </summary>
    public event Action<Exception>? OpeningAttemptFailed = null!;

    private int openingStateFlag = OPENING_STATE_UNSET;

    /// <summary>
    /// True if ESimConnect is opened.
    /// </summary>
    public bool IsOpened { get => openingStateFlag == OPENING_STATE_OPENED; }
    /// <summary>
    /// True if ESimConnect opening in background is in the progress.
    /// </summary>
    public bool IsOpening { get => openingStateFlag == OPENING_STATE_OPENING; }

    /// <summary>
    /// Creates a new instance
    /// </summary>
    /// <param name="eSimConnect">Underlying ESimConnect object</param>
    /// <param name="initialDelayInMs">Initial delay before first opening attempt.</param>
    /// <param name="repeatedAttemptDelayInMs">Delay after unsuccessful attempt.</param>
    public OpenInBackgroundExtender(
      ESimConnect eSimConnect,
      int initialDelayInMs = INITIAL_CONNECTION_DELAY,
      int repeatedAttemptDelayInMs = REPEATED_CONNECTION_DELAY) : base(eSimConnect)
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
        System.Threading.Interlocked.Exchange(ref this.openingStateFlag, OPENING_STATE_OPENED);
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
        DoAfterOpenActions();
        Opened?.Invoke();
      }
    }

    private void DoAfterOpenActions()
    {
      foreach (var action in onConnectedAlwaysActions)
      {
        Task t = new(action);
        t.Start();
      }
      foreach (var action in onConnectedOnceActions)
      {
        Task t = new(action);
        t.Start();
      }
      onConnectedOnceActions.Clear();
    }

    /// <summary>
    /// Invokes the specified action when the connection is established, based on the specified repeat mode.
    /// </summary>
    /// <remarks>If the connection is already established when this method is called, the action is executed
    /// immediately on a separate task. Otherwise, the action is queued to be executed based on the specified repeat
    /// mode.</remarks>
    /// <param name="action">The action to invoke when the connection is established. Cannot be <see langword="null"/>.</param>
    /// <param name="mode">Specifies the repeat mode for the action. Use <see cref="OnOpenActionRepeatMode.Always"/> to invoke the action
    /// every time the connection is established,  or <see cref="OnOpenActionRepeatMode.FirstOnly"/> to invoke the
    /// action only the first time the connection is established.</param>
    public void InvokeWhenConnected(Action action, OnOpenActionRepeatMode mode)
    {
      if (mode == OnOpenActionRepeatMode.Always)
        onConnectedAlwaysActions.Add(action);

      if (this.openingStateFlag == OPENING_STATE_OPENED)
      {
        Task t = new(action);
        t.Start();
      }
      else 
      {
        if (mode == OnOpenActionRepeatMode.FirstOnly)
        {
          onConnectedOnceActions.Add(action);
        }
      }
    }

    /// <summary>
    /// Starts connecting in background. Non-blocking call. If ESimConnect is opened, then event is invoked.
    /// </summary>
    public void OpenRepeatedlyUntilSuccess()
    {
      if (this.openingStateFlag == OPENING_STATE_OPENED) return;

      int currentIsOpeningFlag = System.Threading.Interlocked.Exchange(ref this.openingStateFlag, OPENING_STATE_OPENING);
      if (currentIsOpeningFlag == OPENING_STATE_UNSET)
      {
        connectionTimer.Interval = this.initialDelay;
        connectionTimer.Start();
      }
    }

    /// <summary>
    /// Starts connecting in background. Non-blocking call. If ESimConnect is opened, then event is invoked.
    /// </summary>
    /// <param name="onOpenedAction">Action to invoke when ESimConnect is opened.</param> -->
    /// <param name="mode">Mode of invoking action when ESimConnect is opened.</param>
    public void OpenRepeatedlyUntilSuccess(Action onOpenedAction, OnOpenActionRepeatMode mode)
    {
      InvokeWhenConnected(onOpenedAction, mode);
      OpenRepeatedlyUntilSuccess();
    }

  }
}

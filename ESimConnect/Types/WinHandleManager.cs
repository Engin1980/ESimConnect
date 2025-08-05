using ESystem.Asserting;
using ESystem.Logging;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace ESimConnect.Types
{
  internal class WinHandleManager : IDisposable
  {
    public delegate void ExceptionRaisedDelegate(Exception ex);
    public event ExceptionRaisedDelegate? ExceptionRaised;

    /// <summary>
    /// Predefined windows handler id to recognize requests from Simconnect. For more see API docs.
    /// </summary>
    public const int WM_USER_SIMCONNECT = 0x0402;

    private Window? window = null;
    private readonly object windowHandleLock = new object();
    private IntPtr windowHandle = IntPtr.Zero;
    private HwndSource? hwndSource = null;
    private HwndSourceHook? hook = null;
    private SimConnect? simConnect = null;
    private readonly Logger logger = Logger.Create(nameof(WinHandleManager));
    private readonly ESimConnect parent;
    private bool isParentDisconnected = false;
    public SimConnect? SimConnect { get => simConnect; set => simConnect = value; }


    public delegate void FsExitDetectedDelegate();
    public event FsExitDetectedDelegate? FsExitDetected;

    public IntPtr Handle
    {
      get
      {
        if (window == null)
          throw new InvalidRequestException("Cannot get win-handle when window is not created.");
        return windowHandle;
      }
    }

    public WinHandleManager(ESimConnect parent)
    {
      EAssert.Argument.IsNotNull(parent, nameof(parent));
      this.parent = parent;
      this.parent.Disconnected += (s) => this.isParentDisconnected = true;
    }

    public void AcquireIfRequired()
    {
      lock (this.windowHandleLock)
      {
        if (this.windowHandle == IntPtr.Zero)
        {
          logger.Log(LogLevel.TRACE, "AcquireIfRequired invoked. Window handle is not set. Acquiring...");
          Acquire();
        }
        else
        {
          logger.Log(LogLevel.TRACE, "AcquireIfRequired invoked. Window handle is already set: " + this.windowHandle.ToString("X8") + ". Reusing this handle.");
        }
      }
    }

    public void Acquire()
    {
      lock (this.windowHandleLock)
      {
        if (this.windowHandle != IntPtr.Zero)
          logger.Log(LogLevel.WARNING, "Acquire invoked. Window handle is already set: " + this.windowHandle.ToString("X8") + ". Release issues may occur.");
        logger.LogMethodStart();
        CreateWindow();
        this.hwndSource = HwndSource.FromHwnd(this.windowHandle);
        this.hook = new HwndSourceHook(DefWndProc);
        this.hwndSource.AddHook(this.hook);
        logger.LogMethodEnd();
        this.logger.Log(LogLevel.TRACE, "Acquire invoked. Window handle: " + this.windowHandle.ToString("X8"));
      }
    }

    protected IntPtr DefWndProc(IntPtr _hwnd, int msg, IntPtr _wParam, IntPtr _lParam, ref bool handled)
    {
      handled = false;

      if (msg == WM_USER_SIMCONNECT)
      {
        if (this.simConnect != null && this.hwndSource != null)
        {
          logger.Log(LogLevel.TRACE, "DefWndProc");
          if (this.isParentDisconnected)
          {
            logger.Log(LogLevel.WARNING, "Parent ESimConnect already disconnected. Ignoring message.");
            return (IntPtr)0;
          }
          else
          {
            try
            {
              this.simConnect.ReceiveMessage();
            }
            catch (Exception ex)
            {
              if (ex is System.Runtime.InteropServices.COMException && ex.Message == "0xC00000B0")
              {
                FsExitDetected?.Invoke();
              }
              else
              {
                string s = ExpandExceptionString(ex);
                logger.Log(LogLevel.ERROR, "DefWndProc EXCEPTION " + s);
                this.ExceptionRaised?.Invoke(ex);
              }
            }
            handled = true;
          }
        }
      }
      return (IntPtr)0;
    }

    private static string ExpandExceptionString(Exception? ex)
    {
      List<string> tmp = new();
      while (ex != null)
      {
        StringBuilder sb = new();
        sb.Append(ex.Message)
          .Append("\n\t")
          .Append(ex.StackTrace ?? "");
        tmp.Add(sb.ToString());
        ex = ex.InnerException;
      }
      string ret = string.Join("\n\n", tmp);
      return ret;
    }

    public void Release()
    {
      this.logger.Log(LogLevel.TRACE, "Release invoked.");
      logger.LogMethodStart();
      void destroyWindowHandle()
      {
        this.logger.Log(LogLevel.TRACE, "Destroying window handle invoked.");
        if (this.hwndSource != null)
        {
          this.hwndSource.RemoveHook(this.hook);
          this.hook = null;
          this.hwndSource = null;
        }

        if (this.window != null)
        {
          void closeAndShutdown()
          {
            this.logger.Log(LogLevel.TRACE, "Closing window and shutting down dispatcher invoked.");
            this.window.Close();
            this.window.Dispatcher.InvokeShutdown();
          }

          if (!this.window.Dispatcher.CheckAccess())
            this.window.Dispatcher.Invoke(closeAndShutdown);
          else
            closeAndShutdown();
        }
        this.window = null;
        this.windowHandle = IntPtr.Zero;
      }

      if (Application.Current == null)
        destroyWindowHandle();
      else
        Application.Current.Dispatcher.Invoke(destroyWindowHandle);

      while (this.windowHandle != IntPtr.Zero)
        Thread.Sleep(50);
      logger.LogMethodEnd();
    }

    private void CreateWindow()
    {
      this.logger.Log(LogLevel.TRACE, "Create Window invoked.");
      void createWindowHandle()
      {
        this.logger.Log(LogLevel.TRACE, "Creating window handle invoked.");
        this.windowHandle = Process.GetCurrentProcess().MainWindowHandle;
        var window = new Window();
        var wih = new WindowInteropHelper(window);
        wih.EnsureHandle();
        this.windowHandle = new WindowInteropHelper(window).Handle;
        this.window = window;
      }

      if (Application.Current == null)
      {
        Thread t = new(() =>
        {
          Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
          {
            createWindowHandle();
          }));
          this.logger.Log(LogLevel.TRACE, "Starting Dispatcher.Run on new thread.");
          Dispatcher.Run();
          this.logger.Log(LogLevel.TRACE, "Dispatcher.Run finished on new thread.");
        });
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
      }
      else
        Application.Current.Dispatcher.Invoke(() => createWindowHandle());

      while (this.window == null)
        Thread.Sleep(50);
    }

    public void Dispose()
    {
      Release();
    }
  }
}

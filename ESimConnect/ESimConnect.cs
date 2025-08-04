using ESystem.Logging;
using ESimConnect.Definitions;
using ESimConnect.Types;
using ESystem.Asserting;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media.TextFormatting;
using static ESystem.Functions.TryCatch;

namespace ESimConnect
{

  /// <summary>
  /// Wrapper for Ms.Simconnect object.
  /// </summary>
  public partial class ESimConnect : IDisposable
  {
    #region Classes + Structs

    public static class TypeSize<T>
    {
      #region Fields

      public readonly static int Size;

      #endregion Fields

      #region Constructors

      static TypeSize()
      {
        var dm = new DynamicMethod("SizeOfType", typeof(int), Array.Empty<Type>());
        ILGenerator il = dm.GetILGenerator();
        il.Emit(OpCodes.Sizeof, typeof(T));
        il.Emit(OpCodes.Ret);
        Size = (int)dm.Invoke(null, null)!;
      }

      #endregion Constructors
    }

    public class ESimConnectDataReceivedEventArgs
    {
      #region Properties

      /// <summary>
      /// Returned data
      /// </summary>
      public object Data { get; set; }

      /// <summary>
      /// Corresponding RequestId
      /// </summary>
      public RequestId RequestId { get; set; }

      /// <summary>
      /// Type of data
      /// </summary>
      public Type Type { get; set; }

      #endregion Properties

      #region Constructors

      public ESimConnectDataReceivedEventArgs(RequestId requestId, Type type, object data)
      {
        this.RequestId = requestId;
        this.Type = type ?? throw new ArgumentNullException(nameof(type));
        this.Data = data ?? throw new ArgumentNullException(nameof(data));
      }

      #endregion Constructors
    }

    public class ESimConnectSystemEventInvokedEventArgs
    {
      #region Properties

      /// <summary>
      /// Event name.
      /// </summary>
      public string Event { get; }

      /// <summary>
      /// Corresponding EventId
      /// </summary>
      public EventId EventId { get; }

      /// <summary>
      /// Value
      /// </summary>
      public uint Value { get; }

      #endregion Properties

      #region Constructors

      public ESimConnectSystemEventInvokedEventArgs(EventId eventId, string @event, uint value)
      {
        EventId = eventId;
        Event = @event;
        Value = value;
      }

      #endregion Constructors
    }

    #endregion Classes + Structs

    #region Delegates

    public delegate void ESimConnectDataReceivedDelegate(ESimConnect sender, ESimConnectDataReceivedEventArgs e);

    public delegate void ESimConnectDelegate(ESimConnect sender);

    public delegate void ESimConnectSystemEventInvokedDelegate(ESimConnect sender, ESimConnectSystemEventInvokedEventArgs e);

    public delegate void ESimConnectExceptionDelegate(ESimConnect sender, SimConnectException ex);

    #endregion Delegates

    #region Events

    /// <summary>
    /// Invoked when connected.
    /// </summary>
    public event ESimConnectDelegate? Connected;

    /// <summary>
    /// Invoked when data are received.
    /// </summary>
    /// <remarks>
    /// Some requests must be registered first to receive some data.
    /// </remarks>
    public event ESimConnectDataReceivedDelegate? DataReceived;

    /// <summary>
    /// Invoked when disconnected.
    /// </summary>
    public event ESimConnectDelegate? Disconnected;

    /// <summary>
    /// Invoked when system event occured.
    /// </summary>
    public event ESimConnectSystemEventInvokedDelegate? SystemEventInvoked;

    /// <summary>
    /// Invoked on any error received from SimConnect
    /// </summary>
    public event ESimConnectExceptionDelegate? ThrowsException;

    #endregion Events

    #region Fields

    private readonly RequestsManager requestDataManager = new();
    private readonly RequestExceptionManager requestExceptionManager = new();
    private readonly StructsHandler _Structs;
    private readonly SystemEventsHandler _SystemEvents;
    private readonly ValuesHandler _Values;
    private readonly StringsHandler _Strings;
    private readonly ClientEventsHandler _ClientEvents;
    private readonly WinHandleManager winHandleManager;
    private SimConnect? simConnect;
    private readonly Logger logger;
    #endregion Fields

    #region Properties

    /// <summary>
    /// Offers operations over structs.
    /// </summary>
    public StructsHandler Structs => _Structs;
    
    /// <summary>
    /// Offers operations over System (FS) Events.
    /// </summary>
    public SystemEventsHandler SystemEvents => _SystemEvents;
    
    /// <summary>
    /// Offers operations over SimVars.
    /// </summary>
    public ValuesHandler Values => _Values;

    /// <summary>
    /// Offers operations over Strings.
    /// </summary>
    public StringsHandler Strings => _Strings;

    /// <summary>
    /// Offers operation onver Client Events.
    /// </summary>
    public ClientEventsHandler ClientEvents => _ClientEvents;

    /// <summary>
    /// True if SimConnect is opened. False otherwise.
    /// </summary>
    public bool IsOpened { get => this.simConnect != null; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public ESimConnect()
    {
      logger = Logger.Create(nameof(ESimConnect));

      logger.LogMethodStart();

      this.winHandleManager = new(this);
      this.winHandleManager.FsExitDetected += (() => ResolveExitedFS2020());

      this._SystemEvents = new(this);
      this._Structs = new(this);
      this._ClientEvents = new(this);
      this._Values = new(this);
      this._Strings = new(this);

      logger.LogMethodEnd();
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Unregisters all definitions and close and dispose underlying simConnect object.
    /// </summary>
    public void Close()
    {
      logger.LogMethodStart();
      if (this.simConnect != null)
      {
        this.Structs.UnregisterAll();
        this.Values.UnregisterAll();
        this.Strings.UnregisterAll();
        this.SystemEvents.UnregisterAll();

        this.simConnect.Dispose();
        this.simConnect = null;
      }
      this.winHandleManager.Dispose();
      logger.LogMethodEnd();
    }


    public void Dispose()
    {
      logger.LogMethodStart();
      Close();
      logger.LogMethodEnd();
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Opens underlying simConnect object.
    /// </summary>
    public void Open()
    {
      logger.LogMethodStart();
      EnsureNotConnected();

      Try(
        () => winHandleManager.Acquire(),
        ex => new InternalException("Failed to register windows queue handler.", ex));

      Try(() =>
      {
        this.simConnect = new SimConnect("ESimConnect", winHandleManager.Handle, WinHandleManager.WM_USER_SIMCONNECT, null, 0);
        this.simConnect.OnRecvOpen += SimConnect_OnRecvOpen;
        this.simConnect.OnRecvQuit += SimConnect_OnRecvQuit;
        this.simConnect.OnRecvException += SimConnect_OnRecvException;
        this.simConnect.OnRecvSimobjectDataBytype += SimConnect_OnRecvSimobjectDataBytype;
        this.simConnect.OnRecvSimobjectData += SimConnect_OnRecvSimobjectData;
        this.simConnect.OnRecvEvent += SimConnect_OnRecvEvent;
        this.winHandleManager.SimConnect = this.simConnect;
      },
        ex => new ESimConnectException("Unable to open connection to FS2020.", ex));

      logger.LogMethodEnd();
    }

    private void EnsureConnected()
    {
      if (this.simConnect == null) throw new NotConnectedException();
    }

    private void EnsureNotConnected()
    {
      if (this.simConnect != null) throw new InvalidRequestException("SimConnect already opened.");
    }

    private void ResolveExitedFS2020()
    {
      if (this.simConnect != null)
      {
        this.simConnect.Dispose();
        this.simConnect = null;
      }
      this.Disconnected?.Invoke(this);
    }

    private void SimConnect_OnRecvEvent(SimConnect _, SIMCONNECT_RECV_EVENT data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvEvent), data);

      EventId eventId = new((int)data.uEventID);
      string eventName = this.SystemEvents.GetEventNameByEventId(eventId);
      uint value = data.dwData;

      ESimConnectSystemEventInvokedEventArgs e = new(eventId, eventName, value);
      this.SystemEventInvoked?.Invoke(this, e);
    }

    private void SimConnect_OnRecvException(SimConnect _, SIMCONNECT_RECV_EXCEPTION data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvException), data);
      SIMCONNECT_EXCEPTION ex = (SIMCONNECT_EXCEPTION)data.dwException;
      SimConnectException simConnectExceptionType = EnumConverter.Convert<SIMCONNECT_EXCEPTION, SimConnectException>(ex);
      ThrowsException?.Invoke(this, simConnectExceptionType);
    }

    private void SimConnect_OnRecvOpen(SimConnect _, SIMCONNECT_RECV_OPEN data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvOpen), data);
      this.Connected?.Invoke(this);
    }

    private void SimConnect_OnRecvQuit(SimConnect _, SIMCONNECT_RECV data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvQuit), data);
      this.Disconnected?.Invoke(this);
      this.ResolveExitedFS2020();
    }

    private void SimConnect_OnRecvSimobjectData(SimConnect _, SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvSimobjectData), data);

      RequestId requestId = new((int)data.dwRequestID);
      object content = data.dwData[0];
      var request = this.requestDataManager.GetByRequestId(requestId);

      ESimConnectDataReceivedEventArgs e = new(request.RequestId, request.Type, content);

      this.DataReceived?.Invoke(this, e);
    }

    private void SimConnect_OnRecvSimobjectDataBytype(SimConnect _, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvSimobjectDataBytype), data);

      RequestId requestId = new((int)data.dwRequestID);
      var request = this.requestDataManager.GetByRequestId(requestId);
      object content = data.dwData[0];
      content = this._Strings.UnpackValueIfIsStringContent(content);

      ESimConnectDataReceivedEventArgs e = new(request.RequestId, request.Type, content);
      this.DataReceived?.Invoke(this, e);
    }
    
    private static void ValidateSimVarName(string simVarName)
    {
      string unindexSimVarName(string simVarName)
      {
        string ret;
        int index = simVarName.IndexOf(':');
        ret = index < 0 ? simVarName : simVarName[..(index + 1)];
        return ret;
      }
      bool findSimVar(string simVarName, Type? cls = null)
      {
        bool ret;
        if (cls == null) cls = typeof(SimVars);

        ret = cls.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
          .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
          .Select(q => q.GetValue(null))
          .Any(q => Equals(q, simVarName));
        if (!ret)
        {
          var classes = cls.GetNestedTypes();
          foreach (var c in classes)
          {
            ret = findSimVar(simVarName, c);
            if (ret) break;
          }
        }

        return ret;
      }
      ;

      string tmp = unindexSimVarName(simVarName);
      bool exists = findSimVar(tmp);
      if (!exists)
      {
        throw new Exception($"SimVar '{simVarName}' check failed. SimVar not found in known values.");
      }
    }

    #endregion Methods
  }
}

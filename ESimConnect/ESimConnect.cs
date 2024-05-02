using ELogging;
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

namespace ESimConnect
{
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

      public object Data { get; set; }
      public RequestId RequestId { get; set; }
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

    public class ESimConnectEventInvokedEventArgs
    {
      #region Properties

      public string Event { get; }
      public EventId EventId { get; }
      public uint Value { get; }

      #endregion Properties

      #region Constructors

      public ESimConnectEventInvokedEventArgs(EventId eventId, string @event, uint value)
      {
        EventId = eventId;
        Event = @event;
        Value = value;
      }

      #endregion Constructors
    }

    #endregion Classes + Structs

    #region Delegates

    public delegate void ESimConnectDataReceivedDelegate(ESimConnect _, ESimConnectDataReceivedEventArgs e);

    public delegate void ESimConnectDelegate(ESimConnect _);

    public delegate void ESimConnectEventInvokedDelegate(ESimConnect _, ESimConnectEventInvokedEventArgs e);

    public delegate void ESimConnectExceptionDelegate(ESimConnect _, SimConnectException ex);

    #endregion Delegates

    #region Events

    public event ESimConnectDelegate? Connected;

    public event ESimConnectDataReceivedDelegate? DataReceived;

    public event ESimConnectDelegate? Disconnected;

    public event ESimConnectEventInvokedDelegate? EventInvoked;

    public event ESimConnectExceptionDelegate? ThrowsException;

    #endregion Events

    #region Fields

    // private const uint SIMCONNECT_CLIENT_DATA_REQUEST_FLAG_CHANGED = 0x00000001; // TODO delete if not used
    // private const uint SIMCONNECT_CLIENTDATAOFFSET_AUTO = uint.MaxValue;
    // private const uint SIMCONNECT_GROUP_PRIORITY_HIGHEST = 1;
    private readonly RequestsManager requestDataManager = new();
    private readonly RequestExceptionManager requestExceptionManager = new();
    private readonly StructsHandler _Structs;
    private readonly SystemEventsHandler _SystemEvents;
    private readonly ValuesHandler _Values;
    private readonly ClientEventsHandler _ClientEvents;
    private readonly WinHandleManager winHandleManager = new();
    private SimConnect? simConnect;
    private readonly Logger logger;
    #endregion Fields

    #region Properties

    public StructsHandler Structs => _Structs;
    public SystemEventsHandler SystemEvents => _SystemEvents;
    public ValuesHandler Values => _Values;
    public ClientEventsHandler ClientEvents => _ClientEvents;

    public bool IsOpened { get => this.simConnect != null; }

    #endregion Properties

    #region Constructors

    public ESimConnect()
    {
      logger = Logger.Create(nameof(ESimConnect));

      logger.LogMethodStart();

      winHandleManager.FsExitDetected += (() => ResolveExitedFS2020());

      this._SystemEvents = new(this);
      this._Structs = new(this);
      this._ClientEvents = new(this);
      this._Values = new(this);

      logger.LogMethodEnd();
    }

    #endregion Constructors

    #region Methods

    public void Close()
    {
      logger.LogMethodStart();
      if (this.simConnect != null)
      {
        this.Structs.UnregisterAll();
        this.Values.UnregisterAll();
        this.SystemEvents.UnregisterAll();

        this.winHandleManager.Dispose();

        this.simConnect.Dispose();
        this.simConnect = null;
      }
      logger.LogMethodEnd();
    }

    public void Dispose()
    {
      logger.LogMethodStart();
      Close();
      logger.LogMethodEnd();
      GC.SuppressFinalize(this);
    }

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
        ex => new InternalException("Unable to open connection to FS2020.", ex));

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

      ESimConnectEventInvokedEventArgs e = new(eventId, eventName, value);
      this.EventInvoked?.Invoke(this, e);
    }

    private void SimConnect_OnRecvException(SimConnect _, SIMCONNECT_RECV_EXCEPTION data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvException), data);
      SIMCONNECT_EXCEPTION ex = (SIMCONNECT_EXCEPTION)data.dwException;
      SimConnectException simConnectExceptionType = EnumConverter.ConvertEnum2<SIMCONNECT_EXCEPTION, SimConnectException>(ex);
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

      ESimConnectDataReceivedEventArgs e = new(request.RequestId, request.Type, content);
      this.DataReceived?.Invoke(this, e);
    }

    private void Try(Action tryAction, Func<Exception, Exception> exceptionProducer)
    {
      try
      {
        tryAction.Invoke();
      }
      catch (Exception ex)
      {
        Exception newException = exceptionProducer.Invoke(ex);
        logger.LogException(newException);
        throw newException;
      }
    }

    private T Try<T>(Func<T> tryFunc, Func<Exception, Exception> exceptionProducer)
    {
      T ret;
      try
      {
        ret = tryFunc.Invoke();
      }
      catch (Exception ex)
      {
        Exception newException = exceptionProducer.Invoke(ex);
        logger.LogException(newException);
        throw newException;
      }
      return ret;
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
      };

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

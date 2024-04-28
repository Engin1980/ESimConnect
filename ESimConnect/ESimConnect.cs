using ELogging;
using ESimConnect.Enumerations;
using ESimConnect.Types;
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

namespace ESimConnect
{
  public class ESimConnect : IDisposable
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
      public int? RequestId { get; set; }
      public Type Type { get; set; }

      #endregion Properties

      #region Constructors

      public ESimConnectDataReceivedEventArgs(int? requestId, Type type, object data)
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
      public int RequestId { get; }
      public uint Value { get; }

      #endregion Properties

      #region Constructors

      public ESimConnectEventInvokedEventArgs(int requestId, string @event, uint value)
      {
        RequestId = requestId;
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

    public abstract class BaseHandler
    {
      protected readonly ESimConnect parent;
      protected readonly ELogging.Logger logger;

      protected BaseHandler(ESimConnect parent)
      {
        this.parent = parent;
        this.logger = Logger.Create($"{nameof(ESimConnect)}+{this.GetType().Name[..^7]}");
      }
    }

    //TODO refactor methods to be Register/Request/Unregister
    public class StructsHandler : BaseHandler
    {
      private readonly TypeManager typeManager = new();

      public StructsHandler(ESimConnect parent) : base(parent)
      {
      }

      public int Register<T>(bool validate = false) where T : struct
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        EEnum eTypeId = IdProvider.GetNextAsEnum();
        int epsilon = 0;

        Type t = typeof(T);
        List<SanityHelpers.FieldMapInfo> fieldInfos = SanityHelpers.CheckAndDecodeFieldMappings(t);

        foreach (var fieldInfo in fieldInfos)
        {
          if (validate) parent.ValidateSimVarName(fieldInfo.Name);
          parent.Try(() =>
            this.parent.simConnect!.AddToDataDefinition(eTypeId,
              fieldInfo.Name, fieldInfo.Unit, fieldInfo.Type,
              epsilon, SimConnect.SIMCONNECT_UNUSED),
            ex => new InternalException("Failed to invoke 'simConnect.AddToDataDefinition(...)'.", ex));
        }

        parent.Try(() => this.parent.simConnect!.RegisterDataDefineStruct<T>(eTypeId),
          ex => new InternalException("Failed to invoke 'simConnect.RegisterDataDefineStruct<T>(...)'.", ex));
        this.typeManager.Register((int)eTypeId, t);
        logger.LogMethodEnd();

        return (int)eTypeId;
      }

      public void Request<T>(out int requestId)
      {
        uint radius = 0;
        requestId = IdProvider.GetNext();
        Request<T>(requestId, radius, SimConnectSimObjectType.USER);
      }

      public void Request<T>(out int requestId, uint radius, SimConnectSimObjectType simObjectType)
      {
        requestId = IdProvider.GetNext();
        Request<T>(requestId, radius, simObjectType);
      }

      public void Request<T>(int? customRequestId = null)
        => Request<T>(customRequestId, 0, SimConnectSimObjectType.USER);

      public void Request<T>(int? customRequestId, uint radius, SimConnectSimObjectType simObjectType)
      {
        logger.LogMethodStart(new object?[] { customRequestId, radius, simObjectType });
        parent.EnsureConnected();

        EEnum eTypeId = typeManager.GetIdAsEnum(typeof(T));
        EEnum eRequestId = IdProvider.GetNextAsEnum();
        SIMCONNECT_SIMOBJECT_TYPE simConObjectType = EnumConverter.ConvertEnum2<SimConnectSimObjectType, SIMCONNECT_SIMOBJECT_TYPE>(simObjectType);

        parent.Try(() => this.parent.simConnect!.RequestDataOnSimObjectType(eRequestId, eTypeId, radius, simConObjectType),
          ex => throw new InternalException("Failed to invoke 'RequestDataOnSimObjectType(...)'.", ex));
        this.parent.requestDataManager.Register(customRequestId, typeof(T), eRequestId);
        logger.LogMethodEnd();
      }

      public void RequestRepeatedly<T>(SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        RequestRepeatedly<T>(out int _, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
      }

      public void RequestRepeatedly<T>(out int requestId, SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        requestId = IdProvider.GetNext();
        RequestRepeatedly<T>(requestId, period, sendOnlyOnChange,
          initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
      }

      public void RequestRepeatedly<T>(
        int? customRequestId, SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        logger.LogMethodStart(new object?[] {
        customRequestId, period, sendOnlyOnChange, initialDelayFrames,
        skipBetweenFrames, numberOfReturnedFrames });
        if (this.parent.simConnect == null) throw new NotConnectedException();
        if (initialDelayFrames < 0) initialDelayFrames = 0;
        if (skipBetweenFrames < 0) skipBetweenFrames = 0;
        if (numberOfReturnedFrames < 0) numberOfReturnedFrames = 0;

        SIMCONNECT_DATA_REQUEST_FLAG flag = sendOnlyOnChange
          ? SIMCONNECT_DATA_REQUEST_FLAG.CHANGED
          : SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT;

        EEnum eTypeId = typeManager.GetIdAsEnum(typeof(T));
        EEnum eRequestId = IdProvider.GetNextAsEnum();
        SIMCONNECT_PERIOD simConPeriod = EnumConverter.ConvertEnum2<SimConnectPeriod, SIMCONNECT_PERIOD>(period);

        parent.Try(() =>
          this.parent.simConnect.RequestDataOnSimObject(
            eRequestId, eTypeId, SimConnect.SIMCONNECT_OBJECT_ID_USER, simConPeriod,
            flag, (uint)initialDelayFrames, (uint)skipBetweenFrames, (uint)numberOfReturnedFrames),
          ex => new InternalException($"Failed to invoke 'RequestDataOnSimObject(...)'.", ex));
        this.parent.requestDataManager.Register(customRequestId, typeof(T), eRequestId);
        logger.LogMethodEnd();
      }


      public void Unregister<T>()
      {
        Unregister(typeof(T));
      }

      public void Unregister(Type t)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        EEnum eTypeId = typeManager.GetIdAsEnum(t);

        parent.Try(
          () => this.parent.simConnect!.ClearDataDefinition(eTypeId),
          ex => new InternalException($"Failed to unregister type {t.Name}.", ex));
        this.typeManager.Unregister(t);
        logger.LogMethodEnd();
      }

      public void UnregisterAll()
      {
        var types = this.typeManager.GetRegisteredTypes();
        types.ForEach(q => Unregister(q));
      }
    }

    public class SystemEventsHandler : BaseHandler
    {
      private record EventIdName(EEnum EventId, string EventName);
      private readonly List<EventIdName> eventManager = new();

      public SystemEventsHandler(ESimConnect parent) : base(parent)
      {
      }

      public void Register(string eventName, out int eventId, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        if (validate) ValidateSystemEventName(eventName);

        EEnum eEventId;
        var tmp = this.eventManager.FirstOrDefault(q => q.EventName == eventName);
        if (tmp == null)
        {
          eEventId = IdProvider.GetNextAsEnum();
          parent.Try(() =>
          {
            this.parent.simConnect!.SubscribeToSystemEvent(eEventId, eventName);
            this.eventManager.Add(new EventIdName(eEventId, eventName));
          },
            ex => new InternalException($"Failed to register sim-event listener for '{eventName}'.", ex));
        }
        else
          eEventId = tmp.EventId;

        eventId = (int)eEventId;
        logger.LogMethodEnd();
      }

      public void Unregister(int eventId)
      {
        EEnum eEventId = (EEnum)eventId;
        parent.Try(() =>
        {
          this.parent.simConnect!.UnsubscribeFromSystemEvent(eEventId);
          this.eventManager
            .Where(q => (int)q.EventId == eventId)
            .ToList()
            .ForEach(q => this.eventManager.Remove(q));
        },
          ex => new InternalException($"Failed to unregister sim-event listener for event with id {eEventId}.", ex));

      }

      private static void ValidateSystemEventName(string eventName)
      {
        bool findEvent(string simVarName, Type? cls = null)
        {
          bool ret;
          if (cls == null) cls = typeof(SimEvents);

          ret = cls.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(q => q.GetValue(null))
            .Any(q => Equals(q, simVarName));
          if (!ret)
          {
            var classes = cls.GetNestedTypes();
            foreach (var c in classes)
            {
              ret = findEvent(simVarName, c);
              if (ret) break;
            }
          }

          return ret;
        };

        bool exists = findEvent(eventName);
        if (!exists)
        {
          throw new Exception($"SystemEvent '{eventName}' check failed. SystemEvent name not found in known values.");
        }
      }

      public void UnregisterAll()
      {
        var eventIds = eventManager.Select(q => q.EventId).Select(q => (int)q).ToList();
        eventIds.ForEach(q => Unregister(q));
      }

      internal string GetEventNameByEventId(EEnum eEventId)
      {
        string ret = eventManager.FirstOrDefault(q => q.EventId == eEventId)?.EventName ?? throw new ESimConnectException($"EventId={eEventId} is not registered.");
        return ret;
      }
    }

    public class ClientEventsHandler : BaseHandler
    {
      private readonly EEnum GROUP_ID_PRIORITY_STANDARD = (EEnum)1900000000;

      public ClientEventsHandler(ESimConnect parent) : base(parent)
      {
      }

      public void Invoke(string eventName, uint[]? parameters = null, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        parameters ??= Array.Empty<uint>();

        // up to 5 parameters available, but probably with a different .ddl version
        if (parameters.Length > 1) throw
            new NotImplementedException($"Maximum expected number of parameters is {1} (provided {parameters.Length}).");

        if (validate) Validate(eventName, parameters);

        EEnum eEvent = IdProvider.GetNextAsEnum();
        this.parent.simConnect!.MapClientEventToSimEvent(eEvent, eventName);

        uint val = parameters.Length == 0 ? 0 : parameters[0];
        this.parent.Try(() =>
          this.parent.simConnect.TransmitClientEvent(
          SimConnect.SIMCONNECT_OBJECT_ID_USER, eEvent, val, GROUP_ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY),
          ex => new InternalException($"Failed to invoke 'TransmitClientEvent(...)'", ex));
      }

      private static void Validate(string eventName, uint[] parameters)
      {
        FieldInfo? extractEventField(string eventName, Type? cls = null)
        {
          FieldInfo? ret;
          if (cls == null) cls = typeof(SimClientEvents);

          ret = cls.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .FirstOrDefault(q => q.Name == eventName);

          if (ret == null)
          {
            var classes = cls.GetNestedTypes();
            foreach (var c in classes)
            {
              ret = extractEventField(eventName, c);
              if (ret != null) break;
            }
          }
          return ret;
        };

        FieldInfo? eventField = extractEventField(eventName) ?? throw new Exception($"Event '{eventName}' not found in declarations.");

        var paramAttrs = eventField.GetCustomAttributes().Where(q => q is SimClientEvents.Parameter).Cast<SimClientEvents.Parameter>();
        if (paramAttrs.Count() != parameters.Length)
        {
          throw new Exception($"Event '{eventName}' parameter check failed. Expected {paramAttrs.Count()} params, provided {parameters.Length}.");
        }
      }
    }

    public class ValuesHandler : BaseHandler
    {
      private readonly PrimitiveManager primitiveManager = new();

      public ValuesHandler(ESimConnect parent) : base(parent)
      {
      }

      public int Register<T>(string simVarName, string unit = "Number", SimConnectSimTypeName simTypeName = SimConnectSimTypeName.FLOAT64,
        int epsilon = 0, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        SIMCONNECT_DATATYPE simType = EnumConverter.ConvertEnum2<SimConnectSimTypeName, SIMCONNECT_DATATYPE>(simTypeName);

        EEnum eTypeId = IdProvider.GetNextAsEnum();

        if (validate) parent.ValidateSimVarName(simVarName);

        parent.Try(
          () => parent.simConnect!.AddToDataDefinition(eTypeId, simVarName, unit, simType, epsilon, SimConnect.SIMCONNECT_UNUSED),
          ex => new InternalException("Failed to invoke 'simConnect.AddToDataDefinition(...)'.", ex));

        parent.Try(
          () => parent.simConnect!.RegisterDataDefineStruct<T>(eTypeId),
          ex => new InternalException("Failed to invoke 'simConnect.RegisterDataDefineStruct<T>(...)'.", ex));

        this.primitiveManager.Register((int)eTypeId, typeof(T));

        logger.LogMethodEnd();
        return (int)eTypeId;
      }

      public void Request(int typeId, out int requestId)
        => Request(typeId, 0, SimConnectSimObjectType.USER, out requestId);

      public void Request(int typeId, uint radius, SimConnectSimObjectType simObjectType, out int requestId)
      {
        requestId = IdProvider.GetNext();
        Request(typeId, requestId, radius, simObjectType);
      }

      public int Request(int typeId, int customRequestId)
        => Request(typeId, customRequestId, 0, SimConnectSimObjectType.USER);

      public int Request(int typeId, int customRequestId, uint radius, SimConnectSimObjectType simObjectType)
      {
        logger.LogMethodStart(new object?[] { typeId, customRequestId, radius });
        parent.EnsureConnected();
        this.EnsurePrimitiveTypeIdExists(typeId);

        SIMCONNECT_SIMOBJECT_TYPE sst = EnumConverter.ConvertEnum2<SimConnectSimObjectType, SIMCONNECT_SIMOBJECT_TYPE>(simObjectType);

        EEnum eTypeId = (EEnum)typeId;
        Type t = primitiveManager.GetType(typeId);
        EEnum eRequestId = IdProvider.GetNextAsEnum();
        parent.simConnect!.RequestDataOnSimObjectType(eRequestId, eTypeId, radius, sst);
        parent.requestDataManager.Register(customRequestId, t, eRequestId);
        logger.LogMethodEnd();
        return customRequestId;
      }

      public void RequestRepeatedly(int typeId, out int requestId, SimConnectPeriod period, bool sendOnlyOnChange = true,
                  int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        requestId = IdProvider.GetNext();
        RequestRepeatedly(typeId, requestId, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
      }

      public void RequestRepeatedly(int typeId, int customRequestId, SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        logger.LogMethodStart(new object?[] {
        customRequestId, period, sendOnlyOnChange, initialDelayFrames,
        skipBetweenFrames, numberOfReturnedFrames });
        if (parent.simConnect == null) throw new NotConnectedException();
        if (initialDelayFrames < 0) initialDelayFrames = 0;
        if (skipBetweenFrames < 0) skipBetweenFrames = 0;
        if (numberOfReturnedFrames < 0) numberOfReturnedFrames = 0;

        SIMCONNECT_DATA_REQUEST_FLAG flag = sendOnlyOnChange
          ? SIMCONNECT_DATA_REQUEST_FLAG.CHANGED
          : SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT;

        this.EnsurePrimitiveTypeIdExists(typeId);
        EEnum eTypeId = (EEnum)typeId;
        Type type = this.primitiveManager.GetType(typeId);
        EEnum eRequestId = IdProvider.GetNextAsEnum();

        SIMCONNECT_PERIOD simPeriod = EnumConverter.ConvertEnum2<SimConnectPeriod, SIMCONNECT_PERIOD>(period);

        parent.Try(() =>
          parent.simConnect.RequestDataOnSimObject(
            eRequestId, eTypeId, SimConnect.SIMCONNECT_OBJECT_ID_USER, simPeriod,
            flag, (uint)initialDelayFrames, (uint)skipBetweenFrames, (uint)numberOfReturnedFrames),
          ex => new InternalException($"Failed to invoke 'RequestDataOnSimObject(...)'.", ex));
        parent.requestDataManager.Register(customRequestId, type, eRequestId);
        logger.LogMethodEnd();
      }

      public void Send<T>(int typeId, T value)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (!this.primitiveManager.IsRegistered(typeId))
          throw new ApplicationException($"Primitive type with id {typeId} not registered.");
        Type expectedType = this.primitiveManager.GetType(typeId);
        if (value.GetType().Equals(expectedType) == false)
          throw new ApplicationException($"Primitive type should be {expectedType.Name}, but provided value {value} is {value.GetType().Name}.");

        EEnum eTypeId = (EEnum)typeId;
        parent.simConnect!.SetDataOnSimObject(eTypeId, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, value);

        logger.LogMethodEnd();
      }

      public void Unregister(int typeId)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        EEnum eTypeId = (EEnum)typeId;

        parent.Try(() => parent.simConnect!.ClearDataDefinition(eTypeId),
          ex => new InternalException($"Failed to unregister typeId {typeId}.", ex));
        this.primitiveManager.Unregister(typeId);
        logger.LogMethodEnd();
      }

      public void UnregisterAll()
      {
        var primitiveTypeIds = primitiveManager.GetRegisteredTypesIds();
        primitiveTypeIds.ForEach(q => Unregister(q));
      }

      private void EnsurePrimitiveTypeIdExists(int typeId)
      {
        if (!this.primitiveManager.IsRegistered(typeId))
          throw new Exception($"Primitive typeId={typeId} is not registered.");
      }
    }

    #region Fields

    // private const uint SIMCONNECT_CLIENT_DATA_REQUEST_FLAG_CHANGED = 0x00000001; // TODO delete if not used
    // private const uint SIMCONNECT_CLIENTDATAOFFSET_AUTO = uint.MaxValue;
    // private const uint SIMCONNECT_GROUP_PRIORITY_HIGHEST = 1;
    private readonly RequestDataManager requestDataManager = new();
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

      EEnum eEventId = (EEnum)data.uEventID;
      string eventName = this.SystemEvents.GetEventNameByEventId(eEventId);
      uint value = data.dwData;

      ESimConnectEventInvokedEventArgs e = new((int)eEventId, eventName, value);
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
      EEnum iRequest = (EEnum)data.dwRequestID;
      object ret = data.dwData[0];
      requestDataManager.Recall(iRequest, out Type type, out int? userRequestId);
      ESimConnectDataReceivedEventArgs e = new(userRequestId, type, ret);
      this.DataReceived?.Invoke(this, e);
    }

    private void SimConnect_OnRecvSimobjectDataBytype(SimConnect _, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
    {
      logger.LogObject("Event " + nameof(SimConnect_OnRecvSimobjectDataBytype), data);

      EEnum iRequest = (EEnum)data.dwRequestID;
      requestDataManager.Recall(iRequest, out Type type, out int? userRequestId);
      object ret = data.dwData[0];

      ESimConnectDataReceivedEventArgs e = new(userRequestId, type, ret);
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

    private void ValidateSimVarName(string simVarName)
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

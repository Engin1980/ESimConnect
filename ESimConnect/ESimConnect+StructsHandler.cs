using ESimConnect.Types;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static ESimConnect.Types.RequestsManager;
using static ESystem.Functions.TryCatch;

namespace ESimConnect
{
  public partial class ESimConnect
  {
    /// <summary>
    /// Offers operations over structs.
    /// </summary>
    public class StructsHandler : BaseHandler
    {
      /// <summary>
      /// Default radius. As typical requests are related to the current user/plane, default value is 0.
      /// </summary>
      public const uint DEFAULT_RADIUS = 0;
      /// <summary>
      /// Default sim object type. As typical request are related to the current user/plane, default value is USER.
      /// </summary>
      public const SimConnectSimObjectType DEFAULT_SIMOBJECT_TYPE = SimConnectSimObjectType.USER;
      private readonly TypeManager typeManager = new();

      internal StructsHandler(ESimConnect parent) : base(parent)
      {
      }

      /// <summary>
      /// Registers a type with fields to get values from FS.
      /// </summary>
      /// <typeparam name="T">Registerd type</typeparam>
      /// <param name="validate">True if SimVar names should be validated.</param>
      /// <returns>TypeId uniquely referring registered type.</returns>
      public TypeId Register<T>(bool validate = false) where T : struct
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        TypeId typeId = TypeId.Next();
        int epsilon = 0;

        Type t = typeof(T);
        List<SanityHelpers.FieldMapInfo> fieldInfos = SanityHelpers.CheckAndDecodeFieldMappings(t);

        foreach (var fieldInfo in fieldInfos)
        {
          if (validate) ValidateSimVarName(fieldInfo.Name);
          Try(() =>
            this.parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(),
              fieldInfo.Name, fieldInfo.Unit, fieldInfo.Type,
              epsilon, SimConnect.SIMCONNECT_UNUSED),
            ex => new InternalException("Failed to invoke 'simConnect.AddToDataDefinition(...)'.", ex));
        }

        Try(() => this.parent.simConnect!.RegisterDataDefineStruct<T>(typeId.ToEEnum()),
          ex => new InternalException("Failed to invoke 'simConnect.RegisterDataDefineStruct<T>(...)'.", ex));
        this.typeManager.Register(typeId, t);
        logger.LogMethodEnd();

        return typeId;
      }

      /// <summary>
      /// Requests a data w.r.t. the registered type
      /// </summary>
      /// <typeparam name="T">Target type, must be already registered with assigned TypeId.</typeparam>
      /// <param name="radius">Radius of data request.</param>
      /// <param name="simObjectType">SimObject related to the requested data.</param>
      /// <returns>RequestId uniquely describing the request. Data are returned via event with the same RequestId.</returns>
      /// <exception cref="InternalException">If any issue occurs.</exception>
      public RequestId Request<T>(uint radius = DEFAULT_RADIUS, SimConnectSimObjectType simObjectType = DEFAULT_SIMOBJECT_TYPE)
      {
        TypeId typeId = typeManager.GetByTypeSingle(typeof(T));
        RequestId ret = Request(typeId, radius, simObjectType);
        return ret;
      }

      /// <summary>
      /// Requests a data w.r.t. the registered type
      /// </summary>
      /// <param name="typeId">Previously registered TypeId</param>
      /// <param name="radius">Radius of data request.</param>
      /// <param name="simObjectType">SimObject realted to the requested data.</param>
      /// <returns>RequestId uniquely describing the request. Data are returned via event with the same RequestId.</returns>
      /// <exception cref="InternalException">If any issue occurs.</exception>
      public RequestId Request(TypeId typeId, uint radius = DEFAULT_RADIUS, SimConnectSimObjectType simObjectType = DEFAULT_SIMOBJECT_TYPE)
      {
        logger.LogMethodStart(new object?[] { radius, simObjectType });
        parent.EnsureConnected();

        RequestId requestId = RequestId.Next();
        Type type = typeManager[typeId];
        SIMCONNECT_SIMOBJECT_TYPE simConObjectType = EnumConverter.Convert<SimConnectSimObjectType, SIMCONNECT_SIMOBJECT_TYPE>(simObjectType);

        Try(() => this.parent.simConnect!.RequestDataOnSimObjectType(requestId.ToEEnum(), typeId.ToEEnum(), radius, simConObjectType),
          ex => throw new InternalException("Failed to invoke 'RequestDataOnSimObjectType(...)'.", ex));

        this.parent.requestDataManager.RegisterRequest(requestId, type, typeId, KindOfTypeId.STRUCT);

        logger.LogMethodEnd();
        return requestId;
      }

      public RequestId RequestRepeatedly<T>(
        SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        var ret = RequestId.Next();
        RequestRepeatedly<T>(ret, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
        return ret;
      }
      public void RequestRepeatedly<T>(RequestId requestId,
        SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        var typeId = typeManager.GetByTypeSingle(typeof(T));
        RequestRepeatedly(requestId, typeId, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
      }
      public RequestId RequestRepeatedly(TypeId typeId,
        SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        var ret = RequestId.Next();
        RequestRepeatedly(ret, typeId, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
        return ret;
      }

      public void RequestRepeatedly(RequestId requestId, TypeId typeId,
        SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        logger.LogMethodStart(new object?[] {
          requestId, period, sendOnlyOnChange, initialDelayFrames,
          skipBetweenFrames, numberOfReturnedFrames });
        if (this.parent.simConnect == null) throw new NotConnectedException();
        if (initialDelayFrames < 0) initialDelayFrames = 0;
        if (skipBetweenFrames < 0) skipBetweenFrames = 0;
        if (numberOfReturnedFrames < 0) numberOfReturnedFrames = 0;

        SIMCONNECT_DATA_REQUEST_FLAG flag = sendOnlyOnChange
          ? SIMCONNECT_DATA_REQUEST_FLAG.CHANGED
          : SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT;

        Type type = typeManager[typeId];

        SIMCONNECT_PERIOD simConPeriod = EnumConverter.Convert<SimConnectPeriod, SIMCONNECT_PERIOD>(period);

        Try(() =>
          this.parent.simConnect.RequestDataOnSimObject(
            requestId.ToEEnum(), typeId.ToEEnum(), SimConnect.SIMCONNECT_OBJECT_ID_USER, simConPeriod,
            flag, (uint)initialDelayFrames, (uint)skipBetweenFrames, (uint)numberOfReturnedFrames),
          ex => new InternalException($"Failed to invoke 'RequestDataOnSimObject(...)'.", ex));

        this.parent.requestDataManager.RegisterRepeatedlyRequest(requestId, type, typeId, KindOfTypeId.STRUCT, simConPeriod);

        logger.LogMethodEnd();
      }

      public void Unregister<T>(int? unergistrationDelayMs = null)
      {
        logger.LogMethodStart();
        var typeId = typeManager.GetByTypeSingle(typeof(T));
        UnregisterInternal(new List<TypeId>() { typeId }, unergistrationDelayMs);
        logger.LogMethodEnd();
      }

      public void Unregister(TypeId typeId, int? unergistrationDelayMs = null)
      {
        logger.LogMethodStart();
        UnregisterInternal(new List<TypeId>() { typeId }, unergistrationDelayMs);
        logger.LogMethodEnd();
      }

      public void UnregisterAll(int? unergistrationDelayMs = null)
      {
        logger.LogMethodStart();
        UnregisterInternal(typeManager.GetAllTypeIds().ToList(), unergistrationDelayMs);
        logger.LogMethodEnd();
      }

      private void UnregisterInternal(List<TypeId> typeIds, int? unergistrationDelayMs)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        IEnumerable<Request> requests = parent.requestDataManager.GetAll()
          .Where(q => q.Kind == KindOfTypeId.STRUCT && typeIds.Contains(q.TypeId));

        var periodicalRequests = requests.Where(q => q.Period != null);
        periodicalRequests = periodicalRequests.Where(q => q.Period != SIMCONNECT_PERIOD.NEVER); // unable twice call NEVER (will throw SimConException)
        if (periodicalRequests.Any())
        {
          foreach (var request in periodicalRequests)
            this.RequestRepeatedly(request.RequestId, request.TypeId, SimConnectPeriod.NEVER);

          unergistrationDelayMs ??= UnregistrationDelay.Get(periodicalRequests.Select(q => q.Period!.Value));
          Thread.Sleep(unergistrationDelayMs.Value); //TODO rewrite to following thread invocation after a while to be non-blocking
        }

        foreach (var typeId in typeIds)
        {
          Try(
          () => this.parent.simConnect!.ClearDataDefinition(typeId.ToEEnum()),
          ex => new InternalException($"Failed to unregister typeId '{typeId}' / type '{typeManager[typeId].FullName}'.", ex));
        }

        foreach (var request in requests)
          this.parent.requestDataManager.Unregister(request.RequestId);
        foreach (var typeId in typeIds)
          this.typeManager.Unregister(typeId);

        logger.LogMethodEnd();
      }
    }
  }
}

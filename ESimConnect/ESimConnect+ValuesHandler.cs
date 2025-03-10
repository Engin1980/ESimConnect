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
    public class ValuesHandler : BaseHandler
    {
      private const uint DEFAULT_RADIUS = 0;
      private const SimConnectSimObjectType DEFAULT_SIMOBJECT_TYPE = SimConnectSimObjectType.USER;
      private readonly TypeManager typeManager = new();

      internal ValuesHandler(ESimConnect parent) : base(parent)
      {
      }

      public TypeId Register<T>(string simVarName, string unit = "Number", SimConnectSimTypeName simTypeName = SimConnectSimTypeName.FLOAT64,
        int epsilon = 0, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        SIMCONNECT_DATATYPE simType = EnumConverter.Convert<SimConnectSimTypeName, SIMCONNECT_DATATYPE>(simTypeName);

        TypeId typeId = TypeId.Next();

        if (validate) ValidateSimVarName(simVarName);

        Try(
          () => parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(), simVarName, unit, simType, epsilon, SimConnect.SIMCONNECT_UNUSED),
          ex => new InternalException("Failed to invoke 'simConnect.AddToDataDefinition(...)'.", ex));

        Try(
          () => parent.simConnect!.RegisterDataDefineStruct<T>(typeId.ToEEnum()),
          ex => new InternalException("Failed to invoke 'simConnect.RegisterDataDefineStruct<T>(...)'.", ex));

        this.typeManager.Register(typeId, typeof(T));

        logger.LogMethodEnd();
        return typeId;
      }

      public RequestId Request(TypeId typeId, uint radius = DEFAULT_RADIUS, SimConnectSimObjectType simObjectType = DEFAULT_SIMOBJECT_TYPE)
      {
        logger.LogMethodStart(new object?[] { typeId, radius, simObjectType });
        parent.EnsureConnected();

        SIMCONNECT_SIMOBJECT_TYPE sst = EnumConverter.Convert<SimConnectSimObjectType, SIMCONNECT_SIMOBJECT_TYPE>(simObjectType);

        Type t = typeManager[typeId];
        RequestId requestId = RequestId.Next();
        parent.simConnect!.RequestDataOnSimObjectType(requestId.ToEEnum(), typeId.ToEEnum(), radius, sst);
        parent.requestDataManager.RegisterRequest(requestId, t, typeId, KindOfTypeId.VALUE);
        logger.LogMethodEnd();
        return requestId;
      }

      public RequestId RequestRepeatedly(TypeId typeId, SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        RequestId requestId = RequestId.Next();
        RequestRepeatedly(requestId, typeId, period, sendOnlyOnChange, initialDelayFrames, skipBetweenFrames, numberOfReturnedFrames);
        return requestId;
      }

      public void RequestRepeatedly(RequestId requestId, TypeId typeId,
        SimConnectPeriod period, bool sendOnlyOnChange = true,
        int initialDelayFrames = 0, int skipBetweenFrames = 0, int numberOfReturnedFrames = 0)
      {
        logger.LogMethodStart(new object?[] {
          typeId, period, sendOnlyOnChange, initialDelayFrames,
          skipBetweenFrames, numberOfReturnedFrames });
        if (parent.simConnect == null) throw new NotConnectedException();
        if (initialDelayFrames < 0) initialDelayFrames = 0;
        if (skipBetweenFrames < 0) skipBetweenFrames = 0;
        if (numberOfReturnedFrames < 0) numberOfReturnedFrames = 0;

        SIMCONNECT_DATA_REQUEST_FLAG flag = sendOnlyOnChange
          ? SIMCONNECT_DATA_REQUEST_FLAG.CHANGED
          : SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT;

        Type type = this.typeManager[typeId];

        SIMCONNECT_PERIOD simPeriod = EnumConverter.Convert<SimConnectPeriod, SIMCONNECT_PERIOD>(period);

        Try(() =>
          parent.simConnect.RequestDataOnSimObject(
            requestId.ToEEnum(), typeId.ToEEnum(), SimConnect.SIMCONNECT_OBJECT_ID_USER, simPeriod,
            flag, (uint)initialDelayFrames, (uint)skipBetweenFrames, (uint)numberOfReturnedFrames),
          ex => new InternalException($"Failed to invoke 'RequestDataOnSimObject(...)'.", ex));
        parent.requestDataManager.RegisterRepeatedlyRequest(requestId, type, typeId, KindOfTypeId.VALUE, simPeriod);

        logger.LogMethodEnd();
      }


      public void Send<T>(TypeId typeId, T value)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();
        if (value == null) throw new ArgumentNullException(nameof(value));

        Type expectedType = this.typeManager[typeId];
        if (value.GetType().Equals(expectedType) == false)
          throw new ApplicationException($"Primitive type should be {expectedType.Name}, but provided value {value} is {value.GetType().Name}.");

        parent.simConnect!.SetDataOnSimObject(typeId.ToEEnum(), SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, value);

        logger.LogMethodEnd();
      }

      public void Unregister(TypeId typeId, int? unergistrationDelayMs = null)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        UnregisterInternal(new List<TypeId>() { typeId }, unergistrationDelayMs);
        logger.LogMethodEnd();
      }

      private void UnregisterInternal(List<TypeId> typeIds, int? unergistrationDelayMs)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        IEnumerable<Request> requests = parent.requestDataManager.GetAll()
          .Where(q => q.Kind == KindOfTypeId.VALUE && typeIds.Contains(q.TypeId));

        var periodicalRequests = requests.Where(q => q.Period != null);
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
          ex => new InternalException($"Failed to unregister value-typeId '{typeId}' / type '{typeManager[typeId].FullName}'.", ex));
        }

        foreach (var request in requests)
          this.parent.requestDataManager.Unregister(request.RequestId);
        foreach (var typeId in typeIds)
          this.typeManager.Unregister(typeId);

        logger.LogMethodEnd();
      }

      public void UnregisterAll(int? unergistrationDelayMs = null)
      {
        var primitiveTypeIds = typeManager.GetAllTypeIds().ToList();
        UnregisterInternal(primitiveTypeIds, unergistrationDelayMs);
      }
    }
  }
}

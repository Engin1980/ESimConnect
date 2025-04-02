﻿using ESimConnect.Definitions;
using ESimConnect.Types;
using ESystem.Logging;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESystem.Exceptions;
using static ESimConnect.ESimConnect;
using static ESimConnect.Types.RequestsManager;
using static ESystem.Functions.TryCatch;

namespace ESimConnect
{
  public partial class ESimConnect
  {
    public class StringsHandler : BaseHandler
    {
      public enum StringLength
      {
        _8,
        _32,
        _128,
        _260
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
      private struct String8Struct
      {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string value;
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
      private struct String32Struct
      {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string value;
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
      private struct String128Struct
      {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string value;
      }

      [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
      private struct String260Struct
      {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string value;
      }

      private const uint DEFAULT_RADIUS = 0;
      private const SimConnectSimObjectType DEFAULT_SIMOBJECT_TYPE = SimConnectSimObjectType.USER;
      private readonly TypeManager typeManager = new();

      internal StringsHandler(ESimConnect parent) : base(parent)
      {
      }

      public TypeId Register(string simVarName, StringLength length, int epsilon = 0, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        if (validate) ValidateSimVarName(simVarName);

        TypeId typeId = TypeId.Next();

        Action dataDef;
        Action structDef;
        Type stringType;

        switch (length)
        {
          case StringLength._8:
            dataDef = () => this.parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(),
              simVarName, "", SIMCONNECT_DATATYPE.STRING8, 0, SimConnect.SIMCONNECT_UNUSED);
            structDef = () => this.parent.simConnect!.RegisterDataDefineStruct<String8Struct>(typeId.ToEEnum());
            stringType = typeof(String8Struct);
            break;
          case StringLength._32:
            dataDef = () => this.parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(),
              simVarName, "", SIMCONNECT_DATATYPE.STRING32, 0, SimConnect.SIMCONNECT_UNUSED);
            structDef = () => this.parent.simConnect!.RegisterDataDefineStruct<String32Struct>(typeId.ToEEnum());
            stringType = typeof(String32Struct);
            break;
          case StringLength._128:
            dataDef = () => this.parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(),
              simVarName, "", SIMCONNECT_DATATYPE.STRING128, 0, SimConnect.SIMCONNECT_UNUSED);
            structDef = () => this.parent.simConnect!.RegisterDataDefineStruct<String128Struct>(typeId.ToEEnum());
            stringType = typeof(String128Struct);
            break;
          case StringLength._260:
            dataDef = () => this.parent.simConnect!.AddToDataDefinition(typeId.ToEEnum(),
            simVarName, "", SIMCONNECT_DATATYPE.STRING260, 0, SimConnect.SIMCONNECT_UNUSED);
            structDef = () => this.parent.simConnect!.RegisterDataDefineStruct<String260Struct>(typeId.ToEEnum());
            stringType = typeof(String260Struct);
            break;
          default:
            throw new UnexpectedEnumValueException(length);
        }

        Try(dataDef,
            ex => new InternalException("Failed to invoke 'simConnect.AddToDataDefinition(...)'.", ex));

        Try(structDef,
          ex => new InternalException("Failed to invoke 'simConnect.RegisterDataDefineStruct<T>(...)'.", ex));

        this.typeManager.Register(typeId, stringType);
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

      internal object UnpackValueIfIsStringContent(object content)
      {
        object ret;
        if (content is String8Struct s8)
          ret = s8.value;
        else if (content is String32Struct s32)
          ret = s32.value;
        else if (content is String128Struct s128)
          ret = s128.value;
        else if (content is String260Struct s260)
          ret = s260.value;
        else
          ret = content;
        return ret;
      }
    }
  }
}

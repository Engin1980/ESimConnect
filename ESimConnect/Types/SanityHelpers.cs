using ESimConnect.Definitions;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
    internal static class SanityHelpers
  {
    private readonly static Dictionary<Type, SIMCONNECT_DATATYPE> typeMapping;
    private readonly static Dictionary<int, SIMCONNECT_DATATYPE> typeStringMapping;

    public class FieldMapInfo
    {
      public FieldInfo Field { get; set; }
      public string Name { get; set; }
      public string? Unit { get; set; }
      public SIMCONNECT_DATATYPE Type { get; set; }
    }

    public static List<SanityHelpers.FieldMapInfo> CheckAndDecodeFieldMappings(Type t)
    {
      SanityHelpers.EnsureTypeHasRequiredAttribute(t);
      var fields = t.GetFields();

      var fieldInfos = fields
        .OrderBy(f => Marshal.OffsetOf(t, f.Name).ToInt32())
        .Select(q => new { Field = q, Attribute = SanityHelpers.GetDataDefinitionAttributeOrThrowException(q) })
        .Select(q => new SanityHelpers.FieldMapInfo
        {
          Field = q.Field,
          Name = q.Attribute.Name,
          Unit = q.Attribute.Unit,
          Type = SanityHelpers.ResolveAttributeType(q.Field, q.Attribute)
        })
        .ToList();

      fieldInfos.ForEach(q => SanityHelpers.EnsureFieldHasCorrectType(q.Field, q.Type));
      return fieldInfos;
    }

    static SanityHelpers()
    {
      typeMapping = new(){
        {typeof(int) , SIMCONNECT_DATATYPE.INT32  },
        {typeof(long) , SIMCONNECT_DATATYPE.INT64},
        { typeof(float) , SIMCONNECT_DATATYPE.FLOAT32},
        { typeof(double) , SIMCONNECT_DATATYPE.FLOAT64 }
      };
      typeStringMapping = new Dictionary<int, SIMCONNECT_DATATYPE>()
      {
        {8, SIMCONNECT_DATATYPE.STRING8},
        {32, SIMCONNECT_DATATYPE.STRING32 },
        {64, SIMCONNECT_DATATYPE.STRING64 },
        {128, SIMCONNECT_DATATYPE.STRING128 },
        {256, SIMCONNECT_DATATYPE.STRING256 },
        {260, SIMCONNECT_DATATYPE.STRING260 } };
    }

    internal static void EnsureFieldHasCorrectType(FieldInfo field, SIMCONNECT_DATATYPE simType)
    {
      var fieldType = field.FieldType;
      if (fieldType == typeof(string))
      {
        if (simType != SIMCONNECT_DATATYPE.STRING8
          && simType != SIMCONNECT_DATATYPE.STRING32
          && simType != SIMCONNECT_DATATYPE.STRING64
          && simType != SIMCONNECT_DATATYPE.STRING128
          && simType != SIMCONNECT_DATATYPE.STRING256
          && simType != SIMCONNECT_DATATYPE.STRING260)
          throw new InvalidRequestException($"If the field '{field.Name}' is of type string, " +
            $"the expected sim-type should be string too (but declared type is '{simType}'.");

        var marshalAsAttribute = field.GetCustomAttribute<MarshalAsAttribute>() ??
          throw new InvalidRequestException($"If the field '{field.Name}' is of type string, " +
            $"it should have an '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]', where XXX is the correct string size.");
        if (marshalAsAttribute.SizeConst == 8 && simType != SIMCONNECT_DATATYPE.STRING8)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
        if (marshalAsAttribute.SizeConst == 32 && simType != SIMCONNECT_DATATYPE.STRING32)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
        if (marshalAsAttribute.SizeConst == 64 && simType != SIMCONNECT_DATATYPE.STRING64)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
        if (marshalAsAttribute.SizeConst == 128 && simType != SIMCONNECT_DATATYPE.STRING128)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
        if (marshalAsAttribute.SizeConst == 256 && simType != SIMCONNECT_DATATYPE.STRING256)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
        if (marshalAsAttribute.SizeConst == 260 && simType != SIMCONNECT_DATATYPE.STRING260)
          throw new InvalidRequestException($"If the field '{field.Name}' has simType = {simType}, " +
            $"the '[MarshalAs(UnmanagedType.ByValTStr, SizeConst = XXX)]' " +
            $"SizeConst must match (provided value is {marshalAsAttribute.SizeConst}).");
      }
      else if (typeMapping.ContainsKey(field.FieldType) && typeMapping[field.FieldType] != simType)
        throw new InvalidRequestException($"The field '{field.Name}' has declared type '{field.FieldType}' but " +
          $"requested sim-type is '{simType}' and should be '{typeMapping[field.FieldType]}'.");
    }

    internal static void EnsureTypeHasRequiredAttribute(Type t)
    {
      StructLayoutAttribute? sla = t.StructLayoutAttribute;

      bool hasValidAttribute =
        (sla!.Value == LayoutKind.Sequential)
        && (sla!.CharSet == CharSet.Ansi)
        && (sla!.Pack == 1);

      if (!hasValidAttribute)
      {
        throw new InvalidRequestException($"" +
          $"Struct '{t.Name}' must have [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)] attribute defined, " +
          $"bud provided is [StructLayout({sla!.Value}, CharSet = {sla!.CharSet}, Pack = {sla!.Pack})]" +
          $"See: https://docs.flightsimulator.com/html/Programming_Tools/SimConnect/Programming_SimConnect_Clients_using_Managed_Code.htm ");
      }
    }

    internal static DataDefinitionAttribute GetDataDefinitionAttributeOrThrowException(FieldInfo field)
    {
      DataDefinitionAttribute ret = field.GetCustomAttribute<DataDefinitionAttribute>()
        ?? throw new InvalidRequestException($"Field '{field.Name}' has not the " +
        $"required '{nameof(DataDefinitionAttribute)}' attribute.");

      return ret;
    }

    internal static SIMCONNECT_DATATYPE ResolveAttributeType(FieldInfo field, DataDefinitionAttribute att)
    {
      SIMCONNECT_DATATYPE ret;

      if (att.Type != SimType.UNSPECIFIED)
        ret = att.TypeAsSimConnectDataType;
      else if (typeMapping.ContainsKey(field.FieldType))
        ret = typeMapping[field.FieldType];
      else if (field.FieldType == typeof(string))
      {
        var marshalAsAttribute = field.GetCustomAttribute<MarshalAsAttribute>() ??
          throw new InvalidRequestException($"The field '{field.Name}' is of type string, but " +
          $"it has not custom type defined and also [MarshalAsAttribute] is missing to " +
          $"resolve the correct string length.");
        if (typeStringMapping.TryGetValue(marshalAsAttribute.SizeConst, out ret) == false)
          throw new InvalidRequestException($"The field '{field.Name}' is of type string," +
            $"but its [MarshallAsAttribute].SizeConst doest not match predefined " +
            $"string sizes (8/32/64/128/256/260).");
      }
      else
      {
        throw new InvalidRequestException($"The field '{field.Name}' has defined type " +
          $"'{field.FieldType}', which has not predefined mapping to SIMCONNECT_DATATYPE. " +
          $"you must define the 'type' parameter in [{nameof(DataDefinitionAttribute)}] attribute.");
      }

      return ret;
    }

    public static void EnsureDllFilesAvailable()
    {
      string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
      path = System.IO.Path.GetDirectoryName(path)!;

      var firstFile = System.IO.Path.Combine(path, "Microsoft.FlightSimulator.SimConnect.dll");
      var secondFile = System.IO.Path.Combine(path, "SimConnect.dll");

      if (System.IO.File.Exists(firstFile) == false)
        throw new ESimConnectException($"The required dll file '{firstFile}' not found.");

      if (System.IO.File.Exists(secondFile) == false)
        throw new ESimConnectException($"The required dll file '{secondFile}' not found.");
    }

  }
}

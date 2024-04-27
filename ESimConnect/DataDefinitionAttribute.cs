using ESimConnect.Definitions;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
    [AttributeUsage(AttributeTargets.Field)]
  public class DataDefinitionAttribute : Attribute
  {
    public const int EMPTY_INDEX = -1;

    public DataDefinitionAttribute(string simVarName, string? unit = null, SimType type = SimType.UNSPECIFIED)
    {
      this.Name = simVarName;
      this.Unit = unit;
      this.Type = type;
    }
    public string Name { get; }
    public string? Unit { get; }
    public SimType Type { get; }

    public SIMCONNECT_DATATYPE TypeAsSimConnectDataType
    {
      get
      {
        SIMCONNECT_DATATYPE ret = this.Type switch
        {
          SimType.UNSPECIFIED => throw new InvalidRequestException("Unable to cast 'UNSPECIFIED' to SIMCONNECT_DATATYPE."),
          _ => Enum.Parse<SIMCONNECT_DATATYPE>(Type.ToString())
        };
        return ret;
      }
    }
  }
}

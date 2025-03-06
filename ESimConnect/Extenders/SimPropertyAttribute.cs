using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  [AttributeUsage(AttributeTargets.Property, Inherited = true)]
  public class SimPropertyAttribute : Attribute
  {
    public string Name { get; private set; } = null!;
    public string Unit { get; private set; } = "Number";
    public SimConnectSimTypeName Type { get; private set; } = SimConnectSimTypeName.FLOAT64;

    public SimPropertyAttribute(
      string name,
      string unit = "Number",
      SimConnectSimTypeName type = SimConnectSimTypeName.FLOAT64)
    {
      Name = name;
      Unit = unit;
      Type = type;
    }
  }
}

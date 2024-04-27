using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ESimConnect.SimClientEvents;

namespace ESimConnect
{
  public partial class SimClientEvents
  {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class Parameter : Attribute
    {
      public int Index { get; }
      public string Description { get; }
      public Parameter(int index, string description)
      {
        this.Index = index;
        this.Description = description;
      }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class Deprecated : Attribute
    {
    }
  }
}

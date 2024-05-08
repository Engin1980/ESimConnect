using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Definitions
{
  public partial class SimEvents
  {
    public static partial class Client
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
}
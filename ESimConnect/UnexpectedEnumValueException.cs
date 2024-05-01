using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
  public class UnexpectedEnumValueException : Exception
  {
    //TODO move to ESystem
    public UnexpectedEnumValueException(Enum enumValue) : base($"Unexpected enum value '{enumValue}' for type '{enumValue.GetType().FullName}'.")
    {
    }
  }
}

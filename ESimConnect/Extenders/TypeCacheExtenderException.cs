using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  public class TypeCacheExtenderException : Exception
  {
    public TypeCacheExtenderException(string? message) : base(message)
    {
    }

    public TypeCacheExtenderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
  }
}

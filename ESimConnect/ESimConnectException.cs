using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
  public class ESimConnectException : Exception
  {
    public ESimConnectException(string? message) : base(message)
    {
    }

    public ESimConnectException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
  }

  public class InvalidRequestException : ESimConnectException
  {
    public InvalidRequestException(string? message) : base(message)
    {
    }

    public InvalidRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

  }

  public class InternalException : ESimConnectException
  {
    public InternalException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
  }

  public class NotConnectedException : ESimConnectException
  {
    public NotConnectedException() : base("SimConnect not connected.")
    {
    }
  }

}

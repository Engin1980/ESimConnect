using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  internal static class IdProvider
  {
    private static int nextRequestId = 1;
    private static readonly object lck = new();
    internal static int GetNext()
    {
      int ret;
      lock (lck)
      {
        ret = nextRequestId++;
      }
      return ret;
    }
    internal static EEnum GetNextAsEnum() => (EEnum)GetNext();
  }
}

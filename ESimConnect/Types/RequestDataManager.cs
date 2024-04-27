using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  internal class RequestDataManager
  {
    private class RData
    {
      public readonly EEnum requestId;
      public readonly int? customId;
      public readonly Type type;

      public RData(EEnum requestId, int? customId, Type type)
      {
        this.requestId = requestId;
        this.customId = customId;
        this.type = type;
      }
    }

    private readonly List<RData> inner = new();

    public void Register(int? customId, Type type, EEnum requestId)
    {
      if (customId != null && inner.Any(q => q.customId == customId))
        throw new InvalidRequestException($"customRequestId '{customId}' is already registered.");
      inner.Add(new RData(requestId, customId, type));
    }

    public void Unregister(int? customId)
    {
      if (!inner.Any(q => q.customId == customId))
        inner.Remove(inner.Single(q => q.customId == customId));
    }

    public void Recall(EEnum requestId, out Type type, out int? customId)
    {
      RData rd = inner.Single(q => q.requestId == requestId);
      type = rd.type;
      customId = rd.customId;
    }
  }
}

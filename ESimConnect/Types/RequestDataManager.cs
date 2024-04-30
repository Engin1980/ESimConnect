using Microsoft.FlightSimulator.SimConnect;
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
    private record RData(EEnum requestId, int? customId, Type type);

    private readonly List<RData> inner = new();
    private readonly Dictionary<RData, SIMCONNECT_PERIOD> periods = new();

    public void Register(int? customId, Type type, EEnum requestId)
    {
      this.Register(customId, type, requestId, null);
    }

    public void Register(int? customId, Type type, EEnum requestId, SIMCONNECT_PERIOD? period)
    {
      if (customId != null)
      {
        if (inner.Any(q => q.customId == customId))
          throw new InvalidRequestException($"customRequestId '{customId}' is already registered.");
      }
      else
      {
        if (inner.Any(q => q.type == type))
          throw new InvalidRequestException($"type '{type.Name}' without customId is already registered.");
      }
      var rdata = new RData(requestId, customId, type);
      inner.Add(rdata);
      if (period != null) periods[rdata] = period.Value;
    }

    public void Unregister(int? customId)
    {
      var rdata = inner.SingleOrDefault(q => q.customId == customId);
      if (rdata != null)
      {
        if (periods.ContainsKey(rdata)) periods.Remove(rdata);
        inner.Remove(rdata);
      }
    }

    public void Recall(EEnum requestId, out Type type, out int? customId)
    {
      RData rd = inner.Single(q => q.requestId == requestId);
      type = rd.type;
      customId = rd.customId;
    }

    internal EEnum GetIdAsEnum(int? customId, Type t)
    {
      var matches = this.inner.Where(q => q.type == t && q.customId == customId);
      if (matches.Count() > 1)
        throw new InvalidRequestException(
          $"Type '{t.Name}' is defined more than once. 'customId' is needed.");
      return matches.Single().requestId;
    }

    internal EEnum GetIdAsEnum(Type t)
    {
      return GetIdAsEnum(null, t);
    }
  }
}

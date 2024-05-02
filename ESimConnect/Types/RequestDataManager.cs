using ESystem.Asserting;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  internal class RequestsManager
  {
    public enum KindOfTypeId
    {
      STRUCT,
      VALUE
    }

    public record Request(RequestId RequestId, Type Type, TypeId TypeId, KindOfTypeId Kind, SIMCONNECT_PERIOD? Period);

    private readonly List<Request> inner = new(); //TODO rewrite to dict for speed

    private void Register(Request request)
    {
      EAssert.Argument.IsNotNull(request, nameof(request));
      Request? itemToRemove = null;

      if (request.Period != null)
      {
        // used when period is reset to another value for same request/type
        // cannot be removed directly due to thread-safety
        // at least one record must be kept everytime, so firstly add new, then remove old
        var existing = inner.SingleOrDefault(q => q.RequestId == request.RequestId && q.Period != null);
        if (existing != null)
        {
          EAssert.IsTrue(request.TypeId == existing.TypeId);
          EAssert.IsTrue(request.Type == existing.Type);
          itemToRemove = existing;
        }
      }
      else
      {
        if (inner.Any(q => q.RequestId == request.RequestId && q.Period == null))
          throw new InvalidRequestException("Duplicit id " + request.RequestId);
      }

      inner.Add(request);
      if (itemToRemove != null) inner.Remove(itemToRemove);
    }

    public void RegisterRequest(RequestId requestId, Type type, TypeId typeId, KindOfTypeId kind)
      => Register(new(requestId, type, typeId, kind, null));

    public void RegisterRepeatedlyRequest(RequestId requestId, Type type, TypeId typeId, KindOfTypeId kind, SIMCONNECT_PERIOD period)
      => Register(new(requestId, type, typeId, kind, period));


    public void Unregister(RequestId requestId)
    {
      var item = inner.SingleOrDefault(q => q.RequestId == requestId)
        ?? throw new InvalidRequestException($"RequestId '{requestId}' is not registered.");
      inner.Remove(item);
    }

    internal IEnumerable<Request> GetByTypeId(KindOfTypeId kind, TypeId typeId) => inner.Where(q => q.Kind == kind && q.TypeId == typeId);
    internal Request GetByRequestId(RequestId requestId) => this.inner.Single(q => q.RequestId == requestId);
    internal IEnumerable<Request> GetAll() => this.inner.ToList();
  }
}

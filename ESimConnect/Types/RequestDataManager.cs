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
  internal class RequestDataManager
  {
    public record Request(RequestId RequestId, Type Type, TypeId? TypeId, SIMCONNECT_PERIOD? Period);

    private readonly List<Request> inner = new(); // rewrite to dict for speed

    private void Register(Request request)
    {
      EAssert.Argument.IsNotNull(request, nameof(request));

      if (request.Period != null)
      {
        // used when period is reset to another value for same request/type
        var existing = inner.Single(q => q.RequestId == request.RequestId && q.Period != null);
        if (existing != null)
        {
          EAssert.IsTrue(request.TypeId == existing.TypeId); //TODO how do this for values???
          inner.Remove(existing);
        }
      }
      else
      {
        if (inner.Any(q => q.RequestId == request.RequestId && q.Period == null))
          throw new InvalidRequestException("Duplicit id " + request.RequestId);
      }

      inner.Add(request);
    }

    public void RegisterStructRequest(RequestId requestId, Type type, TypeId typeId)
      => Register(new(requestId, type, typeId, null));

    public void RegisterStructRepeatedlyRequest(RequestId requestId, Type type, TypeId typeId, SIMCONNECT_PERIOD period)
      => Register(new(requestId, type, typeId, period));


    public void Unregister(RequestId requestId)
    {
      var item = inner.SingleOrDefault(q => q.RequestId == requestId)
        ?? throw new InvalidRequestException($"RequestId '{requestId}' is not registered.");
      inner.Remove(item);
    }

    //public void Recall(EEnum requestId, out Type type, out int? customId)
    //{
    //  RequestItem rd = inner.Single(q => q.requestId == requestId);
    //  type = rd.type;
    //  customId = rd.customId;
    //}

    //internal EEnum GetIdAsEnum(int? customId, Type t)
    //{
    //  var matches = this.inner.Where(q => q.type == t && q.customId == customId);
    //  if (matches.Count() > 1)
    //    throw new InvalidRequestException(
    //      $"Type '{t.Name}' is defined more than once. 'customId' is needed.");
    //  return matches.Single().requestId;
    //}

    //internal EEnum GetIdAsEnum(Type t)
    //{
    //  return GetIdAsEnum(null, t);
    //}

    internal IEnumerable<Request> GetByTypeId(TypeId typeId) => inner.Where((Func<Request, bool>)(q => q.typeId == typeId));
    internal Request GetByRequestId(RequestId requestId) => this.inner.Single(q => q.RequestId == requestId);

    internal IEnumerable<Request> GetAll() => this.inner.ToList();
  }
}

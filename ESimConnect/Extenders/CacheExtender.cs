using ESystem;
using ESystem.Asserting;
using ESystem.Miscelaneous;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  public class ValueCacheExtender : AbstractExtender
  {
    public record TypeDefinition(string Name, string Unit, SimConnectSimTypeName Type);
    public record ValueChangeEventArgs(TypeId TypeId, double Value);

    public const string DEFAULT_UNIT = "Number";
    public const SimConnectSimTypeName DEFAULT_TYPE = SimConnectSimTypeName.FLOAT64;

    private readonly object lck = new();
    private readonly Dictionary<TypeDefinition, TypeId> types = new();
    private readonly BiDictionary<TypeId, RequestId> requests = new();
    private readonly ConcurrentDictionary<TypeId, double> values = new();

    public event Action<ValueChangeEventArgs>? ValueChanged;

    public ValueCacheExtender(ESimConnect eSimCon) : base(eSimCon)
    {
      eSimCon.DataReceived += ESimCon_DataReceived;
    }

    private void ESimCon_DataReceived(ESimConnect _, ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      TypeId typeId = requests[e.RequestId];
      double value = (double)e.Data;
      values[typeId] = value;
      this.ValueChanged?.Invoke(new ValueChangeEventArgs(typeId, value));
    }

    public (TypeId, RequestId) Register(
      string name,
      string unit = DEFAULT_UNIT,
      SimConnectSimTypeName type = DEFAULT_TYPE,
      SimConnectPeriod period = 0)
    {
      TypeId typeId = RegisterTypeIfRequired(name, unit, type);

      //TODO here deal with situation when new period is shorter than the previous one
      RequestId requestId = RequestRepeatedlyIfRequired(typeId, period);
      return (typeId, requestId);
    }

    public double GetValue(TypeId typeId)
    {
      EAssert.Argument.IsTrue(values.ContainsKey(typeId), nameof(typeId), "Not registered.");
      double ret = values[typeId];
      return ret;
    }

    private RequestId RequestRepeatedlyIfRequired(TypeId typeId, SimConnectPeriod period)
    {
      RequestId ret;
      lock (lck)
      {
        RequestId? rqId = requests.TryGet(typeId);
        if (rqId == null)
        {
          var requestId = eSimCon.Values.RequestRepeatedly(typeId, period, true);
          requests[typeId] = requestId;
        }
        ret = requests[typeId];
      }
      return ret;
    }

    private TypeId RegisterTypeIfRequired(string name, string unit, SimConnectSimTypeName type)
    {
      TypeId ret;
      TypeDefinition td = new(name, unit, type);
      lock (lck)
      {
        TypeId? tdr = types.TryGet(td);
        if (tdr == null)
        {
          var typeId = eSimCon.Values.Register<double>(td.Name, unit: td.Unit, simTypeName: td.Type);
          types[td] = typeId;
        }
        ret = types[td];
      }

      return ret;
    }
  }
}

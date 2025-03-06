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
    private readonly SimConnectPeriod period;

    public event Action<ValueChangeEventArgs>? ValueChanged;

    public ValueCacheExtender(ESimConnect eSimCon, SimConnectPeriod period = SimConnectPeriod.SECOND) : base(eSimCon)
    {
      EAssert.Argument.IsTrue(period != SimConnectPeriod.NEVER, nameof(period), "Period cannot be 'ONCE' or 'NEVER'");

      this.period = period;
      eSimCon.DataReceived += ESimCon_DataReceived;
    }

    private void ESimCon_DataReceived(ESimConnect _, ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      TypeId typeId = requests[e.RequestId];
      double value = (double)e.Data;
      values[typeId] = value;
      this.ValueChanged?.Invoke(new ValueChangeEventArgs(typeId, value));
    }

    public TypeId Register(
      string name,
      string unit = DEFAULT_UNIT,
      SimConnectSimTypeName type = DEFAULT_TYPE)
    {
      TypeId typeId = RegisterTypeIfRequired(name, unit, type);
      RequestRepeatedlyIfRequired(typeId);
      return typeId;
    }

    public double GetValue(TypeId typeId)
    {
      EAssert.Argument.IsTrue(values.ContainsKey(typeId), nameof(typeId), "Not registered.");
      double ret = values[typeId];
      return ret;
    }

    private void RequestRepeatedlyIfRequired(TypeId typeId)
    {
      lock (lck)
      {
        if (requests.TryGetValue(typeId, out RequestId requestId) == false)
        {
          requestId = eSimCon.Values.RequestRepeatedly(typeId, this.period, true);
          requests[typeId] = requestId;
        }
      }
    }

    private TypeId RegisterTypeIfRequired(string name, string unit, SimConnectSimTypeName type)
    {
      TypeId ret;
      TypeDefinition td = new(name, unit, type);
      lock (lck)
      {
        if (types.TryGetValue(td, out ret) == false)
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

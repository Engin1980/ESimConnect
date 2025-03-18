using ESystem;
using ESystem.Asserting;
using ESystem.Miscelaneous;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  /// <summary>
  /// Caches the value of SimVar obtained from ESimConnect and obtain it every second. 
  /// Provides the value on request.
  /// </summary>
  public class ValueCacheExtender : AbstractExtender
  {
    /// <summary>
    /// Contains type definition info with their value.
    /// </summary>
    /// <param name="SimVarDefinition">Definition of SimVar.</param>
    /// <param name="Value">Value of the </param>
    public record SimVarValue(SimVarDefinition SimVarDefinition, double Value);
    /// <summary>
    /// Contains SimVar definition.
    /// </summary>
    /// <param name="Name">SimVar name</param>
    /// <param name="Unit">SimVar unit in which data are returned (like feet, knots, meters, ...)</param>
    /// <param name="Type">DataType in which value is returned (like Int, Double, ...)</param>
    public record SimVarDefinition(string Name, string Unit, SimConnectSimTypeName Type);
    /// <summary>
    /// Contains TypeId with the current value.
    /// </summary>
    /// <param name="TypeId">TypeId</param>
    /// <param name="Value">Current Value</param>
    public record ValueChangeEventArgs(TypeId TypeId, double Value);
    /// <summary>
    /// Contains simvar definition info.
    /// </summary>
    /// <param name="SimVarDefinition">SimVar definition</param>
    /// <param name="TypeId">Corresponding TypeId</param>
    /// <param name="IsRepeatedRegistration">True if this definition was already registered, false otherwise.</param>
    public record NewRegistration(SimVarDefinition SimVarDefinition, TypeId TypeId, bool IsRepeatedRegistration);

    /// <summary>
    /// Default SimVar unit.
    /// </summary>
    public const string DEFAULT_UNIT = "Number";
    /// <summary>
    /// Defualt SimVar Type
    /// </summary>
    public const SimConnectSimTypeName DEFAULT_TYPE = SimConnectSimTypeName.FLOAT64;

    private readonly object lck = new();
    private readonly Dictionary<SimVarDefinition, TypeId> types = new();
    private readonly BiDictionary<TypeId, RequestId> requests = new();
    private readonly ConcurrentDictionary<TypeId, double> values = new();
    private readonly SimConnectPeriod period;

    /// <summary>
    /// Invoked when value of some registed SimVar was changed.
    /// </summary>
    public event Action<ValueChangeEventArgs>? ValueChanged;
    /// <summary>
    /// Invoked when registration is done (even for already registed SimVar).
    /// </summary>
    public event Action<NewRegistration>? TypeRegistered;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="eSimCon">Underlying ESimConnect object</param>
    /// <param name="period">Period at which values are updated. Second by default.</param>
    public ValueCacheExtender(ESimConnect eSimCon, SimConnectPeriod period = SimConnectPeriod.SECOND) : base(eSimCon)
    {
      EAssert.Argument.IsTrue(period != SimConnectPeriod.NEVER, nameof(period), "Period cannot be 'ONCE' or 'NEVER'");

      this.period = period;
      eSimCon.DataReceived += ESimCon_DataReceived;
    }

    /// <summary>
    /// Registers new SimVar
    /// </summary>
    /// <param name="name">SimVar name</param>
    /// <param name="unit">SimVar unit - like meters, feet, etc.</param>
    /// <param name="type">Data type, like int, float, ...</param>
    /// <returns>Unique TypeId. This Id is required to get the current value.</returns>
    public TypeId Register(
      string name,
      string unit = DEFAULT_UNIT,
      SimConnectSimTypeName type = DEFAULT_TYPE)
    {
      var nr = RegisterTypeIfRequired(name, unit, type);
      if (nr.IsRepeatedRegistration == false)
        RequestRepeatedlyIfRequired(nr.TypeId);

      TypeRegistered?.Invoke(nr);
      return nr.TypeId;
    }

    /// <summary>
    /// Returns all values of all registered SimVars.
    /// </summary>
    /// <returns>SimVar infos + values in list.</returns>
    public List<SimVarValue> GetAllValues()
      => types.Select(q => new SimVarValue(q.Key, TryGetValue(q.Value, double.NaN))).ToList();

    /// <summary>
    /// Returns the current value of the SimVar based on its TypeId.
    /// </summary>
    /// <param name="typeId">TypeId defining the required SimVar.</param>
    /// <returns>Current value of SimVars</returns>
    public double GetValue(TypeId typeId)
    {
      EAssert.Argument.IsTrue(requests.ContainsKey(typeId), nameof(typeId), $"TypeId {typeId} not registered.");
      if (values.TryGetValue(typeId, out double ret) == false)
        ret = double.NaN;
      return ret;
    }

    private void ESimCon_DataReceived(ESimConnect _, ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      if (requests.TryGetValue(e.RequestId, out TypeId typeId))
      {
        double value = (double)e.Data;
        values[typeId] = value;
        this.ValueChanged?.Invoke(new ValueChangeEventArgs(typeId, value));
      }
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

    private NewRegistration RegisterTypeIfRequired(string name, string unit, SimConnectSimTypeName type)
    {
      NewRegistration ret;
      SimVarDefinition td = new(name, unit, type);
      lock (lck)
      {
        if (types.ContainsKey(td))
        {
          ret = new NewRegistration(td, types[td], true);
        }
        else
        {
          TypeId typeId = eSimCon.Values.Register<double>(td.Name, unit: td.Unit, simTypeName: td.Type);
          types[td] = typeId;
          ret = new NewRegistration(td, typeId, false);
        }
      }

      return ret;
    }

    private double TryGetValue(TypeId typeId, double defaultValue)
    {
      double ret;
      if (values.TryGetValue(typeId, out ret) == false)
        ret = defaultValue;
      return ret;
    }
  }
}

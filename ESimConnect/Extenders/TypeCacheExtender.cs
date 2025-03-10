using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  /// <summary>
  /// Extender to register all properties as SimVars in class and return its values on request.
  /// </summary>
  /// <remarks>
  /// The point is to have a class with properties annotated with <see cref="SimPropertyAttribute"/>
  /// For every property, this attribute defines its mapping to SimVar.
  /// Once such type is registered, the call of <see cref="GetSnapshost{T}">GetSnapshot()</see> will 
  /// fill/return and instance with current SimVar data in properties. />
  /// </remarks>
  public class TypeCacheExtender
  {
    private readonly object lck = new();
    private readonly ValueCacheExtender cache;
    private readonly Dictionary<PropertyInfo, TypeId> props = new();
    private readonly HashSet<Type> types = new();

    /// <summary>
    /// Creates a new instance
    /// </summary>
    /// <param name="cache">Underyling <see cref="ValueCacheExtender"/>.</param>
    public TypeCacheExtender(ValueCacheExtender cache)
    {
      EAssert.Argument.IsNotNull(cache, nameof(cache));
      this.cache = cache;
    }

    /// <summary>
    /// Registers a class with <see cref="SimPropertyAttribute" /> annotated properties./>
    /// </summary>
    /// <typeparam name="T">Type to register.</typeparam>
    public void Register<T>()
    {
      this.Register(typeof(T));
    }

    /// <summary>
    /// Registers a class with <see cref="SimPropertyAttribute" /> annotated properties./>
    /// </summary>
    /// <param name="type">Type to register.</param>
    public void Register(Type type)
    {
      var tmp = type
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Select(q => new { Property = q, Attribute = q.GetCustomAttribute<SimPropertyAttribute>() })
        .Where(q => q.Attribute != null)
        .ToList();

      lock (lck)
      {
        foreach (var item in tmp)
        {
          TypeId typeId = cache.Register(item.Attribute!.Name, item.Attribute.Unit, item.Attribute.Type);
          props[item.Property] = typeId;
        }
        types.Add(type);
      }
    }

    /// <summary>
    /// Returns current values in new instance of registered type.
    /// </summary>
    /// <typeparam name="T">Registered type.</typeparam>
    /// <returns>Snapshot of SimVar values in properties in a new instance.</returns>
    /// <exception cref="TypeCacheExtenderException">Thows exception if type is not registered.</exception>
    public T GetSnapshost<T>() where T : new()
    {
      T ret = new();
      FillSnapshost(ret);
      return ret;
    }

    /// <summary>
    /// Sets current values into existing instance of registered type.
    /// </summary>
    /// <typeparam name="T">Registered type.</typeparam>
    /// <param name="snapshot">Instace of registerd type.</param>
    /// <exception cref="TypeCacheExtenderException">Thows exception if type is not registered.</exception>
    public void FillSnapshost<T>(T snapshot)
    {
      EAssert.Argument.IsNotNull(snapshot, nameof(snapshot));
      var tmp = snapshot.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty)
        .Select(q => new { Property = q, Attribute = q.GetCustomAttribute<SimPropertyAttribute>() })
        .Where(q => q.Attribute != null)
        .ToList();

      bool isRegisteredType;
      lock (lck)
      {
        isRegisteredType = types.Contains(snapshot.GetType());
      }
      if (!isRegisteredType)
        throw new TypeCacheExtenderException(
          $"Snapshot request for type '{snapshot.GetType().FullName}' " +
          $"was invoked, but type was not registered first.");

      foreach (var item in tmp)
      {
        TypeId typeId = props[item.Property];
        double val = cache.GetValue(typeId);
        try
        {
          object targetVal = Convert.ChangeType(val, item.Property.PropertyType);
          item.Property.SetValue(snapshot, targetVal);
        }
        catch (Exception ex)
        {
          throw new TypeCacheExtenderException(
            $"Failed to inject value '{val}' to property '{snapshot.GetType().Name}.{item.Property.Name}'.", ex);
        }
      }
    }
  }
}

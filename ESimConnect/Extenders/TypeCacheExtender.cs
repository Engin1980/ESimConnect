using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Extenders
{
  public class TypeCacheExtender
  {
    private readonly object lck = new();
    private readonly ValueCacheExtender cache;
    private readonly Dictionary<PropertyInfo, TypeId> props = new();
    private readonly HashSet<Type> types = new();

    public TypeCacheExtender(ValueCacheExtender cache)
    {
      EAssert.Argument.IsNotNull(cache, nameof(cache));
      this.cache = cache;
    }

    public void Register<T>()
    {
      this.Register(typeof(T));
    }

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

    public T GetSnapshost<T>() where T : new()
    {
      T ret = new();
      FillSnapshost(ret);
      return ret;
    }

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

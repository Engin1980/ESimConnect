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
    private readonly ValueCacheExtender cache;
    private readonly Dictionary<PropertyInfo, TypeId> props = new();

    public TypeCacheExtender(ValueCacheExtender cache)
    {
      EAssert.Argument.IsNotNull(cache, nameof(cache));
      this.cache = cache;
    }

    public void Register(Type type)
    {
      var tmp = type
        .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
        .Select(q => new { Property = q, Attribute = q.GetCustomAttribute<SimPropertyAttribute>() })
        .Where(q => q.Attribute != null)
        .ToList();

      foreach (var item in tmp)
      {
        TypeId typeId = cache.Register(item.Attribute!.Name, item.Attribute.Unit, item.Attribute.Type);
        props[item.Property] = typeId;
      }
    }

    public T GetSnapshost<T>() where T : new()
    {
      T ret = new T();
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

      var misses = tmp.Where(q => props.ContainsKey(q.Property) == false).ToList();
      if (misses.Any())
      {
        throw new ApplicationException(
          $"Requested properties " +
          $"'{string.Join(",", misses.Select(q => q.Property.DeclaringType + "." + q.Property.Name))}' " +
          $"were not registered. Did you register the type first?");
      }

      foreach (var item in tmp)
      {
        TypeId typeId = props[item.Property];
        double val = cache.GetValue(typeId);
        object targetVal = Convert.ChangeType(val, item.Property.PropertyType);
        item.Property.SetValue(snapshot, targetVal);
      }
    }
  }
}

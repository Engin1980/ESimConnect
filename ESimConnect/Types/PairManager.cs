using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  internal class PrimitiveManager
  {
    private readonly Dictionary<int, Type> inner = new();

    public void Register(int id, Type type)
    {
      if (inner.ContainsKey(id))
        throw new ApplicationException("Duplicit ID int Primitive manager.");
      this.inner[id] = type;
    }

    public Type GetType(int id) { return inner[id]; }

    internal void Unregister(int typeId)
    {
      this.inner.Remove(typeId);
    }

    internal bool IsRegistered(int id)
    {
      return inner.ContainsKey(id);
    }

    internal List<int> GetRegisteredTypesIds()
    {
      return inner.Keys.ToList();
    }
  }

  internal class TypeManager : PairManager<int, Type>
  {
    protected override bool IsSame(int a, int b) => a == b;

    protected override bool IsSame(Type a, Type b) => a == b;

    internal EEnum GetIdAsEnum(Type type) => (EEnum)GetId(type);

    public new void Register(int id, Type type) => base.Register(id, type);

    public int GetId(Type type) => base.TryGet(type)
      .OrThrow(() => new InvalidRequestException($"Type {type} not registered."));

    public Type GetType(int id) => base.TryGet(id)
      .OrThrow(() => new InvalidRequestException($"Type with id {id} not registered."));

    internal void EnsureTypeRegistered(Type type)
    {
      int _ = GetId(type);
    }

    internal new void Unregister(Type type) => base.Unregister(type);

    internal List<Type> GetRegisteredTypes()
    {
      return this.GetAllBs();
    }
  }

  internal class EventManager : PairManager<EEnum, string>
  {
    protected override bool IsSame(EEnum a, EEnum b) => a == b;

    protected override bool IsSame(string a, string b) => a == b;

    public new void Register(EEnum id, string @event) => base.Register(id, @event);

    public string GetEvent(EEnum id) => base.TryGet(id)
      .OrThrow(() => new InvalidRequestException($"Event with id {id} not registered."));

    public EEnum GetId(string @event) => this.TryGet(@event)
      .OrThrow(() => new InvalidRequestException($"Event {@event} not registered."));

    internal EEnum TryGetId(string @event) => base.TryGet(@event).OrElseDefault();
  }

  internal class Optional<T>
  {
    public T? Value { get; private set; }
    public bool HasValue { get; private set; }
    public static Optional<T> Of(T t) => new Optional<T>() { Value = t, HasValue = true };
    public static Optional<T> Empty() => new Optional<T>() { Value = default(T), HasValue = false };

    internal T? OrThrow(Func<Exception> exceptionProducer)
    {
      if (!HasValue)
        throw exceptionProducer.Invoke();
      else
        return Value;
    }

    internal T? OrElse(T defaultValue)
    {
      return HasValue ? Value : defaultValue;
    }

    internal T? OrElse(Func<T> producer)
    {
      return HasValue ? Value : producer.Invoke();
    }

    internal T? OrElseDefault()
    {
      if (HasValue)
        return Value;
      else
        return default(T);
    }

    private Optional() { }
  }

  internal abstract class PairManager<Ta, Tb>
  {
    protected abstract bool IsSame(Ta a, Ta b);
    protected abstract bool IsSame(Tb a, Tb b);

    private class Record
    {
      public readonly Ta a;
      public readonly Tb b;

      public Record(Ta a, Tb b)
      {
        this.a = a;
        this.b = b;
      }
    }

    private readonly List<Record> inner = new();

    protected List<Tb> GetAllBs() => inner.Select(q => q.b).ToList();
    protected List<Ta> GetAllAs() => inner.Select(q => q.a).ToList();
    protected void Register(Ta a, Tb b)
    {
      if (a == null || b == null)
        throw new ArgumentNullException("Any value cannot be null.");
      if (inner.Any(q => IsSame(q.a, a)))
        throw new InvalidRequestException(
          $"Unable to register {a}={b}. '{a}' already registered as '{TryGet(a)}'.");
      if (inner.Any(q => IsSame(q.b, b)))
        throw new InvalidRequestException(
          $"Unable to register {a}={b}. '{b}' already registered with '{TryGet(b)}'.");
      inner.Add(new Record(a, b));
    }

    protected Optional<Ta> TryGet(Tb b)
    {
      Record? r = inner.FirstOrDefault(q => IsSame(q.b, b));
      Optional<Ta> ret = r == null ? Optional<Ta>.Empty() : Optional<Ta>.Of(r.a);
      return ret;
    }

    protected Optional<Tb> TryGet(Ta a)
    {
      Record? r = inner.FirstOrDefault(q => IsSame(q.a, a));
      Optional<Tb> ret = r == null ? Optional<Tb>.Empty() : Optional<Tb>.Of(r.b);
      return ret;
    }


    protected void Unregister(Ta id)
    {
      inner.Remove(inner.Single(q => IsSame(q.a, id)));
    }

    protected void Unregister(Tb value)
    {
      inner.Remove(inner.Single(q => IsSame(q.b, value)));
    }
  }
}

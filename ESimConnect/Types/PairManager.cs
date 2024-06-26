﻿using ESystem;
using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ESimConnect.Types
{
    internal class TypeManager
  {
    private readonly List<TypeDef> inner = new();
    public record TypeDef(TypeId TypeId, Type Type);

    public void Register(TypeId typeId, Type type)
    {
      EAssert.Argument.IsNotNull(typeId, nameof(typeId));
      EAssert.Argument.IsNotNull(type, nameof(type));
      EAssert.IsTrue(inner.None(q => q.TypeId == typeId));
      inner.Add(new(typeId, type));
    }

    public void Unregister(TypeId typeId)
    {
      EAssert.IsTrue(inner.Any(q => q.TypeId == typeId));
      inner.RemoveAll(q => q.TypeId == typeId);
    }

    public Type this[TypeId typeid]
    {
      get => inner.SingleOrDefault(q => q.TypeId == typeid)?.Type ?? throw new ESimConnectException($"TypeId '{typeid}' not associated to any type.");
    }

    public IEnumerable<TypeId> GetByType(Type type) => inner.Where(q => q.Type == type).Select(q => q.TypeId);
    public TypeId GetByTypeSingle(Type type)
    {
      var tmp = GetByType(type);
      if (!tmp.Any())
        throw new ESimConnectException($"There is not typeId associated with type '{type.FullName}'.");
      if (tmp.Count() > 1)
        throw new ESimConnectException($"There are multiple typeIds associated with type '{type.FullName}'. Try use exact typeId.");
      return tmp.First();
    }

    internal IEnumerable<TypeId> GetAllTypeIds() => inner.Select(q => q.TypeId);
  }

  internal class EventManager : PairManager<EEnum, string>
  {
    protected override bool IsSame(EEnum a, EEnum b) => a == b;

    protected override bool IsSame(string a, string b) => a == b;

    public new void Register(EEnum id, string @event) => base.Register(id, @event);

    public string GetEvent(EEnum id) => base.TryGet(id)
      .OrThrow(() => new InvalidRequestException($"Event with id {id} not registered."))!;

    public EEnum GetId(string @event) => this.TryGet(@event)
      .OrThrow(() => new InvalidRequestException($"Event {@event} not registered."));

    internal EEnum TryGetId(string @event) => base.TryGet(@event).OrElseDefault();
  }

  internal class Optional<T>
  {
    public T? Value { get; private set; }
    public bool HasValue { get; private set; }
    public static Optional<T> Of(T t) => new() { Value = t, HasValue = true };
    public static Optional<T> Empty() => new() { Value = default, HasValue = false };

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
        return default;
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
      if (a == null)
        throw new ArgumentNullException(nameof(a));
      if (b == null)
        throw new ArgumentNullException(nameof(b));
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

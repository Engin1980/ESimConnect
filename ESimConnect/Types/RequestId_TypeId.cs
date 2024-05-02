﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  internal class IdProvider
  {
    private static int nextId = 1;
    private static readonly SpinLock spinLock = new();
    public static int Next()
    {
      int ret;
      bool lockFlag = false;
      spinLock.Enter(ref lockFlag);
      if (!lockFlag) Monitor.Enter(spinLock);
      ret = nextId++;
      if (!lockFlag) Monitor.Exit(spinLock);
      else spinLock.Exit();
      return ret;
    }
  }

  public readonly struct RequestId
  {
    public static RequestId Next() => new(IdProvider.Next());

    private readonly int value;

    internal RequestId(EEnum value)
    {
      this.value = (int)value;
    }

    public RequestId(int value)
    {
      this.value = value;
    }

    public static bool operator ==(RequestId left, RequestId right) { return left.Equals(right); }
    public static bool operator !=(RequestId left, RequestId right) { return !left.Equals(right); }
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
      if (obj is RequestId requestId)
        return this.value == requestId.value;
      else
        return false;
    }
    public readonly override int GetHashCode()
    {
      return this.value.GetHashCode();
    }
    public readonly override string ToString() => this.value.ToString();

    internal readonly EEnum ToEEnum() => (EEnum)this.value;
    public readonly int ToInt() => this.value;
  }

  public readonly struct EventId
  {
    public static EventId Next() => new(IdProvider.Next());

    private readonly int value;

    internal EventId(EEnum value)
    {
      this.value = (int)value;
    }

    public EventId(int value)
    {
      this.value = value;
    }

    public static bool operator ==(EventId left, EventId right) { return left.Equals(right); }
    public static bool operator !=(EventId left, EventId right) { return !left.Equals(right); }
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
      if (obj is EventId requestId)
        return this.value == requestId.value;
      else
        return false;
    }
    public readonly override int GetHashCode()
    {
      return this.value.GetHashCode();
    }
    public readonly override string ToString() => this.value.ToString();

    internal readonly EEnum ToEEnum() => (EEnum)this.value;
    public readonly int ToInt() => this.value;
  }

  public readonly struct TypeId
  {
    private readonly int value;
    public static TypeId Next() => new(IdProvider.Next());

    internal TypeId(EEnum value)
    {
      this.value = (int)value;
    }

    public TypeId(int value)
    {
      this.value = value;
    }

    public static bool operator ==(TypeId left, TypeId right) { return left.Equals(right); }
    public static bool operator !=(TypeId left, TypeId right) { return !left.Equals(right); }
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
      if (obj is TypeId typeId)
        return this.value == typeId.value;
      else
        return false;
    }
    public readonly override int GetHashCode()
    {
      return this.value.GetHashCode();
    }
    public readonly override string ToString() => this.value.ToString();

    internal readonly EEnum ToEEnum() => (EEnum)this.value;
    public readonly int ToInt() => this.value;
  }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Types
{
  public readonly struct RequestId
  {
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

  public readonly struct TypeId
  {
    private readonly int value;

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

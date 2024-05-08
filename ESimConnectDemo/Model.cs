using ESimConnect;
using ESimConnect.Definitions;
using ESystem.Miscelaneous;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESimConnectDemo
{
  class Model : ESystem.Miscelaneous.NotifyPropertyChanged
  {
    public enum EConnectionStatus
    {
      CONNECTED,
      DISCONNECTED
    }

    public class SimVarValue : NotifyPropertyChanged
    {
      public event Action<SimVarValue>? UpdateRequest;
      public TypeId TypeId { get; set; }
      public RequestId RequestId { get; set; }

      public string Name
      {
        get => base.GetProperty<string>(nameof(Name))!;
        set => base.UpdateProperty(nameof(Name), value);
      }

      public double Value
      {
        get => base.GetProperty<double>(nameof(Value));
        set
        {
          base.UpdateProperty(nameof(Value), value);
          var now = DateTime.Now;
          this.LastUpdateInterval = now - this.LastUpdateTime;
          this.LastUpdateTime = now;
        }
      }

      public ESimConnect.SimConnectPeriod Period
      {
        get => base.GetProperty<ESimConnect.SimConnectPeriod>(nameof(Period));
        set
        {
          base.UpdateProperty(nameof(Period), value);
          this.UpdateRequest?.Invoke(this);
        }
      }

      public bool OnlyOnChange
      {
        get => base.GetProperty<bool>(nameof(OnlyOnChange));
        set
        {
          base.UpdateProperty(nameof(OnlyOnChange), value);
          this.UpdateRequest?.Invoke(this);
        }
      }

      public int FrameSkip
      {
        get => base.GetProperty<int>(nameof(FrameSkip));
        set
        {
          base.UpdateProperty(nameof(FrameSkip), Math.Max(value, 0));
          this.UpdateRequest?.Invoke(this);
        }
      }

      public int ValueToSet
      {
        get => base.GetProperty<int>(nameof(ValueToSet));
        set => base.UpdateProperty(nameof(ValueToSet), value);
      }

      public DateTime LastUpdateTime
      {
        get => base.GetProperty<DateTime>(nameof(LastUpdateTime));
        private set => base.UpdateProperty(nameof(LastUpdateTime), value);
      }

      public TimeSpan LastUpdateInterval
      {
        get => base.GetProperty<TimeSpan>(nameof(LastUpdateInterval));
        private set => base.UpdateProperty(nameof(LastUpdateInterval), value);
      }

      public List<SimConnectPeriod> Periods => Model.Periods;

      public string UnitName
      {
        get => base.GetProperty<string>(nameof(UnitName))!;
        set
        {
          base.UpdateProperty(nameof(UnitName), value);
          this.UpdateRequest?.Invoke(this);
        }
      }
    }

    public class SimVarEvent : NotifyPropertyChanged
    {
      public EventId EventId { get; internal set; }
      public string Name { get; internal set; } = null!;
      public DateTime LastInvoked
      {
        get => base.GetProperty<DateTime>(nameof(LastInvoked))!;
        set => base.UpdateProperty(nameof(LastInvoked), value);
      }
    }

    public EConnectionStatus ConnectionStatus
    {
      get => base.GetProperty<EConnectionStatus>(nameof(ConnectionStatus));
      set => base.UpdateProperty(nameof(ConnectionStatus), value);
    }

    public BindingList<SimVarValue> Values
    {
      get => base.GetProperty<BindingList<SimVarValue>>(nameof(Values))!;
      set => base.UpdateProperty(nameof(Values), value);
    }

    public BindingList<SimVarEvent> Events
    {
      get => base.GetProperty<BindingList<SimVarEvent>>(nameof(Events))!;
      set => base.UpdateProperty(nameof(Events), value);
    }

    public BindingList<string> FiredEvents
    {
      get => base.GetProperty<BindingList<string>>(nameof(FiredEvents))!;
      set => base.UpdateProperty(nameof(FiredEvents), value);
    }

    public Model()
    {
      ConnectionStatus = EConnectionStatus.DISCONNECTED;
      Values = new();
      Events = new();
      FiredEvents = new();
    }

    public static List<SimConnectPeriod> Periods
    {
      get;
      private set;
    }


    public static List<ComboBoxDataItem> SimUnits
    {
      get;
      private set;
    }

    static Model()
    {
      Periods = new()
      {
        SimConnectPeriod.NEVER,
        SimConnectPeriod.SECOND,
        SimConnectPeriod.SIM_FRAME,
        SimConnectPeriod.VISUAL_FRAME
      };

      SimUnits = GenerateSimUnitsComboBoxItems();
    }

    public record ComboBoxDataItem(string Label, string Value);
    private static List<ComboBoxDataItem> GenerateSimUnitsComboBoxItems()
    {
      List<ComboBoxDataItem> ret = new();
      Type t = typeof(SimUnits);
      foreach (var cls in t.GetNestedTypes())
      {
        foreach (var field in cls.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
        {
          string val = (string)field.GetValue(null)!;
          ComboBoxDataItem cbdi = new($"{cls.Name}.{field.Name}", val);
          ret.Add(cbdi);
        }
      }

      ret = ret.OrderBy(q => q.Label).ToList();
      ret.Insert(0, new ComboBoxDataItem("", "Number"));
      return ret;
    }
  }
}

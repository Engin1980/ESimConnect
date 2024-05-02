using ESimConnect;
using ESystem.Miscelaneous;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Model()
        {
            ConnectionStatus = EConnectionStatus.DISCONNECTED;
            Values = new();
        }

        public static List<SimConnectPeriod> Periods
        {
            get;
            private set;
        }

        static Model()
        {
            Periods = new()
            {
                ESimConnect.SimConnectPeriod.NEVER,
                ESimConnect.SimConnectPeriod.SECOND,
                ESimConnect.SimConnectPeriod.SIM_FRAME,
                ESimConnect.SimConnectPeriod.VISUAL_FRAME
            };
        }
    }
}

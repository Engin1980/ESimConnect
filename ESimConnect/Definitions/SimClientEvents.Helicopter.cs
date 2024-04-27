namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class Helicopter
    {
      public static class General
      {
        public const string AUTO_HOVER_OFF = "AUTO_HOVER_OFF";

        public const string AUTO_HOVER_ON = "AUTO_HOVER_ON";

        [Parameter(0, "True/False (1, 0)")]
        public const string AUTO_HOVER_SET = "AUTO_HOVER_SET";

        public const string AUTO_HOVER_TOGGLE = "AUTO_HOVER_TOGGLE";

        [Parameter(0, "Set the collective (0 to 16384)")]
        public const string AXIS_COLLECTIVE_SET = "AXIS_COLLECTIVE_SET";

        [Parameter(0, "Set the steering amount (-16384 to 16384)")]
        public const string AXIS_STEERING_SET = "AXIS_STEERING_SET";

        [Parameter(0, "Set tail rotor speed (0 to 16384)")]
        public const string AXIS_TAIL_ROTOR_SET = "AXIS_TAIL_ROTOR_SET";

        public const string COLLECTIVE_DECR = "COLLECTIVE_DECR";

        public const string COLLECTIVE_INCR = "COLLECTIVE_INCR";

        [Parameter(0, "value")]
        public const string DECREASE_HELO_GOV_BEEP = "DECREASE_HELO_GOV_BEEP";

        [Parameter(0, "value")]
        public const string INCREASE_HELO_GOV_BEEP = "INCREASE_HELO_GOV_BEEP&nbsp;";

        [Parameter(0, "value")]
        public const string SET_HELO_GOV_BEEP = "SET_HELO_GOV_BEEP";

      }
      public static class Cyclic
      {
        [Parameter(0, "Set the lateral cyclic (-16384 to 16384)")]
        public const string AXIS_CYCLIC_LATERAL_SET = "AXIS_CYCLIC_LATERAL_SET";

        [Parameter(0, "Set the longitudinal cyclic (-16384 to 16384)")]
        public const string AXIS_CYCLIC_LONGITUDINAL_SET = "AXIS_CYCLIC_LONGITUDINAL_SET";

        public const string CYCLIC_LATERAL_LEFT = "CYCLIC_LATERAL_LEFT";

        public const string CYCLIC_LATERAL_RIGHT = "CYCLIC_LATERAL_RIGHT";

        public const string CYCLIC_LONGITUDINAL_DOWN = "CYCLIC_LONGITUDINAL_DOWN";

        public const string CYCLIC_LONGITUDINAL_UP = "CYCLIC_LONGITUDINAL_UP";

      }
      public static class ThrottleControl
      {
        [Parameter(0, "Set throttle 1 or 2 (0 to 16384)")]
        public const string AXIS_HELICOPTER_THROTTLE1_SET = "AXIS_HELICOPTER_THROTTLE1_SET";

        [Parameter(0, "Set throttle 1 or 2 (0 to 16384)")]
        public const string AXIS_HELICOPTER_THROTTLE2_SET = "AXIS_HELICOPTER_THROTTLE2_SET";

        [Parameter(0, "Set all throttles (0 to 16384)")]
        public const string AXIS_HELICOPTER_THROTTLE_SET = "AXIS_HELICOPTER_THROTTLE_SET";

        public const string HELICOPTER_THROTTLE1_CUT = "HELICOPTER_THROTTLE1_CUT";

        public const string HELICOPTER_THROTTLE2_CUT = "HELICOPTER_THROTTLE2_CUT";

        [Parameter(0, "Decrement value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE1_DEC = "HELICOPTER_THROTTLE1_DEC";

        [Parameter(0, "Decrement value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE2_DEC = "HELICOPTER_THROTTLE2_DEC";

        public const string HELICOPTER_THROTTLE1_FULL = "HELICOPTER_THROTTLE1_FULL";

        public const string HELICOPTER_THROTTLE2_FULL = "HELICOPTER_THROTTLE2_FULL";

        [Parameter(0, "Increment value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE1_INC = "HELICOPTER_THROTTLE1_INC";

        [Parameter(0, "Increment value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE2_INC = "HELICOPTER_THROTTLE2_INC";

        [Parameter(0, "Throttle value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE1_SET = "HELICOPTER_THROTTLE1_SET";

        [Parameter(0, "Throttle value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE2_SET = "HELICOPTER_THROTTLE2_SET";

        public const string HELICOPTER_THROTTLE_CUT = "HELICOPTER_THROTTLE_CUT";

        [Parameter(0, "Decrement value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE_DEC = "HELICOPTER_THROTTLE_DEC";

        public const string HELICOPTER_THROTTLE_FULL = "HELICOPTER_THROTTLE_FULL";

        [Parameter(0, "Increment value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE_INC = "HELICOPTER_THROTTLE_INC";

        [Parameter(0, "Throttle value (0 to 16384)")]
        public const string HELICOPTER_THROTTLE_SET = "HELICOPTER_THROTTLE_SET";

      }
      public static class RotorControl
      {
        public const string AXIS_ROTOR_BRAKE_SET = "AXIS_ROTOR_BRAKE_SET";

        public const string ROTOR_AXIS_TAIL_ROTOR_SET = "ROTOR_AXIS_TAIL_ROTOR_SET";

        public const string ROTOR_BRAKE_OFF = "ROTOR_BRAKE_OFF";

        public const string ROTOR_BRAKE_ON = "ROTOR_BRAKE_ON";

        [Parameter(0, "True/False (1, 0)")]
        public const string ROTOR_BRAKE_SET = "ROTOR_BRAKE_SET";

        public const string ROTOR_BRAKE_TOGGLE = "ROTOR_BRAKE_TOGGLE";

        [Parameter(0, "True/False (1, 0)")]
        public const string ROTOR_CLUTCH_SWITCH_SET = "ROTOR_CLUTCH_SWITCH_SET";

        public const string ROTOR_CLUTCH_SWITCH_TOGGLE = "ROTOR_CLUTCH_SWITCH_TOGGLE";

        public const string ROTOR_GOV_SWITCH_OFF = "ROTOR_GOV_SWITCH_OFF";

        public const string ROTOR_GOV_SWITCH_ON = "ROTOR_GOV_SWITCH_ON";

        [Parameter(0, "True/False (1, 0)")]
        public const string ROTOR_GOV_SWITCH_SET = "ROTOR_GOV_SWITCH_SET";

        public const string ROTOR_GOV_SWITCH_TOGGLE = "ROTOR_GOV_SWITCH_TOGGLE";

        public const string ROTOR_LATERAL_TRIM_DEC = "ROTOR_LATERAL_TRIM_DEC";

        public const string ROTOR_LATERAL_TRIM_INC = "ROTOR_LATERAL_TRIM_INC";

        [Parameter(0, "Pitch angle (+/- 16384)")]
        public const string ROTOR_LATERAL_TRIM_SET = "ROTOR_LATERAL_TRIM_SET";

        public const string ROTOR_LONGITUDINAL_TRIM_DEC = "ROTOR_LONGITUDINAL_TRIM_DEC";

        public const string ROTOR_LONGITUDINAL_TRIM_INC = "ROTOR_LONGITUDINAL_TRIM_INC";

        [Parameter(0, "Pitch angle (+/- 16384)")]
        public const string ROTOR_LONGITUDINAL_TRIM_SET = "ROTOR_LONGITUDINAL_TRIM_SET";

        public const string ROTOR_TRIM_RESET = "ROTOR_TRIM_RESET";

        public const string TAIL_ROTOR_DECR = "TAIL_ROTOR_DECR";

        public const string TAIL_ROTOR_INCR = "TAIL_ROTOR_INCR";

      }
      public static class DeprecatedItems
      {
        [Deprecated]
        public const string ROTOR_BRAKE = "ROTOR_BRAKE";

        [Deprecated]
        public const string HELI_BEEP_INCREASE = "HELI_BEEP_INCREASE";

        [Deprecated]
        public const string HELI_BEEP_DECREASE = "HELI_BEEP_DECREASE";

      }
    }


  }
}

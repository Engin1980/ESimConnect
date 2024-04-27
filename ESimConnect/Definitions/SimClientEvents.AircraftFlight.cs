namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftFlight
    {
      public static class Ailerons
      {
        public const string AILERON_CENTER = "AILERON_CENTER";

        public const string AILERON_LEFT = "AILERON_LEFT";

        public const string AILERON_RIGHT = "AILERON_RIGHT";

        public const string AILERON_SET = "AILERON_SET";

        [Parameter(0, "Bool")]
        public const string AILERON_TRIM_DISABLED_SET = "AILERON_TRIM_DISABLED_SET";

        public const string AILERON_TRIM_DISABLED_TOGGLE = "AILERON_TRIM_DISABLED_TOGGLE";

        public const string AILERON_TRIM_LEFT = "AILERON_TRIM_LEFT";

        public const string AILERON_TRIM_RIGHT = "AILERON_TRIM_RIGHT";

        public const string AILERON_TRIM_SET = "AILERON_TRIM_SET";

        public const string AILERON_TRIM_SET_EX1 = "AILERON_TRIM_SET_EX1";

        public const string AILERONS_LEFT = "AILERONS_LEFT";

        public const string AILERONS_RIGHT = "AILERONS_RIGHT";

        public const string AXIS_AILERONS_SET = "AXIS_AILERONS_SET";

        public const string CENTER_AILER_RUDDER = "CENTER_AILER_RUDDER";

      }
      public static class Elevators
      {
        [Parameter(0, "Trim position (-16383 to 16384)")]
        public const string AXIS_ELEV_TRIM_SET = "AXIS_ELEV_TRIM_SET";

        [Parameter(0, "Position (-16383 to 16384)")]
        public const string AXIS_ELEVATOR_SET = "AXIS_ELEVATOR_SET";

        public const string ELEV_DOWN = "ELEV_DOWN";

        public const string ELEV_TRIM_DN = "ELEV_TRIM_DN";

        public const string ELEV_TRIM_UP = "ELEV_TRIM_UP";

        public const string ELEV_UP = "ELEV_UP";

        public const string ELEVATOR_DOWN = "ELEVATOR_DOWN";

        [Parameter(0, "Position (-16383 to 16384)")]
        public const string ELEVATOR_SET = "ELEVATOR_SET";

        [Parameter(0, "Bool")]
        public const string ELEVATOR_TRIM_DISABLED_SET = "ELEVATOR_TRIM_DISABLED_SET";

        public const string ELEVATOR_TRIM_DISABLED_TOGGLE = "ELEVATOR_TRIM_DISABLED_TOGGLE";

        [Parameter(0, "Trim position (-16383 to 16384)")]
        public const string ELEVATOR_TRIM_SET = "ELEVATOR_TRIM_SET";

        public const string ELEVATOR_UP = "ELEVATOR_UP";

      }
      public static class Flaps
      {
        public const string AXIS_FLAPS_SET = "AXIS_FLAPS_SET";

        public const string FLAPS_1 = "FLAPS_1";

        public const string FLAPS_2 = "FLAPS_2";

        public const string FLAPS_3 = "FLAPS_3";

        public const string FLAPS_4 = "FLAPS_4";

        public const string FLAPS_CONTINUOUS_DECR = "FLAPS_CONTINUOUS_DECR";

        public const string FLAPS_CONTINUOUS_INCR = "FLAPS_CONTINUOUS_INCR";

        public const string FLAPS_CONTINUOUS_SET = "FLAPS_CONTINUOUS_SET";

        public const string FLAPS_DECR = "FLAPS_DECR";

        public const string FLAPS_DOWN = "FLAPS_DOWN";

        public const string FLAPS_INCR = "FLAPS_INCR";

        public const string FLAPS_SET = "FLAPS_SET";

        public const string FLAPS_UP = "FLAPS_UP";

        public const string CENTER_NT361_CHECK = "CENTER_NT361_CHECK";

        public const string CHVPP_LEFT_HAT_UP = "CHVPP_LEFT_HAT_UP";

        public const string CHVPP_LEFT_HAT_DOWN = "CHVPP_LEFT_HAT_DOWN";

        public const string CHVPP_AP_ALT_WING = "CHVPP_AP_ALT_WING";

        public const string MOUSE_AS_YOKE_RESUME = "MOUSE_AS_YOKE_RESUME";

        public const string MOUSE_AS_YOKE_SUSPEND = "MOUSE_AS_YOKE_SUSPEND";

        public const string MOUSE_AS_YOKE_TOGGLE = "MOUSE_AS_YOKE_TOGGLE";

      }
      public static class Rudder
      {
        public const string AUTORUDDER_TOGGLE = "AUTORUDDER_TOGGLE";

        [Parameter(0, "Value to set")]
        public const string AXIS_RUDDER_SET = "AXIS_RUDDER_SET";

        public const string RUDDER_AXIS_MINUS = "RUDDER_AXIS_MINUS";

        public const string RUDDER_AXIS_PLUS = "RUDDER_AXIS_PLUS";

        public const string RUDDER_CENTER = "RUDDER_CENTER";

        public const string RUDDER_LEFT = "RUDDER_LEFT";

        public const string RUDDER_RIGHT = "RUDDER_RIGHT";

        [Parameter(0, "Value to set")]
        public const string RUDDER_SET = "RUDDER_SET";

        [Parameter(0, "Bool")]
        public const string RUDDER_TRIM_DISABLED_SET = "RUDDER_TRIM_DISABLED_SET";

        public const string RUDDER_TRIM_DISABLED_TOGGLE = "RUDDER_TRIM_DISABLED_TOGGLE";

        public const string RUDDER_TRIM_LEFT = "RUDDER_TRIM_LEFT";

        public const string RUDDER_TRIM_RESET = "RUDDER_TRIM_RESET";

        public const string RUDDER_TRIM_RIGHT = "RUDDER_TRIM_RIGHT";

        [Parameter(0, "Value to set")]
        public const string RUDDER_TRIM_SET = "RUDDER_TRIM_SET";

        [Parameter(0, "Value to set")]
        public const string RUDDER_TRIM_SET_EX1 = "RUDDER_TRIM_SET_EX1";

      }
      public static class AircraftSlewSystem
      {
        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_AHEAD_SET = "AXIS_SLEW_AHEAD_SET";

        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_ALT_SET = "AXIS_SLEW_ALT_SET";

        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_BANK_SET = "AXIS_SLEW_BANK_SET";

        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_HEADING_SET = "AXIS_SLEW_HEADING_SET";

        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_PITCH_SET = "AXIS_SLEW_PITCH_SET";

        [Parameter(0, "Value to set")]
        public const string AXIS_SLEW_SIDEWAYS_SET = "AXIS_SLEW_SIDEWAYS_SET";

        public const string SLEW_AHEAD_MINUS = "SLEW_AHEAD_MINUS";

        public const string SLEW_AHEAD_PLUS = "SLEW_AHEAD_PLUS";

        public const string SLEW_ALTIT_DN_FAST = "SLEW_ALTIT_DN_FAST";

        public const string SLEW_ALTIT_DN_SLOW = "SLEW_ALTIT_DN_SLOW";

        public const string SLEW_ALTIT_FREEZE = "SLEW_ALTIT_FREEZE";

        public const string SLEW_ALTIT_MINUS = "SLEW_ALTIT_MINUS";

        public const string SLEW_ALTIT_PLUS = "SLEW_ALTIT_PLUS";

        public const string SLEW_ALTIT_UP_FAST = "SLEW_ALTIT_UP_FAST";

        public const string SLEW_ALTIT_UP_SLOW = "SLEW_ALTIT_UP_SLOW";

        public const string SLEW_BANK_MINUS = "SLEW_BANK_MINUS";

        public const string SLEW_BANK_PLUS = "SLEW_BANK_PLUS";

        public const string SLEW_FREEZE = "SLEW_FREEZE";

        public const string SLEW_HEADING_MINUS = "SLEW_HEADING_MINUS";

        public const string SLEW_HEADING_PLUS = "SLEW_HEADING_PLUS";

        public const string SLEW_LEFT = "SLEW_LEFT";

        public const string SLEW_OFF = "SLEW_OFF";

        public const string SLEW_ON = "SLEW_ON";

        public const string SLEW_PITCH_DN_FAST = "SLEW_PITCH_DN_FAST";

        public const string SLEW_PITCH_DN_SLOW = "SLEW_PITCH_DN_SLOW";

        public const string SLEW_PITCH_FREEZE = "SLEW_PITCH_FREEZE";

        public const string SLEW_PITCH_MINUS = "SLEW_PITCH_MINUS";

        public const string SLEW_PITCH_PLUS = "SLEW_PITCH_PLUS";

        public const string SLEW_PITCH_UP_FAST = "SLEW_PITCH_UP_FAST";

        public const string SLEW_PITCH_UP_SLOW = "SLEW_PITCH_UP_SLOW";

        public const string SLEW_RESET = "SLEW_RESET";

        public const string SLEW_RIGHT = "SLEW_RIGHT";

        [Parameter(0, "Bool")]
        public const string SLEW_SET = "SLEW_SET";

        public const string SLEW_TOGGLE = "SLEW_TOGGLE";

      }
      public static class Spoilers
      {
        [Parameter(0, "Positon (0 - 1)")]
        public const string AXIS_SPOILER_SET = "AXIS_SPOILER_SET";

        public const string SPOILERS_ARM_OFF = "SPOILERS_ARM_OFF";

        public const string SPOILERS_ARM_ON = "SPOILERS_ARM_ON";

        [Parameter(0, "Bool")]
        public const string SPOILERS_ARM_SET = "SPOILERS_ARM_SET";

        public const string SPOILERS_ARM_TOGGLE = "SPOILERS_ARM_TOGGLE";

        public const string SPOILERS_DEC = "SPOILERS_DEC";

        public const string SPOILERS_INC = "SPOILERS_INC";

        public const string SPOILERS_OFF = "SPOILERS_OFF";

        public const string SPOILERS_ON = "SPOILERS_ON";

        [Parameter(0, "Position (0 to 16383)")]
        public const string SPOILERS_SET = "SPOILERS_SET";

        public const string SPOILERS_TOGGLE = "SPOILERS_TOGGLE";

      }
      public static class ConcordeDepre
      {
        public const string INC_CONCORDE_NOSE_VISOR = "INC_CONCORDE_NOSE_VISOR";

        public const string DEC_CONCORDE_NOSE_VISOR = "DEC_CONCORDE_NOSE_VISOR";

        public const string CONCORDE_NOSE_VISOR_FULL_EXT = "CONCORDE_NOSE_VISOR_FULL_EXT";

        public const string CONCORDE_NOSE_VISOR_FULL_RET = "CONCORDE_NOSE_VISOR_FULL_RET";

      }
    }


  }
}

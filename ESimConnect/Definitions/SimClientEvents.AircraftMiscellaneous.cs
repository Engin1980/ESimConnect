namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftMiscellaneous
    {
      public static class AircraftFailures
      {
        public const string MASTER_CAUTION_ACKNOWLEDGE = "MASTER_CAUTION_ACKNOWLEDGE";

        public const string MASTER_CAUTION_OFF = "MASTER_CAUTION_OFF";

        public const string MASTER_CAUTION_ON = "MASTER_CAUTION_ON";

        public const string MASTER_CAUTION_SET = "MASTER_CAUTION_SET";

        public const string MASTER_CAUTION_TOGGLE = "MASTER_CAUTION_TOGGLE";

        public const string MASTER_WARNING_ACKNOWLEDGE = "MASTER_WARNING_ACKNOWLEDGE";

        public const string MASTER_WARNING_OFF = "MASTER_WARNING_OFF";

        public const string MASTER_WARNING_ON = "MASTER_WARNING_ON";

        public const string MASTER_WARNING_SET = "MASTER_WARNING_SET";

        public const string MASTER_WARNING_TOGGLE = "MASTER_WARNING_TOGGLE";

        public const string TOGGLE_ELECTRICAL_FAILURE = "TOGGLE_ELECTRICAL_FAILURE";

        public const string TOGGLE_ENGINE1_FAILURE = "TOGGLE_ENGINE1_FAILURE";

        public const string TOGGLE_ENGINE2_FAILURE = "TOGGLE_ENGINE2_FAILURE";

        public const string TOGGLE_ENGINE3_FAILURE = "TOGGLE_ENGINE3_FAILURE";

        public const string TOGGLE_ENGINE4_FAILURE = "TOGGLE_ENGINE4_FAILURE";

        public const string TOGGLE_HYDRAULIC_FAILURE = "TOGGLE_HYDRAULIC_FAILURE";

        public const string TOGGLE_LEFT_BRAKE_FAILURE = "TOGGLE_LEFT_BRAKE_FAILURE";

        public const string TOGGLE_PITOT_BLOCKAGE = "TOGGLE_PITOT_BLOCKAGE";

        public const string TOGGLE_RIGHT_BRAKE_FAILURE = "TOGGLE_RIGHT_BRAKE_FAILURE";

        public const string TOGGLE_STATIC_PORT_BLOCKAGE = "TOGGLE_STATIC_PORT_BLOCKAGE";

        public const string TOGGLE_TOTAL_BRAKE_FAILURE = "TOGGLE_TOTAL_BRAKE_FAILURE";

        public const string TOGGLE_VACUUM_FAILURE = "TOGGLE_VACUUM_FAILURE";

      }
      public static class LandingGear
      {
        public const string ANTISKID_BRAKES_TOGGLE = "ANTISKID_BRAKES_TOGGLE";

        [Deprecated]
        public const string AXIS_IND_SET = "AXIS_IND_SET";

        [Parameter(0, "the brake position from -16383 to 16383")]
        public const string AXIS_LEFT_BRAKE_LINEAR_SET = "AXIS_LEFT_BRAKE_LINEAR_SET";

        [Parameter(0, "the brake position from -16383 to 16383")]
        public const string AXIS_LEFT_BRAKE_SET = "AXIS_LEFT_BRAKE_SET";

        [Parameter(0, "the brake position from -16383 to 16383")]
        public const string AXIS_RIGHT_BRAKE_LINEAR_SET = "AXIS_RIGHT_BRAKE_LINEAR_SET";

        [Parameter(0, "the brake position from -16383 to 16383")]
        public const string AXIS_RIGHT_BRAKE_SET = "AXIS_RIGHT_BRAKE_SET";

        public const string BRAKES = "BRAKES";

        public const string BRAKES_LEFT = "BRAKES_LEFT";

        public const string BRAKES_RIGHT = "BRAKES_RIGHT";

        public const string GEAR_DOWN = "GEAR_DOWN";

        public const string GEAR_EMERGENCY_HANDLE_TOGGLE = "GEAR_EMERGENCY_HANDLE_TOGGLE";

        public const string GEAR_PUMP = "GEAR_PUMP";

        [Parameter(0, "Position")]
        public const string GEAR_SET = "GEAR_SET";

        public const string GEAR_TOGGLE = "GEAR_TOGGLE";

        public const string GEAR_UP = "GEAR_UP";

        public const string PARKING_BRAKES = "PARKING_BRAKES";

        [Parameter(0, "Bool")]
        public const string PARKING_BRAKE_SET = "PARKING_BRAKE_SET";

        public const string RETRACT_FLOAT_SWITCH_DEC = "RETRACT_FLOAT_SWITCH_DEC";

        public const string RETRACT_FLOAT_SWITCH_INC = "RETRACT_FLOAT_SWITCH_INC";

      }
      public static class Gliders
      {
        public const string MAC_CREADY_SETTING_DEC = "MAC_CREADY_SETTING_DEC";

        public const string MAC_CREADY_SETTING_INC = "MAC_CREADY_SETTING_INC";

        [Parameter(0, "MacCready value in m/s")]
        public const string MAC_CREADY_SETTING_SET = "MAC_CREADY_SETTING_SET";

        [Parameter(0, "TRUE/FALSE to set or retract the tailhook.")]
        public const string SET_TAIL_HOOK_HANDLE = "SET_TAIL_HOOK_HANDLE";

        public const string TOGGLE_TAIL_HOOK_HANDLE = "TOGGLE_TAIL_HOOK_HANDLE";

        public const string TOW_PLANE_RELEASE = "TOW_PLANE_RELEASE";

        public const string TOW_PLANE_REQUEST = "TOW_PLANE_REQUEST";

      }
      public static class AircraftMiscellaneousSystems
      {
        public const string ANNUNCIATOR_SWITCH_OFF = "ANNUNCIATOR_SWITCH_OFF";

        public const string ANNUNCIATOR_SWITCH_ON = "ANNUNCIATOR_SWITCH_ON";

        public const string ANNUNCIATOR_SWITCH_TOGGLE = "ANNUNCIATOR_SWITCH_TOGGLE";

        public const string BAIL_OUT = "BAIL_OUT";

        public const string BLEED_AIR_SOURCE_CONTROL_INC = "BLEED_AIR_SOURCE_CONTROL_INC";

        public const string BLEED_AIR_SOURCE_CONTROL_DEC = "BLEED_AIR_SOURCE_CONTROL_DEC";

        [Parameter(0, "source value")]
        public const string BLEED_AIR_SOURCE_CONTROL_SET = "BLEED_AIR_SOURCE_CONTROL_SET";

        public const string CABIN_NO_SMOKING_ALERT_SWITCH_TOGGLE = "CABIN_NO_SMOKING_ALERT_SWITCH_TOGGLE";

        public const string CABIN_SEATBELTS_ALERT_SWITCH_TOGGLE = "CABIN_SEATBELTS_ALERT_SWITCH_TOGGLE";

        public const string DECREASE_DECISION_HEIGHT = "DECREASE_DECISION_HEIGHT";

        public const string INCREASE_DECISION_HEIGHT = "INCREASE_DECISION_HEIGHT";

        [Parameter(0, "height (m)")]
        public const string DECISION_HEIGHT_SET = "DECISION_HEIGHT_SET";

        [Parameter(0, "amount")]
        public const string DECREASE_DECISION_ALTITUDE_MSL = "DECREASE_DECISION_ALTITUDE_MSL";

        [Parameter(0, "amount")]
        public const string INCREASE_DECISION_ALTITUDE_MSL = "INCREASE_DECISION_ALTITUDE_MSL";

        [Parameter(0, "height (m)")]
        public const string SET_DECISION_ALTITUDE_MSL = "SET_DECISION_ALTITUDE_MSL";

        [Parameter(0, "combined index (see description)")]
        public const string EXTINGUISH_ENGINE_FIRE = "EXTINGUISH_ENGINE_FIRE";

        public const string HORN_TRIGGER = "HORN_TRIGGER";

        [Parameter(0, "TRUE/FALSE to set or the hydraulic switch on/off")]
        public const string HYDRAULIC_SWITCH_TOGGLE = "HYDRAULIC_SWITCH_TOGGLE";

        public const string PITOT_HEAT_OFF = "PITOT_HEAT_OFF";

        public const string PITOT_HEAT_ON = "PITOT_HEAT_ON";

        [Parameter(0, "TRUE/FALSE to set or the pitot heat switch on/off")]
        [Parameter(1, "Pitot index")]
        public const string PITOT_HEAT_SET = "PITOT_HEAT_SET";

        public const string PITOT_HEAT_TOGGLE = "PITOT_HEAT_TOGGLE";

        public const string TOGGLE_PUSHBACK = "TOGGLE_PUSHBACK";

        public const string RELEASE_DROPPABLE_OBJECTS = "RELEASE_DROPPABLE_OBJECTS";

        public const string SCRIPT_EVENT_1 = "SCRIPT_EVENT_1";

        public const string SCRIPT_EVENT_2 = "SCRIPT_EVENT_2";

        public const string SEE_OWN_AC_OFF = "SEE_OWN_AC_OFF";

        public const string SEE_OWN_AC_ON = "SEE_OWN_AC_ON";

        public const string SEE_OWN_AC_SET = "SEE_OWN_AC_SET";

        public const string SEE_OWN_AC_TOGGLE = "SEE_OWN_AC_TOGGLE";

        [Parameter(0, "TRUE/FALSE to fold or unfold wings.")]
        public const string SET_WING_FOLD = "SET_WING_FOLD";

        public const string SMOKE_OFF = "SMOKE_OFF";

        public const string SMOKE_ON = "SMOKE_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable the smoke system")]
        public const string SMOKE_SET = "SMOKE_SET";

        public const string SMOKE_TOGGLE = "SMOKE_TOGGLE";

        public const string TOGGLE_ALTERNATE_STATIC = "TOGGLE_ALTERNATE_STATIC";

        public const string TOGGLE_AIRCRAFT_EXIT = "TOGGLE_AIRCRAFT_EXIT";

        public const string TOGGLE_AIRCRAFT_EXIT_FAST = "TOGGLE_AIRCRAFT_EXIT_FAST";

        public const string TOGGLE_STRUCTURAL_DEICE = "TOGGLE_STRUCTURAL_DEICE";

        public const string TOGGLE_TAILWHEEL_LOCK = "TOGGLE_TAILWHEEL_LOCK";

        [Parameter(0, "valve index from 1 to n where n is the NumberOfReleaseValves defined in the systems.cfg file.")]
        public const string TOGGLE_WATER_BALLAST_VALVE = "TOGGLE_WATER_BALLAST_VALVE";

        public const string TOGGLE_WATER_RUDDER = "TOGGLE_WATER_RUDDER";

        public const string TOGGLE_WING_FOLD = "TOGGLE_WING_FOLD";

        public const string TUG_DISABLE = "TUG_DISABLE";

        [Parameter(0, "Heading (0 - 4294967295")]
        public const string TUG_HEADING = "TUG_HEADING";

        [Parameter(0, "Speed (ft / s)")]
        public const string TUG_SPEED = "TUG_SPEED";

        public const string WAR_EMERGENCY_POWER = "WAR_EMERGENCY_POWER";

      }
      public static class SimControl
      {
        public const string BACK_TO_FLY = "BACK_TO_FLY";

      }
      public static class CabinPressurization
      {
        public const string PRESSURIZATION_PRESSURE_ALT_INC = "PRESSURIZATION_PRESSURE_ALT_INC";

        public const string PRESSURIZATION_PRESSURE_ALT_DEC = "PRESSURIZATION_PRESSURE_ALT_DEC";

        public const string PRESSURIZATION_CLIMB_RATE_INC = "PRESSURIZATION_CLIMB_RATE_INC";

        public const string PRESSURIZATION_CLIMB_RATE_DEC = "PRESSURIZATION_CLIMB_RATE_DEC";

        [Deprecated]
        [Parameter(0, "Value")]
        public const string PRESSURIZATION_CLIMB_RATE_SET = "PRESSURIZATION_CLIMB_RATE_SET";

        public const string PRESSURIZATION_PRESSURE_DUMP_SWITCH = "PRESSURIZATION_PRESSURE_DUMP_SWITCH";

      }
      public static class NoseWheelSteering
      {
        [Parameter(0, "Steering position (+/-16384)")]
        public const string AXIS_STEERING_SET = "AXIS_STEERING_SET";

        [Parameter(0, "Steering position (+/-16383)")]
        public const string NOSE_WHEEL_STEERING_LIMIT_SET = "NOSE_WHEEL_STEERING_LIMIT_SET";

        public const string STEERING_INC = "STEERING_INC";

        public const string STEERING_DEC = "STEERING_DEC";

        [Parameter(0, "Steering position (+/-16383)")]
        public const string STEERING_SET = "STEERING_SET";

      }
      public static class WindshieldDeIce
      {
        public const string WINDSHIELD_DEICE_OFF = "WINDSHIELD_DEICE_OFF";

        public const string WINDSHIELD_DEICE_ON = "WINDSHIELD_DEICE_ON";

        [Parameter(0, "Bool")]
        public const string WINDSHIELD_DEICE_SET = "WINDSHIELD_DEICE_SET";

        public const string WINDSHIELD_DEICE_TOGGLE = "WINDSHIELD_DEICE_TOGGLE";

      }
      public static class CatapultLaunches
      {
        public const string TAKEOFF_ASSIST_ARM_TOGGLE = "TAKEOFF_ASSIST_ARM_TOGGLE";

        [Parameter(0, "Bool")]
        public const string TAKEOFF_ASSIST_ARM_SET = "TAKEOFF_ASSIST_ARM_SET";

        public const string TAKEOFF_ASSIST_FIRE = "TAKEOFF_ASSIST_FIRE";

        public const string TOGGLE_LAUNCH_BAR_SWITCH = "TOGGLE_LAUNCH_BAR_SWITCH";

        [Parameter(0, "Bool")]
        public const string SET_LAUNCH_BAR_SWITCH = "SET_LAUNCH_BAR_SWITCH";

      }
      public static class SlingsAndHoists
      {
        [Deprecated]
        public const string SLING_PICKUP_RELEASE = "SLING_PICKUP_RELEASE";

        [Deprecated]
        public const string HOIST_SWITCH_EXTEND = "HOIST_SWITCH_EXTEND";

        [Deprecated]
        public const string HOIST_SWITCH_RETRACT = "HOIST_SWITCH_RETRACT";

        [Deprecated]
        public const string HOIST_SWITCH_SELECT = "HOIST_SWITCH_SELECT";

        [Deprecated]
        public const string HOIST_SWITCH_SET = "HOIST_SWITCH_SET";

        [Deprecated]
        public const string HOIST_DEPLOY_TOGGLE = "HOIST_DEPLOY_TOGGLE";

        [Deprecated]
        public const string HOIST_DEPLOY_SET = "HOIST_DEPLOY_SET";

      }
      public static class Weapons
      {
        [Deprecated]
        public const string GUNSIGHT_SEL = "GUNSIGHT_SEL";

        [Deprecated]
        public const string GUNSIGHT_TOGGLE = "GUNSIGHT_TOGGLE";

        [Deprecated]
        public const string FIRE_ALL_GUNS = "FIRE_ALL_GUNS";

        [Deprecated]
        public const string FIRE_PRIMARY_GUNS = "FIRE_PRIMARY_GUNS";

        [Deprecated]
        public const string FIRE_SECONDARY_GUNS = "FIRE_SECONDARY_GUNS";

        [Deprecated]
        public const string STOP_PRIMARY_GUNS = "STOP_PRIMARY_GUNS";

        [Deprecated]
        public const string STOP_SECONDARY_GUNS = "STOP_SECONDARY_GUNS";

        [Deprecated]
        public const string STOP_ALL_GUNS = "STOP_ALL_GUNS";

      }
    }


  }
}

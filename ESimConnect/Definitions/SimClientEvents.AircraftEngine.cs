using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftEngine
    {
      public static class AntiIce
      {
        [Parameter(0, "Position (0 - 16384)")]
        public const string ANTI_ICE_GRADUAL_SET = "ANTI_ICE_GRADUAL_SET";

        [Parameter(0, "Position (0 - 16384)")]
        public const string ANTI_ICE_GRADUAL_SET_ENG1 = "ANTI_ICE_GRADUAL_SET_ENG1";

        [Parameter(0, "Position (0 - 16384)")]
        public const string ANTI_ICE_GRADUAL_SET_ENG2 = "ANTI_ICE_GRADUAL_SET_ENG2";

        [Parameter(0, "Position (0 - 16384)")]
        public const string ANTI_ICE_GRADUAL_SET_ENG3 = "ANTI_ICE_GRADUAL_SET_ENG3";

        [Parameter(0, "Position (0 - 16384)")]
        public const string ANTI_ICE_GRADUAL_SET_ENG4 = "ANTI_ICE_GRADUAL_SET_ENG4";

        public const string ANTI_ICE_ON = "ANTI_ICE_ON";

        public const string ANTI_ICE_OFF = "ANTI_ICE_OFF";

        [Parameter(0, "Bool")]
        public const string ANTI_ICE_SET = "ANTI_ICE_SET";

        [Parameter(0, "Bool")]
        public const string ANTI_ICE_SET_ENG1 = "ANTI_ICE_SET_ENG1";

        [Parameter(0, "Bool")]
        public const string ANTI_ICE_SET_ENG2 = "ANTI_ICE_SET_ENG2";

        [Parameter(0, "Bool")]
        public const string ANTI_ICE_SET_ENG3 = "ANTI_ICE_SET_ENG3";

        [Parameter(0, "Bool")]
        public const string ANTI_ICE_SET_ENG4 = "ANTI_ICE_SET_ENG4";

        public const string ANTI_ICE_TOGGLE = "ANTI_ICE_TOGGLE";

        public const string ANTI_ICE_TOGGLE_ENG1 = "ANTI_ICE_TOGGLE_ENG1";

        public const string ANTI_ICE_TOGGLE_ENG2 = "ANTI_ICE_TOGGLE_ENG2";

        public const string ANTI_ICE_TOGGLE_ENG3 = "ANTI_ICE_TOGGLE_ENG3";

        public const string ANTI_ICE_TOGGLE_ENG4 = "ANTI_ICE_TOGGLE_ENG4";

      }
      public static class ConditionLever
      {
        [Parameter(0, "Axis value")]
        public const string AXIS_CONDITION_LEVER_1_SET = "AXIS_CONDITION_LEVER_1_SET";

        [Parameter(0, "Axis value")]
        public const string AXIS_CONDITION_LEVER_2_SET = "AXIS_CONDITION_LEVER_2_SET";

        [Parameter(0, "Axis value")]
        public const string AXIS_CONDITION_LEVER_3_SET = "AXIS_CONDITION_LEVER_3_SET";

        [Parameter(0, "Axis value")]
        public const string AXIS_CONDITION_LEVER_4_SET = "AXIS_CONDITION_LEVER_4_SET";

        [Parameter(0, "Engine index")]
        public const string CONDITION_LEVER_DEC = "CONDITION_LEVER_DEC";

        [Parameter(0, "Engine index")]
        public const string CONDITION_LEVER_INC = "CONDITION_LEVER_INC";

        [Parameter(0, "Position")]
        public const string CONDITION_LEVER_SET = "CONDITION_LEVER_SET";

        public const string CONDITION_LEVER_1_DEC = "CONDITION_LEVER_1_DEC";

        public const string CONDITION_LEVER_2_DEC = "CONDITION_LEVER_2_DEC";

        public const string CONDITION_LEVER_3_DEC = "CONDITION_LEVER_3_DEC";

        public const string CONDITION_LEVER_4_DEC = "CONDITION_LEVER_4_DEC";

        public const string CONDITION_LEVER_1_INC = "CONDITION_LEVER_1_INC";

        public const string CONDITION_LEVER_2_INC = "CONDITION_LEVER_2_INC";

        public const string CONDITION_LEVER_3_INC = "CONDITION_LEVER_3_INC";

        public const string CONDITION_LEVER_4_INC = "CONDITION_LEVER_4_INC";

        [Parameter(0, "Position")]
        public const string CONDITION_LEVER_1_SET = "CONDITION_LEVER_1_SET";

        [Parameter(0, "Position")]
        public const string CONDITION_LEVER_2_SET = "CONDITION_LEVER_2_SET";

        [Parameter(0, "Position")]
        public const string CONDITION_LEVER_3_SET = "CONDITION_LEVER_3_SET";

        [Parameter(0, "Position")]
        public const string CONDITION_LEVER_4_SET = "CONDITION_LEVER_4_SET";

      }
      public static class Fuel
      {
        public const string AXIS_MIXTURE_SET = "AXIS_MIXTURE_SET";

        public const string AXIS_MIXTURE1_SET = "AXIS_MIXTURE1_SET";

        public const string AXIS_MIXTURE2_SET = "AXIS_MIXTURE2_SET";

        public const string AXIS_MIXTURE3_SET = "AXIS_MIXTURE3_SET";

        public const string AXIS_MIXTURE4_SET = "AXIS_MIXTURE4_SET";

        public const string FUEL_PUMP = "FUEL_PUMP";

        public const string MIXTURE_DECR = "MIXTURE_DECR";

        public const string MIXTURE1_DECR = "MIXTURE1_DECR";

        public const string MIXTURE2_DECR = "MIXTURE2_DECR";

        public const string MIXTURE3_DECR = "MIXTURE3_DECR";

        public const string MIXTURE4_DECR = "MIXTURE4_DECR";

        public const string MIXTURE_DECR_SMALL = "MIXTURE_DECR_SMALL";

        public const string MIXTURE1_DECR_SMALL = "MIXTURE1_DECR_SMALL";

        public const string MIXTURE2_DECR_SMALL = "MIXTURE2_DECR_SMALL";

        public const string MIXTURE3_DECR_SMALL = "MIXTURE3_DECR_SMALL";

        public const string MIXTURE4_DECR_SMALL = "MIXTURE4_DECR_SMALL";

        public const string MIXTURE_INCR = "MIXTURE_INCR";

        public const string MIXTURE1_INCR = "MIXTURE1_INCR";

        public const string MIXTURE2_INCR = "MIXTURE2_INCR";

        public const string MIXTURE3_INCR = "MIXTURE3_INCR";

        public const string MIXTURE4_INCR = "MIXTURE4_INCR";

        public const string MIXTURE_INCR_SMALL = "MIXTURE_INCR_SMALL";

        public const string MIXTURE1_INCR_SMALL = "MIXTURE1_INCR_SMALL";

        public const string MIXTURE2_INCR_SMALL = "MIXTURE2_INCR_SMALL";

        public const string MIXTURE3_INCR_SMALL = "MIXTURE3_INCR_SMALL";

        public const string MIXTURE4_INCR_SMALL = "MIXTURE4_INCR_SMALL";

        public const string MIXTURE_LEAN = "MIXTURE_LEAN";

        public const string MIXTURE1_LEAN = "MIXTURE1_LEAN";

        public const string MIXTURE2_LEAN = "MIXTURE2_LEAN";

        public const string MIXTURE3_LEAN = "MIXTURE3_LEAN";

        public const string MIXTURE4_LEAN = "MIXTURE4_LEAN";

        public const string MIXTURE_RICH = "MIXTURE_RICH";

        public const string MIXTURE1_RICH = "MIXTURE1_RICH";

        public const string MIXTURE2_RICH = "MIXTURE2_RICH";

        public const string MIXTURE3_RICH = "MIXTURE3_RICH";

        public const string MIXTURE4_RICH = "MIXTURE4_RICH";

        public const string MIXTURE_SET = "MIXTURE_SET";

        public const string MIXTURE_SET_BEST = "MIXTURE_SET_BEST";

        public const string MIXTURE1_SET = "MIXTURE1_SET";

        public const string MIXTURE2_SET = "MIXTURE2_SET";

        public const string MIXTURE3_SET = "MIXTURE3_SET";

        public const string MIXTURE4_SET = "MIXTURE4_SET";

        public const string SET_FUEL_VALVE_ENG1 = "SET_FUEL_VALVE_ENG1";

        public const string SET_FUEL_VALVE_ENG2 = "SET_FUEL_VALVE_ENG2";

        public const string SET_FUEL_VALVE_ENG3 = "SET_FUEL_VALVE_ENG3";

        public const string SET_FUEL_VALVE_ENG4 = "SET_FUEL_VALVE_ENG4";

        public const string SHUTOFF_VALVE_TOGGLE = "SHUTOFF_VALVE_TOGGLE";

        public const string SHUTOFF_VALVE_ON = "SHUTOFF_VALVE_ON";

        public const string SHUTOFF_VALVE_OFF = "SHUTOFF_VALVE_OFF";

        public const string TOGGLE_ELECT_FUEL_PUMP = "TOGGLE_ELECT_FUEL_PUMP";

        public const string TOGGLE_ELECT_FUEL_PUMP1 = "TOGGLE_ELECT_FUEL_PUMP1";

        public const string TOGGLE_ELECT_FUEL_PUMP2 = "TOGGLE_ELECT_FUEL_PUMP2";

        public const string TOGGLE_ELECT_FUEL_PUMP3 = "TOGGLE_ELECT_FUEL_PUMP3";

        public const string TOGGLE_ELECT_FUEL_PUMP4 = "TOGGLE_ELECT_FUEL_PUMP4";

        public const string TOGGLE_FUEL_VALVE_ALL = "TOGGLE_FUEL_VALVE_ALL";

        public const string TOGGLE_FUEL_VALVE_ENG1 = "TOGGLE_FUEL_VALVE_ENG1";

        public const string TOGGLE_FUEL_VALVE_ENG2 = "TOGGLE_FUEL_VALVE_ENG2";

        public const string TOGGLE_FUEL_VALVE_ENG3 = "TOGGLE_FUEL_VALVE_ENG3";

        public const string TOGGLE_FUEL_VALVE_ENG4 = "TOGGLE_FUEL_VALVE_ENG4";

      }
      public static class Magneto
      {
        [Parameter(0, "Magneto index")]
        public const string MAGNETO = "MAGNETO";

        [Parameter(0, "Magneto index")]
        public const string MAGNETO_BOTH = "MAGNETO_BOTH";

        public const string MAGNETO1_BOTH = "MAGNETO1_BOTH";

        public const string MAGNETO2_BOTH = "MAGNETO2_BOTH";

        public const string MAGNETO3_BOTH = "MAGNETO3_BOTH";

        public const string MAGNETO4_BOTH = "MAGNETO4_BOTH";

        public const string MAGNETO_DECR = "MAGNETO_DECR";

        public const string MAGNETO1_DECR = "MAGNETO1_DECR";

        public const string MAGNETO2_DECR = "MAGNETO2_DECR";

        public const string MAGNETO3_DECR = "MAGNETO3_DECR";

        public const string MAGNETO4_DECR = "MAGNETO4_DECR";

        public const string MAGNETO_INCR = "MAGNETO_INCR";

        public const string MAGNETO1_INCR = "MAGNETO1_INCR";

        public const string MAGNETO2_INCR = "MAGNETO2_INCR";

        public const string MAGNETO3_INCR = "MAGNETO3_INCR";

        public const string MAGNETO4_INCR = "MAGNETO4_INCR";

        public const string MAGNETO_LEFT = "MAGNETO_LEFT";

        public const string MAGNETO1_LEFT = "MAGNETO1_LEFT";

        public const string MAGNETO2_LEFT = "MAGNETO2_LEFT";

        public const string MAGNETO3_LEFT = "MAGNETO3_LEFT";

        public const string MAGNETO4_LEFT = "MAGNETO4_LEFT";

        public const string MAGNETO_OFF = "MAGNETO_OFF";

        public const string MAGNETO1_OFF = "MAGNETO1_OFF";

        public const string MAGNETO2_OFF = "MAGNETO2_OFF";

        public const string MAGNETO3_OFF = "MAGNETO3_OFF";

        public const string MAGNETO4_OFF = "MAGNETO4_OFF";

        public const string MAGNETO_RIGHT = "MAGNETO_RIGHT";

        public const string MAGNETO1_RIGHT = "MAGNETO1_RIGHT";

        public const string MAGNETO2_RIGHT = "MAGNETO2_RIGHT";

        public const string MAGNETO3_RIGHT = "MAGNETO3_RIGHT";

        public const string MAGNETO4_RIGHT = "MAGNETO4_RIGHT";

        public const string MAGNETO_START = "MAGNETO_START";

        public const string MAGNETO1_START = "MAGNETO1_START";

        public const string MAGNETO2_START = "MAGNETO2_START";

        public const string MAGNETO3_START = "MAGNETO3_START";

        public const string MAGNETO4_START = "MAGNETO4_START";

      }
      public static class Miscellaneous
      {
        [Parameter(0, "position from 0 to 16983")]
        public const string COWLFLAP1_SET = "COWLFLAP1_SET";

        [Parameter(0, "position from 0 to 16983")]
        public const string COWLFLAP2_SET = "COWLFLAP2_SET";

        [Parameter(0, "position from 0 to 16983")]
        public const string COWLFLAP3_SET = "COWLFLAP3_SET";

        [Parameter(0, "position from 0 to 16983")]
        public const string COWLFLAP4_SET = "COWLFLAP4_SET";

        public const string DEC_COWL_FLAPS = "DEC_COWL_FLAPS";

        public const string DEC_COWL_FLAPS1 = "DEC_COWL_FLAPS1";

        public const string DEC_COWL_FLAPS2 = "DEC_COWL_FLAPS2";

        public const string DEC_COWL_FLAPS3 = "DEC_COWL_FLAPS3";

        public const string DEC_COWL_FLAPS4 = "DEC_COWL_FLAPS4";

        public const string ENGINE = "ENGINE";

        public const string ENGINE_AUTO_START = "ENGINE_AUTO_START";

        public const string ENGINE_AUTO_SHUTDOWN = "ENGINE_AUTO_SHUTDOWN";

        [Parameter(0, "The engine index to target (from 1 to 4)")]
        [Parameter(1, "Set to TRUE/FALSE to set the engine as source (TRUE) or not (FALSE)")]
        public const string ENGINE_BLEED_AIR_SOURCE_SET = "ENGINE_BLEED_AIR_SOURCE_SET";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        public const string ENGINE_BLEED_AIR_SOURCE_TOGGLE = "ENGINE_BLEED_AIR_SOURCE_TOGGLE";

        public const string ENGINE_MASTER_SET = "ENGINE_MASTER_SET";

        public const string ENGINE_MASTER_1_SET = "ENGINE_MASTER_1_SET";

        public const string ENGINE_MASTER_2_SET = "ENGINE_MASTER_2_SET";

        public const string ENGINE_MASTER_3_SET = "ENGINE_MASTER_3_SET";

        public const string ENGINE_MASTER_4_SET = "ENGINE_MASTER_4_SET";

        public const string ENGINE_MASTER_TOGGLE = "ENGINE_MASTER_TOGGLE";

        public const string ENGINE_MASTER_1_TOGGLE = "ENGINE_MASTER_1_TOGGLE";

        public const string ENGINE_MASTER_2_TOGGLE = "ENGINE_MASTER_2_TOGGLE";

        public const string ENGINE_MASTER_3_TOGGLE = "ENGINE_MASTER_3_TOGGLE";

        public const string ENGINE_MASTER_4_TOGGLE = "ENGINE_MASTER_4_TOGGLE";

        public const string ENGINE_MODE_CRANK_SET = "ENGINE_MODE_CRANK_SET";

        public const string ENGINE_MODE_NORM_SET = "ENGINE_MODE_NORM_SET";

        public const string ENGINE_MODE_IGN_START = "ENGINE_MODE_IGN_START";

        public const string ENGINE_PRIMER = "ENGINE_PRIMER";

        public const string INC_COWL_FLAPS = "INC_COWL_FLAPS";

        public const string INC_COWL_FLAPS1 = "INC_COWL_FLAPS1";

        public const string INC_COWL_FLAPS2 = "INC_COWL_FLAPS2";

        public const string INC_COWL_FLAPS3 = "INC_COWL_FLAPS3";

        public const string INC_COWL_FLAPS4 = "INC_COWL_FLAPS4";

        public const string OIL_COOLING_FLAPS_DOWN = "OIL_COOLING_FLAPS_DOWN";

        public const string OIL_COOLING_FLAPS_SET = "OIL_COOLING_FLAPS_SET";

        public const string OIL_COOLING_FLAPS_TOGGLE = "OIL_COOLING_FLAPS_TOGGLE";

        public const string OIL_COOLING_FLAPS_UP = "OIL_COOLING_FLAPS_UP";

        public const string RADIATOR_COOLING_FLAPS_DOWN = "RADIATOR_COOLING_FLAPS_DOWN";

        public const string RADIATOR_COOLING_FLAPS_SET = "RADIATOR_COOLING_FLAPS_SET";

        public const string RADIATOR_COOLING_FLAPS_TOGGLE = "RADIATOR_COOLING_FLAPS_TOGGLE";

        public const string RADIATOR_COOLING_FLAPS_UP = "RADIATOR_COOLING_FLAPS_UP";

        public const string TOGGLE_MASTER_IGNITION_SWITCH = "TOGGLE_MASTER_IGNITION_SWITCH";

        public const string TOGGLE_PRIMER = "TOGGLE_PRIMER";

        public const string TOGGLE_PRIMER1 = "TOGGLE_PRIMER1";

        public const string TOGGLE_PRIMER2 = "TOGGLE_PRIMER2";

        public const string TOGGLE_PRIMER3 = "TOGGLE_PRIMER3";

        public const string TOGGLE_PRIMER4 = "TOGGLE_PRIMER4";

        public const string TOGGLE_AFTERBURNER = "TOGGLE_AFTERBURNER";

        public const string TOGGLE_AFTERBURNER1 = "TOGGLE_AFTERBURNER1";

        public const string TOGGLE_AFTERBURNER2 = "TOGGLE_AFTERBURNER2";

        public const string TOGGLE_AFTERBURNER3 = "TOGGLE_AFTERBURNER3";

        public const string TOGGLE_AFTERBURNER4 = "TOGGLE_AFTERBURNER4";

      }
      public static class Propeller
      {
        public const string AXIS_PROPELLER_SET = "AXIS_PROPELLER_SET";

        public const string AXIS_PROPELLER1_SET = "AXIS_PROPELLER1_SET";

        public const string AXIS_PROPELLER2_SET = "AXIS_PROPELLER2_SET";

        public const string AXIS_PROPELLER3_SET = "AXIS_PROPELLER3_SET";

        public const string AXIS_PROPELLER4_SET = "AXIS_PROPELLER4_SET";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        public const string PROP_FORCE_BETA_OFF = "PROP_FORCE_BETA_OFF";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        public const string PROP_FORCE_BETA_ON = "PROP_FORCE_BETA_ON";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        [Parameter(1, "Whether or not to force the prop beta (Boolean).")]
        public const string PROP_FORCE_BETA_SET = "PROP_FORCE_BETA_SET";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        public const string PROP_FORCE_BETA_TOGGLE = "PROP_FORCE_BETA_TOGGLE";

        [Parameter(0, "The engine index to target (from 1 to 4, or 0 for all engines)")]
        [Parameter(1, "&nbsp;The angle that the prop should be forced to. This is stored as the 16k representation of an angle between -180 degrees and + 180 degrees")]
        public const string PROP_FORCE_BETA_VALUE_SET = "PROP_FORCE_BETA_VALUE_SET";

        public const string PROP_LOCK_OFF = "PROP_LOCK_OFF";

        public const string PROP_LOCK_ON = "PROP_LOCK_ON";

        [Parameter(0, "True/False&nbsp;(1, 0)")]
        public const string PROP_LOCK_SET = "PROP_LOCK_SET";

        public const string PROP_LOCK_TOGGLE = "PROP_LOCK_TOGGLE";

        public const string PROP_PITCH_AXIS_SET_EX1 = "PROP_PITCH_AXIS_SET_EX1";

        public const string PROP_PITCH1_AXIS_SET_EX1 = "PROP_PITCH1_AXIS_SET_EX1";

        public const string PROP_PITCH2_AXIS_SET_EX1 = "PROP_PITCH2_AXIS_SET_EX1";

        public const string PROP_PITCH3_AXIS_SET_EX1 = "PROP_PITCH3_AXIS_SET_EX1";

        public const string PROP_PITCH4_AXIS_SET_EX1 = "PROP_PITCH4_AXIS_SET_EX1";

        public const string PROP_PITCH_DECR = "PROP_PITCH_DECR";

        public const string PROP_PITCH1_DECR = "PROP_PITCH1_DECR";

        public const string PROP_PITCH2_DECR = "PROP_PITCH2_DECR";

        public const string PROP_PITCH3_DECR = "PROP_PITCH3_DECR";

        public const string PROP_PITCH4_DECR = "PROP_PITCH4_DECR";

        public const string PROP_PITCH_DECR_SMALL = "PROP_PITCH_DECR_SMALL";

        public const string PROP_PITCH1_DECR_SMALL = "PROP_PITCH1_DECR_SMALL";

        public const string PROP_PITCH2_DECR_SMALL = "PROP_PITCH2_DECR_SMALL";

        public const string PROP_PITCH3_DECR_SMALL = "PROP_PITCH3_DECR_SMALL";

        public const string PROP_PITCH4_DECR_SMALL = "PROP_PITCH4_DECR_SMALL";

        public const string PROP_PITCH_DECREASE_EX1 = "PROP_PITCH_DECREASE_EX1";

        public const string PROP_PITCH1_DECREASE_EX1 = "PROP_PITCH1_DECREASE_EX1";

        public const string PROP_PITCH2_DECREASE_EX1 = "PROP_PITCH2_DECREASE_EX1";

        public const string PROP_PITCH3_DECREASE_EX1 = "PROP_PITCH3_DECREASE_EX1";

        public const string PROP_PITCH4_DECREASE_EX1 = "PROP_PITCH4_DECREASE_EX1";

        public const string PROP_PITCH_DECREASE_SMALL_EX1 = "PROP_PITCH_DECREASE_SMALL_EX1";

        public const string PROP_PITCH1_DECREASE_SMALL_EX1 = "PROP_PITCH1_DECREASE_SMALL_EX1";

        public const string PROP_PITCH2_DECREASE_SMALL_EX1 = "PROP_PITCH2_DECREASE_SMALL_EX1";

        public const string PROP_PITCH3_DECREASE_SMALL_EX1 = "PROP_PITCH3_DECREASE_SMALL_EX1";

        public const string PROP_PITCH4_DECREASE_SMALL_EX1 = "PROP_PITCH4_DECREASE_SMALL_EX1";

        public const string PROP_PITCH_HI = "PROP_PITCH_HI";

        public const string PROP_PITCH1_HI = "PROP_PITCH1_HI";

        public const string PROP_PITCH2_HI = "PROP_PITCH2_HI";

        public const string PROP_PITCH3_HI = "PROP_PITCH3_HI";

        public const string PROP_PITCH4_HI = "PROP_PITCH4_HI";

        public const string PROP_PITCH_HI_EX1 = "PROP_PITCH_HI_EX1";

        public const string PROP_PITCH1_HI_EX1 = "PROP_PITCH1_HI_EX1";

        public const string PROP_PITCH2_HI_EX1 = "PROP_PITCH2_HI_EX1";

        public const string PROP_PITCH3_HI_EX1 = "PROP_PITCH3_HI_EX1";

        public const string PROP_PITCH4_HI_EX1 = "PROP_PITCH4_HI_EX1";

        public const string PROP_PITCH_INCR = "PROP_PITCH_INCR";

        public const string PROP_PITCH1_INCR = "PROP_PITCH1_INCR";

        public const string PROP_PITCH2_INCR = "PROP_PITCH2_INCR";

        public const string PROP_PITCH3_INCR = "PROP_PITCH3_INCR";

        public const string PROP_PITCH4_INCR = "PROP_PITCH4_INCR";

        public const string PROP_PITCH_INCR_SMALL = "PROP_PITCH_INCR_SMALL";

        public const string PROP_PITCH1_INCR_SMALL = "PROP_PITCH1_INCR_SMALL";

        public const string PROP_PITCH2_INCR_SMALL = "PROP_PITCH2_INCR_SMALL";

        public const string PROP_PITCH3_INCR_SMALL = "PROP_PITCH3_INCR_SMALL";

        public const string PROP_PITCH4_INCR_SMALL = "PROP_PITCH4_INCR_SMALL";

        public const string PROP_PITCH_INCREASE_EX1 = "PROP_PITCH_INCREASE_EX1";

        public const string PROP_PITCH1_INCREASE_EX1 = "PROP_PITCH1_INCREASE_EX1";

        public const string PROP_PITCH2_INCREASE_EX1 = "PROP_PITCH2_INCREASE_EX1";

        public const string PROP_PITCH3_INCREASE_EX1 = "PROP_PITCH3_INCREASE_EX1";

        public const string PROP_PITCH4_INCREASE_EX1 = "PROP_PITCH4_INCREASE_EX1";

        public const string PROP_PITCH_INCREASE_SMALL_EX1 = "PROP_PITCH_INCREASE_SMALL_EX1";

        public const string PROP_PITCH1_INCREASE_SMALL_EX1 = "PROP_PITCH1_INCREASE_SMALL_EX1";

        public const string PROP_PITCH2_INCREASE_SMALL_EX1 = "PROP_PITCH2_INCREASE_SMALL_EX1";

        public const string PROP_PITCH3_INCREASE_SMALL_EX1 = "PROP_PITCH3_INCREASE_SMALL_EX1";

        public const string PROP_PITCH4_INCREASE_SMALL_EX1 = "PROP_PITCH4_INCREASE_SMALL_EX1";

        public const string PROP_PITCH_LO = "PROP_PITCH_LO";

        public const string PROP_PITCH1_LO = "PROP_PITCH1_LO";

        public const string PROP_PITCH2_LO = "PROP_PITCH2_LO";

        public const string PROP_PITCH3_LO = "PROP_PITCH3_LO";

        public const string PROP_PITCH4_LO = "PROP_PITCH4_LO";

        public const string PROP_PITCH_LO_EX1 = "PROP_PITCH_LO_EX1";

        public const string PROP_PITCH1_LO_EX1 = "PROP_PITCH1_LO_EX1";

        public const string PROP_PITCH2_LO_EX1 = "PROP_PITCH2_LO_EX1";

        public const string PROP_PITCH3_LO_EX1 = "PROP_PITCH3_LO_EX1";

        public const string PROP_PITCH4_LO_EX1 = "PROP_PITCH4_LO_EX1";

        public const string PROP_PITCH_SET = "PROP_PITCH_SET";

        public const string PROP_PITCH1_SET = "PROP_PITCH1_SET";

        public const string PROP_PITCH2_SET = "PROP_PITCH2_SET";

        public const string PROP_PITCH3_SET = "PROP_PITCH3_SET";

        public const string PROP_PITCH4_SET = "PROP_PITCH4_SET";

        public const string TOGGLE_PROPELLER_SYNC = "TOGGLE_PROPELLER_SYNC";

        public const string PROPELLER_REVERSE_THRUST_TOGGLE = "PROPELLER_REVERSE_THRUST_TOGGLE";

        public const string PROPELLER_REVERSE_THRUST_HOLD = "PROPELLER_REVERSE_THRUST_HOLD";

        public const string TOGGLE_AUTOFEATHER_ARM = "TOGGLE_AUTOFEATHER_ARM";

        public const string TOGGLE_FEATHER_SWITCHES = "TOGGLE_FEATHER_SWITCHES";

        public const string TOGGLE_FEATHER_SWITCH_1 = "TOGGLE_FEATHER_SWITCH_1";

        public const string TOGGLE_FEATHER_SWITCH_2 = "TOGGLE_FEATHER_SWITCH_2";

        public const string TOGGLE_FEATHER_SWITCH_3 = "TOGGLE_FEATHER_SWITCH_3";

        public const string TOGGLE_FEATHER_SWITCH_4 = "TOGGLE_FEATHER_SWITCH_4";

        public const string TOGGLE_PROPELLER_DEICE = "TOGGLE_PROPELLER_DEICE";

      }
      public static class Throttle
      {
        [Parameter(0, "the value between 0 - 16384")]
        public const string AXIS_THROTTLE_MINUS = "AXIS_THROTTLE_MINUS";

        [Parameter(0, "the value between 0 - 16384")]
        public const string AXIS_THROTTLE_PLUS = "AXIS_THROTTLE_PLUS";

        public const string AXIS_THROTTLE_SET = "AXIS_THROTTLE_SET";

        public const string AXIS_THROTTLE1_SET = "AXIS_THROTTLE1_SET";

        public const string AXIS_THROTTLE2_SET = "AXIS_THROTTLE2_SET";

        public const string AXIS_THROTTLE3_SET = "AXIS_THROTTLE3_SET";

        public const string AXIS_THROTTLE4_SET = "AXIS_THROTTLE4_SET";

        public const string DECREASE_THROTTLE = "DECREASE_THROTTLE";

        public const string INCREASE_THROTTLE = "INCREASE_THROTTLE";

        public const string SET_REVERSE_THRUST_OFF = "SET_REVERSE_THRUST_OFF";

        public const string SET_REVERSE_THRUST_ON = "SET_REVERSE_THRUST_ON";

        public const string SET_THROTTLE1_REVERSE_THRUST_OFF = "SET_THROTTLE1_REVERSE_THRUST_OFF";

        public const string SET_THROTTLE2_REVERSE_THRUST_OFF = "SET_THROTTLE2_REVERSE_THRUST_OFF";

        public const string SET_THROTTLE3_REVERSE_THRUST_OFF = "SET_THROTTLE3_REVERSE_THRUST_OFF";

        public const string SET_THROTTLE4_REVERSE_THRUST_OFF = "SET_THROTTLE4_REVERSE_THRUST_OFF";

        public const string SET_THROTTLE1_REVERSE_THRUST_ON = "SET_THROTTLE1_REVERSE_THRUST_ON";

        public const string SET_THROTTLE2_REVERSE_THRUST_ON = "SET_THROTTLE2_REVERSE_THRUST_ON";

        public const string SET_THROTTLE3_REVERSE_THRUST_ON = "SET_THROTTLE3_REVERSE_THRUST_ON";

        public const string SET_THROTTLE4_REVERSE_THRUST_ON = "SET_THROTTLE4_REVERSE_THRUST_ON";

        public const string THROTTLE_INCR = "THROTTLE_INCR";

        public const string THROTTLE_DECR = "THROTTLE_DECR";

        public const string THROTTLE1_DECR = "THROTTLE1_DECR";

        public const string THROTTLE2_DECR = "THROTTLE2_DECR";

        public const string THROTTLE3_DECR = "THROTTLE3_DECR";

        public const string THROTTLE4_DECR = "THROTTLE4_DECR";

        public const string THROTTLE_10 = "THROTTLE_10";

        public const string THROTTLE_20 = "THROTTLE_20";

        public const string THROTTLE_30 = "THROTTLE_30";

        public const string THROTTLE_40 = "THROTTLE_40";

        public const string THROTTLE_50 = "THROTTLE_50";

        public const string THROTTLE_60 = "THROTTLE_60";

        public const string THROTTLE_70 = "THROTTLE_70";

        public const string THROTTLE_80 = "THROTTLE_80";

        public const string THROTTLE_90 = "THROTTLE_90";

        public const string THROTTLE_AXIS_SET_EX1 = "THROTTLE_AXIS_SET_EX1";

        public const string THROTTLE1_AXIS_SET_EX1 = "THROTTLE1_AXIS_SET_EX1";

        public const string THROTTLE2_AXIS_SET_EX1 = "THROTTLE2_AXIS_SET_EX1";

        public const string THROTTLE3_AXIS_SET_EX1 = "THROTTLE3_AXIS_SET_EX1";

        public const string THROTTLE4_AXIS_SET_EX1 = "THROTTLE4_AXIS_SET_EX1";

        public const string THROTTLE_CUT = "THROTTLE_CUT";

        public const string THROTTLE1_CUT = "THROTTLE1_CUT";

        public const string THROTTLE2_CUT = "THROTTLE2_CUT";

        public const string THROTTLE3_CUT = "THROTTLE3_CUT";

        public const string THROTTLE4_CUT = "THROTTLE4_CUT";

        public const string THROTTLE_CUT_EX1 = "THROTTLE_CUT_EX1";

        public const string THROTTLE1_CUT_EX1 = "THROTTLE1_CUT_EX1";

        public const string THROTTLE2_CUT_EX1 = "THROTTLE2_CUT_EX1";

        public const string THROTTLE3_CUT_EX1 = "THROTTLE3_CUT_EX1";

        public const string THROTTLE4_CUT_EX1 = "THROTTLE4_CUT_EX1";

        public const string THROTTLE_DECR_SMALL = "THROTTLE_DECR_SMALL";

        public const string THROTTLE1_DECR_SMALL = "THROTTLE1_DECR_SMALL";

        public const string THROTTLE2_DECR_SMALL = "THROTTLE2_DECR_SMALL";

        public const string THROTTLE3_DECR_SMALL = "THROTTLE3_DECR_SMALL";

        public const string THROTTLE4_DECR_SMALL = "THROTTLE4_DECR_SMALL";

        public const string THROTTLE_DECREASE_EX1 = "THROTTLE_DECREASE_EX1";

        public const string THROTTLE1_DECREASE_EX1 = "THROTTLE1_DECREASE_EX1";

        public const string THROTTLE2_DECREASE_EX1 = "THROTTLE2_DECREASE_EX1";

        public const string THROTTLE3_DECREASE_EX1 = "THROTTLE3_DECREASE_EX1";

        public const string THROTTLE4_DECREASE_EX1 = "THROTTLE4_DECREASE_EX1";

        public const string THROTTLE_DECREASE_SMALL_EX1 = "THROTTLE_DECREASE_SMALL_EX1";

        public const string THROTTLE1_DECREASE_SMALL_EX1 = "THROTTLE1_DECREASE_SMALL_EX1";

        public const string THROTTLE2_DECREASE_SMALL_EX1 = "THROTTLE2_DECREASE_SMALL_EX1";

        public const string THROTTLE3_DECREASE_SMALL_EX1 = "THROTTLE3_DECREASE_SMALL_EX1";

        public const string THROTTLE4_DECREASE_SMALL_EX1 = "THROTTLE4_DECREASE_SMALL_EX1";

        public const string THROTTLE_FULL = "THROTTLE_FULL";

        public const string THROTTLE1_FULL = "THROTTLE1_FULL";

        public const string THROTTLE2_FULL = "THROTTLE2_FULL";

        public const string THROTTLE3_FULL = "THROTTLE3_FULL";

        public const string THROTTLE4_FULL = "THROTTLE4_FULL";

        public const string THROTTLE_FULL_EX1 = "THROTTLE_FULL_EX1";

        public const string THROTTLE1_FULL_EX1 = "THROTTLE1_FULL_EX1";

        public const string THROTTLE2_FULL_EX1 = "THROTTLE2_FULL_EX1";

        public const string THROTTLE3_FULL_EX1 = "THROTTLE3_FULL_EX1";

        public const string THROTTLE4_FULL_EX1 = "THROTTLE4_FULL_EX1";

        public const string THROTTLE_INCREASE_EX1 = "THROTTLE_INCREASE_EX1";

        public const string THROTTLE1_INCR = "THROTTLE1_INCR";

        public const string THROTTLE2_INCR = "THROTTLE2_INCR";

        public const string THROTTLE3_INCR = "THROTTLE3_INCR";

        public const string THROTTLE4_INCR = "THROTTLE4_INCR";

        public const string THROTTLE1_INCREASE_EX1 = "THROTTLE1_INCREASE_EX1";

        public const string THROTTLE2_INCREASE_EX1 = "THROTTLE2_INCREASE_EX1";

        public const string THROTTLE3_INCREASE_EX1 = "THROTTLE3_INCREASE_EX1";

        public const string THROTTLE4_INCREASE_EX1 = "THROTTLE4_INCREASE_EX1";

        public const string THROTTLE_INCREASE_SMALL_EX1 = "THROTTLE_INCREASE_SMALL_EX1";

        public const string THROTTLE1_INCR_SMALL = "THROTTLE1_INCR_SMALL";

        public const string THROTTLE2_INCR_SMALL = "THROTTLE2_INCR_SMALL";

        public const string THROTTLE3_INCR_SMALL = "THROTTLE3_INCR_SMALL";

        public const string THROTTLE4_INCR_SMALL = "THROTTLE4_INCR_SMALL";

        public const string THROTTLE1_INCREASE_SMALL_EX1 = "THROTTLE1_INCREASE_SMALL_EX1";

        public const string THROTTLE2_INCREASE_SMALL_EX1 = "THROTTLE2_INCREASE_SMALL_EX1";

        public const string THROTTLE3_INCREASE_SMALL_EX1 = "THROTTLE3_INCREASE_SMALL_EX1";

        public const string THROTTLE4_INCREASE_SMALL_EX1 = "THROTTLE4_INCREASE_SMALL_EX1";

        public const string THROTTLE_REVERSE_THRUST_TOGGLE = "THROTTLE_REVERSE_THRUST_TOGGLE";

        public const string THROTTLE_REVERSE_THRUST_HOLD = "THROTTLE_REVERSE_THRUST_HOLD";

        public const string THROTTLE1_REVERSE_THRUST_HOLD = "THROTTLE1_REVERSE_THRUST_HOLD";

        public const string THROTTLE2_REVERSE_THRUST_HOLD = "THROTTLE2_REVERSE_THRUST_HOLD";

        public const string THROTTLE3_REVERSE_THRUST_HOLD = "THROTTLE3_REVERSE_THRUST_HOLD";

        public const string THROTTLE4_REVERSE_THRUST_HOLD = "THROTTLE4_REVERSE_THRUST_HOLD";

        public const string THROTTLE_SET = "THROTTLE_SET";

        public const string THROTTLE1_SET = "THROTTLE1_SET";

        public const string THROTTLE2_SET = "THROTTLE2_SET";

        public const string THROTTLE3_SET = "THROTTLE3_SET";

        public const string THROTTLE4_SET = "THROTTLE4_SET";

        public const string TOGGLE_THROTTLE1_REVERSE_THRUST = "TOGGLE_THROTTLE1_REVERSE_THRUST";

        public const string TOGGLE_THROTTLE2_REVERSE_THRUST = "TOGGLE_THROTTLE2_REVERSE_THRUST";

        public const string TOGGLE_THROTTLE3_REVERSE_THRUST = "TOGGLE_THROTTLE3_REVERSE_THRUST";

        public const string TOGGLE_THROTTLE4_REVERSE_THRUST = "TOGGLE_THROTTLE4_REVERSE_THRUST";

      }
      public static class Turbine
      {
        [Parameter(0, "Engine index (1 to 4)")]
        [Parameter(1, "State (TRUE / FALSE)")]
        public const string ISOLATE_TURBINE_SET = "ISOLATE_TURBINE_SET";

        [Parameter(0, "Engine index (1 to 4)")]
        public const string ISOLATE_TURBINE_ON = "ISOLATE_TURBINE_ON";

        [Parameter(0, "Engine index (1 to 4)")]
        public const string ISOLATE_TURBINE_OFF = "ISOLATE_TURBINE_OFF";

        [Parameter(0, "Engine index (1 to 4)")]
        public const string ISOLATE_TURBINE_TOGGLE = "ISOLATE_TURBINE_TOGGLE";

        public const string TURBINE_IGNITION_SWITCH_SET = "TURBINE_IGNITION_SWITCH_SET";

        public const string TURBINE_IGNITION_SWITCH_SET1 = "TURBINE_IGNITION_SWITCH_SET1";

        public const string TURBINE_IGNITION_SWITCH_SET2 = "TURBINE_IGNITION_SWITCH_SET2";

        public const string TURBINE_IGNITION_SWITCH_SET3 = "TURBINE_IGNITION_SWITCH_SET3";

        public const string TURBINE_IGNITION_SWITCH_SET4 = "TURBINE_IGNITION_SWITCH_SET4";

        public const string TURBINE_IGNITION_SWITCH_TOGGLE = "TURBINE_IGNITION_SWITCH_TOGGLE";

      }
      public static class Starter
      {
        [Parameter(0, "Index")]
        public const string JET_STARTER = "JET_STARTER";

        [Parameter(0, "Bool")]
        public const string SET_STARTER1_HELD = "SET_STARTER1_HELD";

        [Parameter(0, "Bool")]
        public const string SET_STARTER2_HELD = "SET_STARTER2_HELD";

        [Parameter(0, "Bool")]
        public const string SET_STARTER3_HELD = "SET_STARTER3_HELD";

        [Parameter(0, "Bool")]
        public const string SET_STARTER4_HELD = "SET_STARTER4_HELD";

        [Parameter(0, "Bool")]
        public const string SET_STARTER_ALL_HELD = "SET_STARTER_ALL_HELD";

        [Parameter(0, "Bool")]
        public const string STARTER_SET = "STARTER_SET";

        [Parameter(0, "Bool")]
        public const string STARTER1_SET = "STARTER1_SET";

        [Parameter(0, "Bool")]
        public const string STARTER2_SET = "STARTER2_SET";

        [Parameter(0, "Bool")]
        public const string STARTER3_SET = "STARTER3_SET";

        [Parameter(0, "Bool")]
        public const string STARTER4_SET = "STARTER4_SET";

        public const string TOGGLE_ALL_STARTERS = "TOGGLE_ALL_STARTERS";

        public const string TOGGLE_MASTER_STARTER_SWITCH = "TOGGLE_MASTER_STARTER_SWITCH";

        public const string TOGGLE_STARTER1 = "TOGGLE_STARTER1";

        public const string TOGGLE_STARTER2 = "TOGGLE_STARTER2";

        public const string TOGGLE_STARTER3 = "TOGGLE_STARTER3";

        public const string TOGGLE_STARTER4 = "TOGGLE_STARTER4";

      }
    }

  }
}

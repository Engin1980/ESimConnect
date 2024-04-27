namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftAutopilot
    {
      public static class Autopilot
      {
        [Deprecated]
        public const string AP_MASTER_ALT = "AP_MASTER_ALT";

        public const string AP_AIRSPEED_HOLD = "AP_AIRSPEED_HOLD";

        public const string AP_AIRSPEED_OFF = "AP_AIRSPEED_OFF";

        public const string AP_AIRSPEED_ON = "AP_AIRSPEED_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_AIRSPEED_SET = "AP_AIRSPEED_SET";

        [Parameter(0, "New reference altitude")]
        [Parameter(1, "Index")]
        public const string AP_ALT_VAR_DEC = "AP_ALT_VAR_DEC";

        [Parameter(0, "New reference altitude")]
        [Parameter(1, "Index")]
        public const string AP_ALT_VAR_INC = "AP_ALT_VAR_INC";

        public const string AP_ALT_HOLD = "AP_ALT_HOLD";

        public const string AP_ALT_HOLD_OFF = "AP_ALT_HOLD_OFF";

        public const string AP_ALT_HOLD_ON = "AP_ALT_HOLD_ON";

        public const string AP_ALT_RADIO_MODE_OFF = "AP_ALT_RADIO_MODE_OFF";

        public const string AP_ALT_RADIO_MODE_ON = "AP_ALT_RADIO_MODE_ON";

        [Parameter(0, "bool")]
        public const string AP_ALT_RADIO_MODE_SET = "AP_ALT_RADIO_MODE_SET";

        public const string AP_ALT_RADIO_MODE_TOGGLE = "AP_ALT_RADIO_MODE_TOGGLE";

        [Parameter(0, "New reference altitude")]
        [Parameter(1, "Index")]
        public const string AP_ALT_VAR_SET_ENGLISH = "AP_ALT_VAR_SET_ENGLISH";

        [Parameter(0, "New reference altitude")]
        [Parameter(1, "Index")]
        public const string AP_ALT_VAR_SET_METRIC = "AP_ALT_VAR_SET_METRIC";

        public const string AP_APR_HOLD = "AP_APR_HOLD";

        public const string AP_APR_HOLD_OFF = "AP_APR_HOLD_OFF";

        public const string AP_APR_HOLD_ON = "AP_APR_HOLD_ON";

        public const string AP_ATT_HOLD = "AP_ATT_HOLD";

        public const string AP_ATT_HOLD_OFF = "AP_ATT_HOLD_OFF";

        public const string AP_ATT_HOLD_ON = "AP_ATT_HOLD_ON";

        public const string AP_AVIONICS_MANAGED_OFF = "AP_AVIONICS_MANAGED_OFF";

        public const string AP_AVIONICS_MANAGED_ON = "AP_AVIONICS_MANAGED_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_AVIONICS_MANAGED_SET = "AP_AVIONICS_MANAGED_SET";

        public const string AP_AVIONICS_MANAGED_TOGGLE = "AP_AVIONICS_MANAGED_TOGGLE";

        public const string AP_BANK_HOLD = "AP_BANK_HOLD";

        public const string AP_BANK_HOLD_OFF = "AP_BANK_HOLD_OFF";

        public const string AP_BANK_HOLD_ON = "AP_BANK_HOLD_ON";

        public const string AP_BC_HOLD = "AP_BC_HOLD";

        public const string AP_BC_HOLD_OFF = "AP_BC_HOLD_OFF";

        public const string AP_BC_HOLD_ON = "AP_BC_HOLD_ON";

        public const string AP_HDG_HOLD = "AP_HDG_HOLD";

        public const string AP_HDG_HOLD_OFF = "AP_HDG_HOLD_OFF";

        public const string AP_HDG_HOLD_ON = "AP_HDG_HOLD_ON";

        public const string AP_LOC_HOLD = "AP_LOC_HOLD";

        public const string AP_LOC_HOLD_OFF = "AP_LOC_HOLD_OFF";

        public const string AP_LOC_HOLD_ON = "AP_LOC_HOLD_ON";

        [Parameter(0, "the Index of the engine to target (1 - 4)")]
        public const string AP_MACH_VAR_DEC = "AP_MACH_VAR_DEC";

        [Parameter(0, "Index of the engine to target (1 - 4)")]
        public const string AP_MACH_VAR_INC = "AP_MACH_VAR_INC";

        public const string AP_MACH_HOLD = "AP_MACH_HOLD";

        public const string AP_MACH_OFF = "AP_MACH_OFF";

        public const string AP_MACH_ON = "AP_MACH_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_MACH_SET = "AP_MACH_SET";

        [Parameter(0, "Integer mach value / 100 (eg: 100 as value results as mach 1)")]
        [Parameter(1, "Index of the engine to target (1 - 4)")]
        public const string AP_MACH_VAR_SET = "AP_MACH_VAR_SET";

        [Parameter(0, "Integer mach value \\ 1000000 (eg: 1000 as value results as mach 0.001)")]
        [Parameter(1, "Index of the engine to target (1 - 4)")]
        public const string AP_MACH_VAR_SET_EX1 = "AP_MACH_VAR_SET_EX1";

        public const string AP_MANAGED_SPEED_IN_MACH_OFF = "AP_MANAGED_SPEED_IN_MACH_OFF";

        public const string AP_MANAGED_SPEED_IN_MACH_ON = "AP_MANAGED_SPEED_IN_MACH_ON";

        [Parameter(0, "use TRUE/FALSE to enabled/disable")]
        public const string AP_MANAGED_SPEED_IN_MACH_SET = "AP_MANAGED_SPEED_IN_MACH_SET";

        public const string AP_MANAGED_SPEED_IN_MACH_TOGGLE = "AP_MANAGED_SPEED_IN_MACH_TOGGLE";

        public const string AP_MASTER = "AP_MASTER";

        [Parameter(0, "angle")]
        public const string AP_MAX_BANK_ANGLE_SET = "AP_MAX_BANK_ANGLE_SET";

        public const string AP_MAX_BANK_INC = "AP_MAX_BANK_INC";

        public const string AP_MAX_BANK_DEC = "AP_MAX_BANK_DEC";

        [Parameter(0, "the index to use for max bank angle.")]
        public const string AP_MAX_BANK_SET = "AP_MAX_BANK_SET";

        [Parameter(0, "Velocity")]
        public const string AP_MAX_BANK_VELOCITY_SET = "AP_MAX_BANK_VELOCITY_SET";

        public const string AP_N1_HOLD = "AP_N1_HOLD";

        public const string AP_N1_REF_DEC = "AP_N1_REF_DEC";

        public const string AP_N1_REF_INC = "AP_N1_REF_INC";

        [Parameter(0, "Integer N1 reference value")]
        [Parameter(1, "Index of the engine to target (1 - 4)")]
        public const string AP_N1_REF_SET = "AP_N1_REF_SET";

        [Parameter(0, "the NAVindex to use")]
        public const string AP_NAV_SELECT_SET = "AP_NAV_SELECT_SET";

        public const string AP_NAV1_HOLD = "AP_NAV1_HOLD";

        public const string AP_NAV1_HOLD_OFF = "AP_NAV1_HOLD_OFF";

        public const string AP_NAV1_HOLD_ON = "AP_NAV1_HOLD_ON";

        public const string AP_PANEL_ALTITUDE_HOLD = "AP_PANEL_ALTITUDE_HOLD";

        public const string AP_PANEL_ALTITUDE_OFF = "AP_PANEL_ALTITUDE_OFF";

        public const string AP_PANEL_ALTITUDE_ON = "AP_PANEL_ALTITUDE_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_PANEL_ALTITUDE_SET = "AP_PANEL_ALTITUDE_SET";

        public const string AP_PANEL_HEADING_HOLD = "AP_PANEL_HEADING_HOLD";

        public const string AP_PANEL_HEADING_OFF = "AP_PANEL_HEADING_OFF";

        public const string AP_PANEL_HEADING_ON = "AP_PANEL_HEADING_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_PANEL_HEADING_SET = "AP_PANEL_HEADING_SET";

        public const string AP_PANEL_MACH_HOLD = "AP_PANEL_MACH_HOLD";

        public const string AP_PANEL_MACH_OFF = "AP_PANEL_MACH_OFF";

        public const string AP_PANEL_MACH_ON = "AP_PANEL_MACH_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_PANEL_MACH_SET = "AP_PANEL_MACH_SET";

        public const string AP_PANEL_SPEED_HOLD = "AP_PANEL_SPEED_HOLD";

        public const string AP_PANEL_SPEED_OFF = "AP_PANEL_SPEED_OFF";

        public const string AP_PANEL_SPEED_ON = "AP_PANEL_SPEED_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_PANEL_SPEED_SET = "AP_PANEL_SPEED_SET";

        public const string AP_PANEL_VS_OFF = "AP_PANEL_VS_OFF";

        public const string AP_PANEL_VS_ON = "AP_PANEL_VS_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_PANEL_VS_SET = "AP_PANEL_VS_SET";

        public const string AP_PANEL_VS_HOLD = "AP_PANEL_VS_HOLD";

        public const string AP_PITCH_LEVELER = "AP_PITCH_LEVELER";

        public const string AP_PITCH_LEVELER_OFF = "AP_PITCH_LEVELER_OFF";

        public const string AP_PITCH_LEVELER_ON = "AP_PITCH_LEVELER_ON";

        public const string AP_PITCH_REF_INC_DN = "AP_PITCH_REF_INC_DN";

        public const string AP_PITCH_REF_INC_UP = "AP_PITCH_REF_INC_UP";

        public const string AP_PITCH_REF_SELECT = "AP_PITCH_REF_SELECT";

        [Parameter(0, "pitch value between -16384 and 16384")]
        public const string AP_PITCH_REF_SET = "AP_PITCH_REF_SET";

        [Parameter(0, "slot index from 1 to 4")]
        public const string AP_RPM_SLOT_INDEX_SET = "AP_RPM_SLOT_INDEX_SET";

        public const string AP_SPD_VAR_DEC = "AP_SPD_VAR_DEC";

        public const string AP_SPD_VAR_INC = "AP_SPD_VAR_INC";

        [Parameter(0, "value in Knots.")]
        [Parameter(1, "the managed index, from 1 to 4, or 0.")]
        public const string AP_SPD_VAR_SET = "AP_SPD_VAR_SET";

        [Parameter(0, "value in Knots.")]
        [Parameter(1, "the managed index, from 1 to 4, or 0.")]
        public const string AP_SPD_VAR_SET_EX1 = "AP_SPD_VAR_SET_EX1";

        [Parameter(0, "slot index from 1 to 4")]
        public const string AP_SPEED_SLOT_INDEX_SET = "AP_SPEED_SLOT_INDEX_SET";

        public const string AP_VS_HOLD = "AP_VS_HOLD";

        public const string AP_VS_OFF = "AP_VS_OFF";

        public const string AP_VS_ON = "AP_VS_ON";

        [Parameter(0, "TRUE/FALSE to enable/disable")]
        public const string AP_VS_SET = "AP_VS_SET";

        [Parameter(0, "slot index from 1 to 4")]
        public const string AP_VS_SLOT_INDEX_SET = "AP_VS_SLOT_INDEX_SET";

        public const string AP_VS_VAR_DEC = "AP_VS_VAR_DEC";

        public const string AP_VS_VAR_INC = "AP_VS_VAR_INC";

        public const string AP_VS_VAR_SET_CURRENT = "AP_VS_VAR_SET_CURRENT";

        [Parameter(0, "New VS reference")]
        [Parameter(1, "Index")]
        public const string AP_VS_VAR_SET_ENGLISH = "AP_VS_VAR_SET_ENGLISH";

        [Parameter(0, "New VS reference")]
        [Parameter(1, "Index")]
        public const string AP_VS_VAR_SET_METRIC = "AP_VS_VAR_SET_METRIC";

        public const string AP_WING_LEVELER = "AP_WING_LEVELER";

        public const string AP_WING_LEVELER_OFF = "AP_WING_LEVELER_OFF";

        public const string AP_WING_LEVELER_ON = "AP_WING_LEVELER_ON";

        public const string AP_PANEL_SPEED_HOLD_TOGGLE = "AP_PANEL_SPEED_HOLD_TOGGLE";

        public const string AP_PANEL_MACH_HOLD_TOGGLE = "AP_PANEL_MACH_HOLD_TOGGLE";

        public const string AUTOPILOT_AIRSPEED_ACQUIRE = "AUTOPILOT_AIRSPEED_ACQUIRE";

        [Parameter(0, "boolean value to enable/disable the disengage value")]
        public const string AUTOPILOT_DISENGAGE_SET = "AUTOPILOT_DISENGAGE_SET";

        public const string AUTOPILOT_DISENGAGE_TOGGLE = "AUTOPILOT_DISENGAGE_TOGGLE";

        public const string AUTOPILOT_OFF = "AUTOPILOT_OFF";

        public const string AUTOPILOT_ON = "AUTOPILOT_ON";

        public const string AUTOPILOT_PANEL_AIRSPEED_SET = "AUTOPILOT_PANEL_AIRSPEED_SET";

        [Deprecated]
        public const string AUTOPILOT_PANEL_CRUISE_SPEED = "AUTOPILOT_PANEL_CRUISE_SPEED";

        [Deprecated]
        public const string AUTOPILOT_PANEL_MAX_SPEED = "AUTOPILOT_PANEL_MAX_SPEED";

        public const string ALTITUDE_SLOT_INDEX_SET = "ALTITUDE_SLOT_INDEX_SET";

        public const string FLIGHT_LEVEL_CHANGE = "FLIGHT_LEVEL_CHANGE";

        public const string FLIGHT_LEVEL_CHANGE_OFF = "FLIGHT_LEVEL_CHANGE_OFF";

        public const string FLIGHT_LEVEL_CHANGE_ON = "FLIGHT_LEVEL_CHANGE_ON";

        public const string HEADING_SLOT_INDEX_SET = "HEADING_SLOT_INDEX_SET";

        public const string RPM_SLOT_INDEX_SET = "RPM_SLOT_INDEX_SET";

        public const string SPEED_SLOT_INDEX_SET = "SPEED_SLOT_INDEX_SET";

        public const string VS_SLOT_INDEX_SET = "VS_SLOT_INDEX_SET";

      }
      public static class AircraftAutomaticFlightSystemsAutopilot
      {
        [Parameter(0, "the reference value")]
        public const string AIRSPEED_BUG_SELECT = "AIRSPEED_BUG_SELECT";

        [Parameter(0, "the reference value")]
        public const string ALTITUDE_BUG_SELECT = "ALTITUDE_BUG_SELECT";

        public const string AUTO_THROTTLE_ARM = "AUTO_THROTTLE_ARM";

        [Parameter(0, "The engine to target. Use 0 to target all engines, or 1 to 4 to target a specific engine.")]
        public const string AUTO_THROTTLE_DISCONNECT = "AUTO_THROTTLE_DISCONNECT";

        public const string AUTO_THROTTLE_TO_GA = "AUTO_THROTTLE_TO_GA";

        public const string AUTOBRAKE_DISARM = "AUTOBRAKE_DISARM";

        public const string AUTOBRAKE_HI_SET = "AUTOBRAKE_HI_SET";

        public const string AUTOBRAKE_LO_SET = "AUTOBRAKE_LO_SET";

        public const string AUTOBRAKE_MED_SET = "AUTOBRAKE_MED_SET";

        public const string DECREASE_AUTOBRAKE_CONTROL = "DECREASE_AUTOBRAKE_CONTROL";

        public const string FLY_BY_WIRE_ELAC_TOGGLE = "FLY_BY_WIRE_ELAC_TOGGLE";

        public const string FLY_BY_WIRE_FAC_TOGGLE = "FLY_BY_WIRE_FAC_TOGGLE";

        public const string FLY_BY_WIRE_SEC_TOGGLE = "FLY_BY_WIRE_SEC_TOGGLE";

        public const string GPWS_SWITCH_TOGGLE = "GPWS_SWITCH_TOGGLE";

        public const string HEADING_BUG_DEC = "HEADING_BUG_DEC";

        public const string HEADING_BUG_INC = "HEADING_BUG_INC";

        [Parameter(0, "heading bug index")]
        public const string HEADING_BUG_SELECT = "HEADING_BUG_SELECT";

        [Parameter(0, "Value in degrees")]
        [Parameter(1, "Index")]
        public const string HEADING_BUG_SET = "HEADING_BUG_SET";

        [Parameter(0, "Value between 0 and 16384")]
        [Parameter(1, "Index")]
        public const string AP_HEADING_BUG_SET_EX1 = "AP_HEADING_BUG_SET_EX1";

        public const string INCREASE_AUTOBRAKE_CONTROL = "INCREASE_AUTOBRAKE_CONTROL";

        [Parameter(0, "autobreak level")]
        public const string SET_AUTOBRAKE_CONTROL = "SET_AUTOBRAKE_CONTROL";

        public const string SYNC_FLIGHT_DIRECTOR_PITCH = "SYNC_FLIGHT_DIRECTOR_PITCH";

        public const string TOGGLE_FLIGHT_DIRECTOR = "TOGGLE_FLIGHT_DIRECTOR";

        public const string YAW_DAMPER_TOGGLE = "YAW_DAMPER_TOGGLE";

        public const string YAW_DAMPER_ON = "YAW_DAMPER_ON";

        public const string YAW_DAMPER_OFF = "YAW_DAMPER_OFF";

        [Parameter(0, "enable/disable yaw damper (TRUE, FALSE)")]
        public const string YAW_DAMPER_SET = "YAW_DAMPER_SET";

        [Parameter(0, "reference value")]
        public const string VSI_BUG_SELECT = "VSI_BUG_SELECT";

      }
      public static class G1000MultiFunctionDisplay
      {
        public const string G1000_MFD_CLEAR_BUTTON = "G1000_MFD_CLEAR_BUTTON";

        public const string G1000_MFD_CURSOR_BUTTON = "G1000_MFD_CURSOR_BUTTON";

        public const string G1000_MFD_DIRECTTO_BUTTON = "G1000_MFD_DIRECTTO_BUTTON";

        public const string G1000_MFD_ENTER_BUTTON = "G1000_MFD_ENTER_BUTTON";

        public const string G1000_MFD_FLIGHTPLAN_BUTTON = "G1000_MFD_FLIGHTPLAN_BUTTON";

        public const string G1000_MFD_GROUP_KNOB_DEC = "G1000_MFD_GROUP_KNOB_DEC";

        public const string G1000_MFD_GROUP_KNOB_INC = "G1000_MFD_GROUP_KNOB_INC";

        public const string G1000_MFD_MENU_BUTTON = "G1000_MFD_MENU_BUTTON";

        public const string G1000_MFD_PAGE_KNOB_DEC = "G1000_MFD_PAGE_KNOB_DEC";

        public const string G1000_MFD_PAGE_KNOB_INC = "G1000_MFD_PAGE_KNOB_INC";

        public const string G1000_MFD_PROCEDURE_BUTTON = "G1000_MFD_PROCEDURE_BUTTON";

        public const string G1000_MFD_SOFTKEY1 = "G1000_MFD_SOFTKEY1";

        public const string G1000_MFD_SOFTKEY2 = "G1000_MFD_SOFTKEY2";

        public const string G1000_MFD_SOFTKEY3 = "G1000_MFD_SOFTKEY3";

        public const string G1000_MFD_SOFTKEY4 = "G1000_MFD_SOFTKEY4";

        public const string G1000_MFD_SOFTKEY5 = "G1000_MFD_SOFTKEY5";

        public const string G1000_MFD_SOFTKEY6 = "G1000_MFD_SOFTKEY6";

        public const string G1000_MFD_SOFTKEY7 = "G1000_MFD_SOFTKEY7";

        public const string G1000_MFD_SOFTKEY8 = "G1000_MFD_SOFTKEY8";

        public const string G1000_MFD_SOFTKEY9 = "G1000_MFD_SOFTKEY9";

        public const string G1000_MFD_SOFTKEY10 = "G1000_MFD_SOFTKEY10";

        public const string G1000_MFD_SOFTKEY11 = "G1000_MFD_SOFTKEY11";

        public const string G1000_MFD_SOFTKEY12 = "G1000_MFD_SOFTKEY12";

        public const string G1000_MFD_ZOOMIN_BUTTON = "G1000_MFD_ZOOMIN_BUTTON";

        public const string G1000_MFD_ZOOMOUT_BUTTON = "G1000_MFD_ZOOMOUT_BUTTON";

        public const string G1000_PFD_CLEAR_BUTTON = "G1000_PFD_CLEAR_BUTTON";

        public const string G1000_PFD_CURSOR_BUTTON = "G1000_PFD_CURSOR_BUTTON";

        public const string G1000_PFD_DIRECTTO_BUTTON = "G1000_PFD_DIRECTTO_BUTTON";

        public const string G1000_PFD_ENTER_BUTTON = "G1000_PFD_ENTER_BUTTON";

        public const string G1000_PFD_FLIGHTPLAN_BUTTON = "G1000_PFD_FLIGHTPLAN_BUTTON";

        public const string G1000_PFD_GROUP_KNOB_INC = "G1000_PFD_GROUP_KNOB_INC";

        public const string G1000_PFD_GROUP_KNOB_DEC = "G1000_PFD_GROUP_KNOB_DEC";

        public const string G1000_PFD_MENU_BUTTON = "G1000_PFD_MENU_BUTTON";

        public const string G1000_PFD_PAGE_KNOB_INC = "G1000_PFD_PAGE_KNOB_INC";

        public const string G1000_PFD_PAGE_KNOB_DEC = "G1000_PFD_PAGE_KNOB_DEC";

        public const string G1000_PFD_PROCEDURE_BUTTON = "G1000_PFD_PROCEDURE_BUTTON";

        public const string G1000_PFD_SOFTKEY1 = "G1000_PFD_SOFTKEY1";

        public const string G1000_PFD_SOFTKEY2 = "G1000_PFD_SOFTKEY2";

        public const string G1000_PFD_SOFTKEY3 = "G1000_PFD_SOFTKEY3";

        public const string G1000_PFD_SOFTKEY4 = "G1000_PFD_SOFTKEY4";

        public const string G1000_PFD_SOFTKEY5 = "G1000_PFD_SOFTKEY5";

        public const string G1000_PFD_SOFTKEY6 = "G1000_PFD_SOFTKEY6";

        public const string G1000_PFD_SOFTKEY7 = "G1000_PFD_SOFTKEY7";

        public const string G1000_PFD_SOFTKEY8 = "G1000_PFD_SOFTKEY8";

        public const string G1000_PFD_SOFTKEY9 = "G1000_PFD_SOFTKEY9";

        public const string G1000_PFD_SOFTKEY10 = "G1000_PFD_SOFTKEY10";

        public const string G1000_PFD_SOFTKEY11 = "G1000_PFD_SOFTKEY11";

        public const string G1000_PFD_SOFTKEY12 = "G1000_PFD_SOFTKEY12";

        public const string G1000_PFD_ZOOMIN_BUTTON = "G1000_PFD_ZOOMIN_BUTTON";

        public const string G1000_PFD_ZOOMOUT_BUTTON = "G1000_PFD_ZOOMOUT_BUTTON";

      }
      public static class VirtualCopilot
      {
        public const string VIRTUAL_COPILOT_ACTION = "VIRTUAL_COPILOT_ACTION";

        [Parameter(0, "Enable or disable (TRUE/FALSE)")]
        public const string VIRTUAL_COPILOT_SET = "VIRTUAL_COPILOT_SET";

        public const string VIRTUAL_COPILOT_TOGGLE = "VIRTUAL_COPILOT_TOGGLE";

      }
      public static class GLimiter
      {
        public const string G_LIMITER_OFF = "G_LIMITER_OFF";

        public const string G_LIMITER_ON = "G_LIMITER_ON";

        public const string G_LIMITER_SET = "G_LIMITER_SET";

        public const string G_LIMITER_TOGGLE = "G_LIMITER_TOGGLE";

      }
    }

  }
}

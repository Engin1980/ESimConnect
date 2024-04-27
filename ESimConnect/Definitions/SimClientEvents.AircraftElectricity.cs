using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftElectricity
    {
      public static class Alternator
      {
        [Parameter(0, "alternator index")]
        public const string ALTERNATOR_OFF = "ALTERNATOR_OFF";

        [Parameter(0, "alternator index")]
        public const string ALTERNATOR_ON = "ALTERNATOR_ON";

        [Parameter(0, "The state on/off (bool)")]
        [Parameter(1, "alternator index")]
        public const string ALTERNATOR_SET = "ALTERNATOR_SET";

        public const string ELECTRICAL_ALTERNATOR_BREAKER_TOGGLE = "ELECTRICAL_ALTERNATOR_BREAKER_TOGGLE";

        public const string TOGGLE_ALTERNATOR1 = "TOGGLE_ALTERNATOR1";

        public const string TOGGLE_ALTERNATOR2 = "TOGGLE_ALTERNATOR2";

        public const string TOGGLE_ALTERNATOR3 = "TOGGLE_ALTERNATOR3";

        public const string TOGGLE_ALTERNATOR4 = "TOGGLE_ALTERNATOR4";

        [Parameter(0, "alternator index")]
        public const string TOGGLE_MASTER_ALTERNATOR = "TOGGLE_MASTER_ALTERNATOR";

      }
      public static class Apu
      {
        [Parameter(0, "bool")]
        public const string APU_BLEED_AIR_SOURCE_SET = "APU_BLEED_AIR_SOURCE_SET";

        public const string APU_BLEED_AIR_SOURCE_TOGGLE = "APU_BLEED_AIR_SOURCE_TOGGLE";

        [Parameter(0, "fire extinguisher index")]
        public const string APU_EXTINGUISH_FIRE = "APU_EXTINGUISH_FIRE";

        public const string APU_GENERATOR_SWITCH_TOGGLE = "APU_GENERATOR_SWITCH_TOGGLE";

        [Parameter(0, "New switch value (0, or 1)")]
        [Parameter(1, "index of the APU switch")]
        public const string APU_GENERATOR_SWITCH_SET = "APU_GENERATOR_SWITCH_SET";

        public const string APU_OFF_SWITCH = "APU_OFF_SWITCH";

        public const string APU_STARTER = "APU_STARTER";

      }
      public static class Batteries
      {
        [Parameter(0, "bool")]
        public const string BATTERY1_SET = "BATTERY1_SET";

        [Parameter(0, "bool")]
        public const string BATTERY2_SET = "BATTERY2_SET";

        [Parameter(0, "bool")]
        public const string BATTERY3_SET = "BATTERY3_SET";

        [Parameter(0, "bool")]
        public const string BATTERY4_SET = "BATTERY4_SET";

        public const string ELECTRICAL_BATTERY_BREAKER_TOGGLE = "ELECTRICAL_BATTERY_BREAKER_TOGGLE";

        [Parameter(0, "battery index")]
        public const string MASTER_BATTERY_OFF = "MASTER_BATTERY_OFF";

        [Parameter(0, "battery index")]
        public const string MASTER_BATTERY_ON = "MASTER_BATTERY_ON";

        [Parameter(0, "battery index")]
        [Parameter(1, "bool")]
        public const string MASTER_BATTERY_SET = "MASTER_BATTERY_SET";

        [Parameter(0, "battery index")]
        public const string TOGGLE_MASTER_BATTERY = "TOGGLE_MASTER_BATTERY";

        [Parameter(0, "battery index")]
        public const string TOGGLE_MASTER_BATTERY_ALTERNATOR = "TOGGLE_MASTER_BATTERY_ALTERNATOR";

        public const string TOGGLE_ELECTRIC_VACUUM_PUMP = "TOGGLE_ELECTRIC_VACUUM_PUMP";

      }
      public static class Breakers
      {
        public const string BREAKER_AVNFAN_TOGGLE = "BREAKER_AVNFAN_TOGGLE";

        public const string BREAKER_AUTOPILOT_TOGGLE = "BREAKER_AUTOPILOT_TOGGLE";

        public const string BREAKER_GPS_TOGGLE = "BREAKER_GPS_TOGGLE";

        public const string BREAKER_ADF_TOGGLE = "BREAKER_ADF_TOGGLE";

        public const string BREAKER_XPNDR_TOGGLE = "BREAKER_XPNDR_TOGGLE";

        public const string BREAKER_FLAP_TOGGLE = "BREAKER_FLAP_TOGGLE";

        public const string BREAKER_INST_TOGGLE = "BREAKER_INST_TOGGLE";

        public const string BREAKER_TURNCOORD_TOGGLE = "BREAKER_TURNCOORD_TOGGLE";

        public const string BREAKER_INSTLTS_TOGGLE = "BREAKER_INSTLTS_TOGGLE";

        public const string BREAKER_ALTFLD_TOGGLE = "BREAKER_ALTFLD_TOGGLE";

        public const string BREAKER_WARN_TOGGLE = "BREAKER_WARN_TOGGLE";

        public const string BREAKER_AVNBUS1_TOGGLE = "BREAKER_AVNBUS1_TOGGLE";

        public const string BREAKER_AVNBUS2_TOGGLE = "BREAKER_AVNBUS2_TOGGLE";

        public const string BREAKER_NAVCOM1_TOGGLE = "BREAKER_NAVCOM1_TOGGLE";

        public const string BREAKER_NAVCOM2_TOGGLE = "BREAKER_NAVCOM2_TOGGLE";

        public const string BREAKER_NAVCOM3_TOGGLE = "BREAKER_NAVCOM3_TOGGLE";

        public const string BREAKER_AVNFAN_SET = "BREAKER_AVNFAN_SET";

        public const string BREAKER_AUTOPILOT_SET = "BREAKER_AUTOPILOT_SET";

        public const string BREAKER_GPS_SET = "BREAKER_GPS_SET";

        public const string BREAKER_NAVCOM1_SET = "BREAKER_NAVCOM1_SET";

        public const string BREAKER_NAVCOM2_SET = "BREAKER_NAVCOM2_SET";

        public const string BREAKER_NAVCOM3_SET = "BREAKER_NAVCOM3_SET";

        public const string BREAKER_ADF_SET = "BREAKER_ADF_SET";

        public const string BREAKER_XPNDR_SET = "BREAKER_XPNDR_SET";

        public const string BREAKER_FLAP_SET = "BREAKER_FLAP_SET";

        public const string BREAKER_INST_SET = "BREAKER_INST_SET";

        public const string BREAKER_AVNBUS1_SET = "BREAKER_AVNBUS1_SET";

        public const string BREAKER_AVNBUS2_SET = "BREAKER_AVNBUS2_SET";

        public const string BREAKER_TURNCOORD_SET = "BREAKER_TURNCOORD_SET";

        public const string BREAKER_INSTLTS_SET = "BREAKER_INSTLTS_SET";

        public const string BREAKER_ALTFLD_SET = "BREAKER_ALTFLD_SET";

        public const string BREAKER_WARN_SET = "BREAKER_WARN_SET";

      }
      public static class AircraftLights
      {
        [Parameter(0, "light circuit index")]
        public const string ALL_LIGHTS_TOGGLE = "ALL_LIGHTS_TOGGLE";

        [Parameter(0, "light circuit index")]
        public const string BEACON_LIGHTS_ON = "BEACON_LIGHTS_ON";

        [Parameter(0, "light circuit index")]
        public const string BEACON_LIGHTS_OFF = "BEACON_LIGHTS_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string BEACON_LIGHTS_SET = "BEACON_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string CABIN_LIGHTS_OFF = "CABIN_LIGHTS_OFF";

        [Parameter(0, "light circuit index")]
        public const string CABIN_LIGHTS_ON = "CABIN_LIGHTS_ON";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string CABIN_LIGHTS_SET = "CABIN_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string GLARESHIELD_LIGHTS_OFF = "GLARESHIELD_LIGHTS_OFF";

        [Parameter(0, "light circuit index")]
        public const string GLARESHIELD_LIGHTS_ON = "GLARESHIELD_LIGHTS_ON";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string GLARESHIELD_LIGHTS_SET = "GLARESHIELD_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string GLARESHIELD_LIGHTS_TOGGLE = "GLARESHIELD_LIGHTS_TOGGLE";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHT_DOWN = "LANDING_LIGHT_DOWN";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHT_HOME = "LANDING_LIGHT_HOME";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHT_LEFT = "LANDING_LIGHT_LEFT";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHT_RIGHT = "LANDING_LIGHT_RIGHT";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHT_UP = "LANDING_LIGHT_UP";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHTS_ON = "LANDING_LIGHTS_ON";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHTS_OFF = "LANDING_LIGHTS_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string LANDING_LIGHTS_SET = "LANDING_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string LANDING_LIGHTS_TOGGLE = "LANDING_LIGHTS_TOGGLE";

        public const string LIGHT_POTENTIOMETER_INC = "LIGHT_POTENTIOMETER_INC";

        public const string LIGHT_POTENTIOMETER_DEC = "LIGHT_POTENTIOMETER_DEC";

        [Parameter(0, "Index")]
        [Parameter(1, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_SET = "LIGHT_POTENTIOMETER_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_1_SET = "LIGHT_POTENTIOMETER_1_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_2_SET = "LIGHT_POTENTIOMETER_2_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_3_SET = "LIGHT_POTENTIOMETER_3_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_4_SET = "LIGHT_POTENTIOMETER_4_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_5_SET = "LIGHT_POTENTIOMETER_5_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_6_SET = "LIGHT_POTENTIOMETER_6_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_7_SET = "LIGHT_POTENTIOMETER_7_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_8_SET = "LIGHT_POTENTIOMETER_8_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_9_SET = "LIGHT_POTENTIOMETER_9_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_10_SET = "LIGHT_POTENTIOMETER_10_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_11_SET = "LIGHT_POTENTIOMETER_11_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_12_SET = "LIGHT_POTENTIOMETER_12_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_13_SET = "LIGHT_POTENTIOMETER_13_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_14_SET = "LIGHT_POTENTIOMETER_14_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_15_SET = "LIGHT_POTENTIOMETER_15_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_16_SET = "LIGHT_POTENTIOMETER_16_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_17_SET = "LIGHT_POTENTIOMETER_17_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_18_SET = "LIGHT_POTENTIOMETER_18_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_19_SET = "LIGHT_POTENTIOMETER_19_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_20_SET = "LIGHT_POTENTIOMETER_20_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_21_SET = "LIGHT_POTENTIOMETER_21_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_22_SET = "LIGHT_POTENTIOMETER_22_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_23_SET = "LIGHT_POTENTIOMETER_23_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_24_SET = "LIGHT_POTENTIOMETER_24_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_25_SET = "LIGHT_POTENTIOMETER_25_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_26_SET = "LIGHT_POTENTIOMETER_26_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_27_SET = "LIGHT_POTENTIOMETER_27_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_28_SET = "LIGHT_POTENTIOMETER_28_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_29_SET = "LIGHT_POTENTIOMETER_29_SET";

        [Parameter(0, "Potentiometer value")]
        public const string LIGHT_POTENTIOMETER_30_SET = "LIGHT_POTENTIOMETER_30_SET";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string LOGO_LIGHTS_SET = "LOGO_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string NAV_LIGHTS_ON = "NAV_LIGHTS_ON";

        [Parameter(0, "light circuit index")]
        public const string NAV_LIGHTS_OFF = "NAV_LIGHTS_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string NAV_LIGHTS_SET = "NAV_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string PANEL_LIGHTS_ON = "PANEL_LIGHTS_ON";

        [Parameter(0, "light circuit index")]
        public const string PANEL_LIGHTS_OFF = "PANEL_LIGHTS_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string PANEL_LIGHTS_SET = "PANEL_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string PANEL_LIGHTS_TOGGLE = "PANEL_LIGHTS_TOGGLE";

        [Parameter(0, "light circuit index")]
        public const string PEDESTRAL_LIGHTS_OFF = "PEDESTRAL_LIGHTS_OFF";

        [Parameter(0, "light circuit index")]
        public const string PEDESTRAL_LIGHTS_ON = "PEDESTRAL_LIGHTS_ON";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string PEDESTRAL_LIGHTS_SET = "PEDESTRAL_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string PEDESTRAL_LIGHTS_TOGGLE = "PEDESTRAL_LIGHTS_TOGGLE";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light circuit index")]
        public const string RECOGNITION_LIGHTS_SET = "RECOGNITION_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string STROBES_ON = "STROBES_ON";

        [Parameter(0, "light circuit index")]
        public const string STROBES_OFF = "STROBES_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string STROBES_SET = "STROBES_SET";

        [Parameter(0, "light circuit index")]
        public const string STROBES_TOGGLE = "STROBES_TOGGLE";

        [Parameter(0, "light circuit index")]
        public const string TAXI_LIGHTS_ON = "TAXI_LIGHTS_ON";

        [Parameter(0, "light circuit index")]
        public const string TAXI_LIGHTS_OFF = "TAXI_LIGHTS_OFF";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string TAXI_LIGHTS_SET = "TAXI_LIGHTS_SET";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_BEACON_LIGHTS = "TOGGLE_BEACON_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_CABIN_LIGHTS = "TOGGLE_CABIN_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_LOGO_LIGHTS = "TOGGLE_LOGO_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_NAV_LIGHTS = "TOGGLE_NAV_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_RECOGNITION_LIGHTS = "TOGGLE_RECOGNITION_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_TAXI_LIGHTS = "TOGGLE_TAXI_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string TOGGLE_WING_LIGHTS = "TOGGLE_WING_LIGHTS";

        [Parameter(0, "light circuit index")]
        public const string WING_LIGHTS_OFF = "WING_LIGHTS_OFF";

        [Parameter(0, "light circuit index")]
        public const string WING_LIGHTS_ON = "WING_LIGHTS_ON";

        [Parameter(0, "state, either on (1) or off (0)")]
        [Parameter(1, "light index")]
        public const string WING_LIGHTS_SET = "WING_LIGHTS_SET";

      }
      public static class Miscellaneous
      {
        [Parameter(0, "Source bus index")]
        [Parameter(1, "Target Bus Index")]
        public const string ELECTRICAL_BUS_BREAKER_TOGGLE = "ELECTRICAL_BUS_BREAKER_TOGGLE";

        [Parameter(0, "Source bus index")]
        [Parameter(1, "Alternator index")]
        public const string ELECTRICAL_BUS_TO_ALTERNATOR_CONNECTION_TOGGLE = "ELECTRICAL_BUS_TO_ALTERNATOR_CONNECTION_TOGGLE";

        [Parameter(0, "Source bus index")]
        [Parameter(1, "battery index")]
        public const string ELECTRICAL_BUS_TO_BATTERY_CONNECTION_TOGGLE = "ELECTRICAL_BUS_TO_BATTERY_CONNECTION_TOGGLE";

        [Parameter(0, "Source bus index")]
        [Parameter(1, "bus index")]
        public const string ELECTRICAL_BUS_TO_BUS_CONNECTION_TOGGLE = "ELECTRICAL_BUS_TO_BUS_CONNECTION_TOGGLE";

        [Parameter(0, "Source bus index")]
        [Parameter(1, "circuit index")]
        public const string ELECTRICAL_BUS_TO_CIRCUIT_CONNECTION_TOGGLE = "ELECTRICAL_BUS_TO_CIRCUIT_CONNECTION_TOGGLE";

        [Parameter(0, "Source bus index")]
        [Parameter(1, "external power index")]
        public const string ELECTRICAL_BUS_TO_EXTERNAL_POWER_CONNECTION_TOGGLE = "ELECTRICAL_BUS_TO_EXTERNAL_POWER_CONNECTION_TOGGLE";

        public const string ELECTRICAL_CIRCUIT_BREAKER_TOGGLE = "ELECTRICAL_CIRCUIT_BREAKER_TOGGLE";

        [Parameter(0, "circuit index")]
        [Parameter(1, "circuit power setting (%)")]
        public const string ELECTRICAL_CIRCUIT_POWER_SETTING_SET = "ELECTRICAL_CIRCUIT_POWER_SETTING_SET";

        public const string ELECTRICAL_CIRCUIT_TOGGLE = "ELECTRICAL_CIRCUIT_TOGGLE";

        [Parameter(0, "procedure index")]
        [Parameter(1, "bInverse (optional)")]
        public const string ELECTRICAL_EXECUTE_PROCEDURE = "ELECTRICAL_EXECUTE_PROCEDURE";

        public const string ELECTRICAL_EXTERNAL_POWER_BREAKER_TOGGLE = "ELECTRICAL_EXTERNAL_POWER_BREAKER_TOGGLE";

        [Parameter(0, "panel light circuit index")]
        [Parameter(1, "power setting (%)")]
        public const string PANEL_LIGHTS_POWER_SETTING_SET = "PANEL_LIGHTS_POWER_SETTING_SET";

        [Parameter(0, "cabin light circuit index")]
        [Parameter(1, "power setting (%)")]
        public const string CABIN_LIGHTS_POWER_SETTING_SET = "CABIN_LIGHTS_POWER_SETTING_SET";

        [Parameter(0, "pedestal light circuit index")]
        [Parameter(1, "power setting (%)")]
        public const string PEDESTRAL_LIGHTS_POWER_SETTING_SET = "PEDESTRAL_LIGHTS_POWER_SETTING_SET";

        [Parameter(0, "glareshield light circuit index")]
        [Parameter(1, "power setting (%)")]
        public const string GLARESHIELD_LIGHTS_POWER_SETTING_SET = "GLARESHIELD_LIGHTS_POWER_SETTING_SET";

        [Parameter(0, "external power index")]
        [Parameter(1, "The state on/off (1, 0)")]
        public const string SET_EXTERNAL_POWER = "SET_EXTERNAL_POWER";

        [Parameter(0, "external power index")]
        public const string TOGGLE_EXTERNAL_POWER = "TOGGLE_EXTERNAL_POWER";

      }
    }
  }
}

namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class FuelSystem
    {
      public static class CrossFeed
      {
        public const string CROSS_FEED_OPEN = "CROSS_FEED_OPEN";

        public const string CROSS_FEED_TOGGLE = "CROSS_FEED_TOGGLE";

        public const string CROSS_FEED_OFF = "CROSS_FEED_OFF";

        public const string CROSS_FEED_LEFT_TO_RIGHT = "CROSS_FEED_LEFT_TO_RIGHT";

        public const string CROSS_FEED_RIGHT_TO_LEFT = "CROSS_FEED_RIGHT_TO_LEFT";

      }
      public static class AircraftFuelSelector
      {
        public const string FUEL_SELECTOR_1_ISOLATE = "FUEL_SELECTOR_1_ISOLATE";

        public const string FUEL_SELECTOR_1_CROSSFEED = "FUEL_SELECTOR_1_CROSSFEED";

        public const string FUEL_SELECTOR_2_ALL = "FUEL_SELECTOR_2_ALL";

        public const string FUEL_SELECTOR_2_CENTER = "FUEL_SELECTOR_2_CENTER";

        public const string FUEL_SELECTOR_2_CROSSFEED = "FUEL_SELECTOR_2_CROSSFEED";

        public const string FUEL_SELECTOR_2_ISOLATE = "FUEL_SELECTOR_2_ISOLATE";

        public const string FUEL_SELECTOR_2_LEFT = "FUEL_SELECTOR_2_LEFT";

        public const string FUEL_SELECTOR_2_LEFT_AUX = "FUEL_SELECTOR_2_LEFT_AUX";

        public const string FUEL_SELECTOR_2_LEFT_MAIN = "FUEL_SELECTOR_2_LEFT_MAIN";

        public const string FUEL_SELECTOR_2_OFF = "FUEL_SELECTOR_2_OFF";

        public const string FUEL_SELECTOR_2_RIGHT = "FUEL_SELECTOR_2_RIGHT";

        public const string FUEL_SELECTOR_2_RIGHT_AUX = "FUEL_SELECTOR_2_RIGHT_AUX";

        public const string FUEL_SELECTOR_2_RIGHT_MAIN = "FUEL_SELECTOR_2_RIGHT_MAIN";

        public const string FUEL_SELECTOR_2_SET = "FUEL_SELECTOR_2_SET";

        public const string FUEL_SELECTOR_3_ALL = "FUEL_SELECTOR_3_ALL";

        public const string FUEL_SELECTOR_3_CENTER = "FUEL_SELECTOR_3_CENTER";

        public const string FUEL_SELECTOR_3_CROSSFEED = "FUEL_SELECTOR_3_CROSSFEED";

        public const string FUEL_SELECTOR_3_ISOLATE = "FUEL_SELECTOR_3_ISOLATE";

        public const string FUEL_SELECTOR_3_LEFT = "FUEL_SELECTOR_3_LEFT";

        public const string FUEL_SELECTOR_3_LEFT_AUX = "FUEL_SELECTOR_3_LEFT_AUX";

        public const string FUEL_SELECTOR_3_LEFT_MAIN = "FUEL_SELECTOR_3_LEFT_MAIN";

        public const string FUEL_SELECTOR_3_OFF = "FUEL_SELECTOR_3_OFF";

        public const string FUEL_SELECTOR_3_RIGHT = "FUEL_SELECTOR_3_RIGHT";

        public const string FUEL_SELECTOR_3_RIGHT_AUX = "FUEL_SELECTOR_3_RIGHT_AUX";

        public const string FUEL_SELECTOR_3_RIGHT_MAIN = "FUEL_SELECTOR_3_RIGHT_MAIN";

        public const string FUEL_SELECTOR_3_SET = "FUEL_SELECTOR_3_SET";

        public const string FUEL_SELECTOR_4_ALL = "FUEL_SELECTOR_4_ALL";

        public const string FUEL_SELECTOR_4_CENTER = "FUEL_SELECTOR_4_CENTER";

        public const string FUEL_SELECTOR_4_CROSSFEED = "FUEL_SELECTOR_4_CROSSFEED";

        public const string FUEL_SELECTOR_4_ISOLATE = "FUEL_SELECTOR_4_ISOLATE";

        public const string FUEL_SELECTOR_4_OFF = "FUEL_SELECTOR_4_OFF";

        public const string FUEL_SELECTOR_4_LEFT = "FUEL_SELECTOR_4_LEFT";

        public const string FUEL_SELECTOR_4_LEFT_AUX = "FUEL_SELECTOR_4_LEFT_AUX";

        public const string FUEL_SELECTOR_4_LEFT_MAIN = "FUEL_SELECTOR_4_LEFT_MAIN";

        public const string FUEL_SELECTOR_4_RIGHT = "FUEL_SELECTOR_4_RIGHT";

        public const string FUEL_SELECTOR_4_RIGHT_AUX = "FUEL_SELECTOR_4_RIGHT_AUX";

        public const string FUEL_SELECTOR_4_RIGHT_MAIN = "FUEL_SELECTOR_4_RIGHT_MAIN";

        public const string FUEL_SELECTOR_4_SET = "FUEL_SELECTOR_4_SET";

        public const string FUEL_SELECTOR_ALL = "FUEL_SELECTOR_ALL";

        public const string FUEL_SELECTOR_CENTER = "FUEL_SELECTOR_CENTER";

        public const string FUEL_SELECTOR_LEFT = "FUEL_SELECTOR_LEFT";

        public const string FUEL_SELECTOR_LEFT_AUX = "FUEL_SELECTOR_LEFT_AUX";

        public const string FUEL_SELECTOR_LEFT_MAIN = "FUEL_SELECTOR_LEFT_MAIN";

        public const string FUEL_SELECTOR_OFF = "FUEL_SELECTOR_OFF";

        public const string FUEL_SELECTOR_RIGHT = "FUEL_SELECTOR_RIGHT";

        public const string FUEL_SELECTOR_RIGHT_AUX = "FUEL_SELECTOR_RIGHT_AUX";

        public const string FUEL_SELECTOR_RIGHT_MAIN = "FUEL_SELECTOR_RIGHT_MAIN";

        public const string FUEL_SELECTOR_SET = "FUEL_SELECTOR_SET";

      }
      public static class AircraftFuelSystem
      {
        [Parameter(0, "Junction Index")]
        [Parameter(1, "Option index")]
        public const string FUELSYSTEM_JUNCTION_SET = "FUELSYSTEM_JUNCTION_SET";

        [Parameter(0, "Pump Index")]
        public const string FUELSYSTEM_PUMP_OFF = "FUELSYSTEM_PUMP_OFF";

        [Parameter(0, "Pump Index")]
        public const string FUELSYSTEM_PUMP_ON = "FUELSYSTEM_PUMP_ON";

        [Parameter(0, "Pump Index")]
        [Parameter(1, "Status                   0 = Off                   1 = On                   2 = Auto")]
        public const string FUELSYSTEM_PUMP_SET = "FUELSYSTEM_PUMP_SET";

        [Parameter(0, "Pump Index")]
        public const string FUELSYSTEM_PUMP_TOGGLE = "FUELSYSTEM_PUMP_TOGGLE";

        [Parameter(0, "Trigger Index")]
        public const string FUELSYSTEM_TRIGGER_OFF = "FUELSYSTEM_TRIGGER_OFF";

        [Parameter(0, "Trigger Index")]
        public const string FUELSYSTEM_TRIGGER_ON = "FUELSYSTEM_TRIGGER_ON";

        [Parameter(0, "Trigger Index")]
        [Parameter(1, "Status, either on (1) or off (0)")]
        public const string FUELSYSTEM_TRIGGER_SET = "FUELSYSTEM_TRIGGER_SET";

        [Parameter(0, "Trigger Index")]
        public const string FUELSYSTEM_TRIGGER_TOGGLE = "FUELSYSTEM_TRIGGER_TOGGLE";

        [Parameter(0, "Valve Index")]
        public const string FUELSYSTEM_VALVE_CLOSE = "FUELSYSTEM_VALVE_CLOSE";

        [Parameter(0, "Valve Index")]
        public const string FUELSYSTEM_VALVE_OPEN = "FUELSYSTEM_VALVE_OPEN";

        [Parameter(0, "Valve Index")]
        [Parameter(1, "Status, either open (1) or closed (0)")]
        public const string FUELSYSTEM_VALVE_SET = "FUELSYSTEM_VALVE_SET";

        [Parameter(0, "Valve Index")]
        public const string FUELSYSTEM_VALVE_TOGGLE = "FUELSYSTEM_VALVE_TOGGLE";

      }
      public static class FuelTransferK
      {
        public const string SET_FUEL_TRANSFER_CUSTOM = "SET_FUEL_TRANSFER_CUSTOM";

        public const string FUEL_TRANSFER_CUSTOM_INDEX_TOGGLE = "FUEL_TRANSFER_CUSTOM_INDEX_TOGGLE";

        public const string SET_FUEL_TRANSFER_FORWARD = "SET_FUEL_TRANSFER_FORWARD";

        public const string SET_FUEL_TRANSFER_AFT = "SET_FUEL_TRANSFER_AFT";

        public const string SET_FUEL_TRANSFER_AUTO = "SET_FUEL_TRANSFER_AUTO";

        public const string SET_FUEL_TRANSFER_OFF = "SET_FUEL_TRANSFER_OFF";

      }
      public static class Miscellaneous
      {
        [Parameter(0, "The fuel quantity")]
        public const string ADD_FUEL_QUANTITY = "ADD_FUEL_QUANTITY";

        public const string ELECT_FUEL_PUMP1_SET = "ELECT_FUEL_PUMP1_SET";

        public const string ELECT_FUEL_PUMP2_SET = "ELECT_FUEL_PUMP2_SET";

        public const string ELECT_FUEL_PUMP3_SET = "ELECT_FUEL_PUMP3_SET";

        public const string ELECT_FUEL_PUMP4_SET = "ELECT_FUEL_PUMP4_SET";

        public const string ENGINE_FUELFLOW_BUG_POSITION1 = "ENGINE_FUELFLOW_BUG_POSITION1";

        public const string ENGINE_FUELFLOW_BUG_POSITION2 = "ENGINE_FUELFLOW_BUG_POSITION2";

        public const string ENGINE_FUELFLOW_BUG_POSITION3 = "ENGINE_FUELFLOW_BUG_POSITION3";

        public const string ENGINE_FUELFLOW_BUG_POSITION4 = "ENGINE_FUELFLOW_BUG_POSITION4";

        public const string FUEL_DUMP_SWITCH_SET = "FUEL_DUMP_SWITCH_SET";

        public const string FUEL_DUMP_TOGGLE = "FUEL_DUMP_TOGGLE";

        public const string MANUAL_FUEL_PRESSURE_PUMP = "MANUAL_FUEL_PRESSURE_PUMP";

        [Parameter(0, "The pump index")]
        [Parameter(1, "A value between 0 and 16384")]
        public const string MANUAL_FUEL_PRESSURE_PUMP_SET = "MANUAL_FUEL_PRESSURE_PUMP_SET";

        public const string MANUAL_FUEL_TRANSFER = "MANUAL_FUEL_TRANSFER";

        public const string RELEASE_DROP_TANK_ALL = "RELEASE_DROP_TANK_ALL";

        public const string RELEASE_DROP_TANK_1 = "RELEASE_DROP_TANK_1";

        public const string RELEASE_DROP_TANK_2 = "RELEASE_DROP_TANK_2";

        public const string REPAIR_AND_REFUEL = "REPAIR_AND_REFUEL";

        public const string REQUEST_FUEL_KEY = "REQUEST_FUEL_KEY";

        [Parameter(0, "Tank index (optional)")]
        public const string ANTIDETONATION_TANK_VALVE_TOGGLE = "ANTIDETONATION_TANK_VALVE_TOGGLE";

        [Parameter(0, "Tank index (optional)")]
        public const string NITROUS_TANK_VALVE_TOGGLE = "NITROUS_TANK_VALVE_TOGGLE";

      }
      public static class FuelSelectorCodes
      {
        public const string FUEL_TANK_SELECTOR_OFF = "FUEL_TANK_SELECTOR_OFF";

        public const string FUEL_TANK_SELECTOR_ALL = "FUEL_TANK_SELECTOR_ALL";

        public const string FUEL_TANK_SELECTOR_LEFT = "FUEL_TANK_SELECTOR_LEFT";

        public const string FUEL_TANK_SELECTOR_RIGHT = "FUEL_TANK_SELECTOR_RIGHT";

        public const string FUEL_TANK_SELECTOR_LEFT_AUX = "FUEL_TANK_SELECTOR_LEFT_AUX";

        public const string FUEL_TANK_SELECTOR_RIGHT_AUX = "FUEL_TANK_SELECTOR_RIGHT_AUX";

        public const string FUEL_TANK_SELECTOR_CENTER = "FUEL_TANK_SELECTOR_CENTER";

        public const string FUEL_TANK_SELECTOR_CENTER2 = "FUEL_TANK_SELECTOR_CENTER2";

        public const string FUEL_TANK_SELECTOR_CENTER3 = "FUEL_TANK_SELECTOR_CENTER3";

        public const string FUEL_TANK_SELECTOR_EXTERNAL1 = "FUEL_TANK_SELECTOR_EXTERNAL1";

        public const string FUEL_TANK_SELECTOR_EXTERNAL2 = "FUEL_TANK_SELECTOR_EXTERNAL2";

        public const string FUEL_TANK_SELECTOR_RIGHT_TIP = "FUEL_TANK_SELECTOR_RIGHT_TIP";

        public const string FUEL_TANK_SELECTOR_LEFT_TIP = "FUEL_TANK_SELECTOR_LEFT_TIP";

        public const string FUEL_TANK_SELECTOR_CROSSFEED = "FUEL_TANK_SELECTOR_CROSSFEED";

        public const string FUEL_TANK_SELECTOR_CROSSFEED_L2R = "FUEL_TANK_SELECTOR_CROSSFEED_L2R";

        public const string FUEL_TANK_SELECTOR_CROSSFEED_R2L = "FUEL_TANK_SELECTOR_CROSSFEED_R2L";

        public const string FUEL_TANK_SELECTOR_BOTH = "FUEL_TANK_SELECTOR_BOTH";

        public const string FUEL_TANK_SELECTOR_EXTERNAL_ALL = "FUEL_TANK_SELECTOR_EXTERNAL_ALL";

        public const string FUEL_TANK_SELECTOR_ISOLATE = "FUEL_TANK_SELECTOR_ISOLATE";

      }
    }


  }
}

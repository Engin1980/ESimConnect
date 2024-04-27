namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftInstrumentation
    {
      public static class AircraftInstruments
      {
        public const string ATTITUDE_BARS_POSITION_DOWN = "ATTITUDE_BARS_POSITION_DOWN";

        public const string ATTITUDE_BARS_POSITION_UP = "ATTITUDE_BARS_POSITION_UP";

        [Parameter(0, "Index")]
        [Parameter(1, "Value")]
        public const string ATTITUDE_BARS_POSITION_SET = "ATTITUDE_BARS_POSITION_SET";

        public const string BAROMETRIC = "BAROMETRIC";

        [Parameter(0, "the index of the altimeter")]
        public const string BAROMETRIC_STD_PRESSURE = "BAROMETRIC_STD_PRESSURE";

        public const string GAUGE_KEYSTROKE = "GAUGE_KEYSTROKE";

        public const string GYRO_DRIFT_DEC = "GYRO_DRIFT_DEC";

        public const string GYRO_DRIFT_INC = "GYRO_DRIFT_INC";

        [Parameter(0, "Drift angle (degrees)")]
        public const string GYRO_DRIFT_SET = "GYRO_DRIFT_SET";

        public const string GYRO_DRIFT_SET_EX1 = "GYRO_DRIFT_SET_EX1";

        public const string HEADING_GYRO_SET = "HEADING_GYRO_SET";

        public const string INDUCTOR_COMPASS_REF_INC = "INDUCTOR_COMPASS_REF_INC";

        public const string INDUCTOR_COMPASS_REF_DEC = "INDUCTOR_COMPASS_REF_DEC";

        public const string KOHLSMAN_INC = "KOHLSMAN_INC";

        public const string KOHLSMAN_DEC = "KOHLSMAN_DEC";

        [Parameter(0, "Value to set")]
        [Parameter(1, "Altimeter index")]
        public const string KOHLSMAN_SET = "KOHLSMAN_SET";

        public const string RESET_G_FORCE_INDICATOR = "RESET_G_FORCE_INDICATOR";

        public const string RESET_MAX_RPM_INDICATOR = "RESET_MAX_RPM_INDICATOR";

        public const string ATTITUDE_CAGE_BUTTON = "ATTITUDE_CAGE_BUTTON";

        public const string TOGGLE_ICS = "TOGGLE_ICS";

        public const string TOGGLE_SPEAKER = "TOGGLE_SPEAKER";

        public const string TOGGLE_TURN_INDICATOR_SWITCH = "TOGGLE_TURN_INDICATOR_SWITCH";

        public const string TOGGLE_VARIOMETER_SWITCH = "TOGGLE_VARIOMETER_SWITCH";

        public const string TRUE_AIRSPEED_CAL_DEC = "TRUE_AIRSPEED_CAL_DEC";

        public const string TRUE_AIRSPEED_CAL_INC = "TRUE_AIRSPEED_CAL_INC";

        [Parameter(0, "Degrees")]
        public const string TRUE_AIRSPEED_CAL_SET = "TRUE_AIRSPEED_CAL_SET";

        public const string VARIOMETER_SOUND_TOGGLE = "VARIOMETER_SOUND_TOGGLE";

      }
      public static class Atc
      {
        public const string ATC = "ATC";

        public const string ATC_MENU_CLOSE = "ATC_MENU_CLOSE";

        public const string ATC_MENU_OPEN = "ATC_MENU_OPEN";

        public const string ATC_MENU_0 = "ATC_MENU_0";

        public const string ATC_MENU_1 = "ATC_MENU_1";

        public const string ATC_MENU_2 = "ATC_MENU_2";

        public const string ATC_MENU_3 = "ATC_MENU_3";

        public const string ATC_MENU_4 = "ATC_MENU_4";

        public const string ATC_MENU_5 = "ATC_MENU_5";

        public const string ATC_MENU_6 = "ATC_MENU_6";

        public const string ATC_MENU_7 = "ATC_MENU_7";

        public const string ATC_MENU_8 = "ATC_MENU_8";

        public const string ATC_MENU_9 = "ATC_MENU_9";

      }
      public static class Egt
      {
        public const string EGT = "EGT";

        public const string EGT_DEC = "EGT_DEC";

        public const string EGT_INC = "EGT_INC";

        [Parameter(0, "Bug value (0 to 32768)")]
        public const string EGT_SET = "EGT_SET";

        public const string EGT1_DEC = "EGT1_DEC";

        public const string EGT2_DEC = "EGT2_DEC";

        public const string EGT3_DEC = "EGT3_DEC";

        public const string EGT4_DEC = "EGT4_DEC";

        public const string EGT1_INC = "EGT1_INC";

        public const string EGT2_INC = "EGT2_INC";

        public const string EGT3_INC = "EGT3_INC";

        public const string EGT4_INC = "EGT4_INC";

        [Parameter(0, "Bug value (0 to 32768)")]
        public const string EGT1_SET = "EGT1_SET";

        [Parameter(0, "Bug value (0 to 32768)")]
        public const string EGT2_SET = "EGT2_SET";

        [Parameter(0, "Bug value (0 to 32768)")]
        public const string EGT3_SET = "EGT3_SET";

        [Parameter(0, "Bug value (0 to 32768)")]
        public const string EGT4_SET = "EGT4_SET";

      }
    }

  }
}

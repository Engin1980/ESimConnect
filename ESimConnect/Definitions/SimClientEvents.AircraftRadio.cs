namespace ESimConnect
{
  public partial class SimClientEvents
  {
    public static class AircraftRadio
    {
      public static class Adf
      {
        public const string ADF = "ADF";

        public const string ADF_CARD_DEC = "ADF_CARD_DEC";

        public const string ADF_CARD_INC = "ADF_CARD_INC";

        [Parameter(0, "Card value")]
        public const string ADF_CARD_SET = "ADF_CARD_SET";

        public const string ADF_1_DEC = "ADF_1_DEC";

        public const string ADF2_1_DEC = "ADF2_1_DEC";

        public const string ADF_10_DEC = "ADF_10_DEC";

        public const string ADF2_10_DEC = "ADF2_10_DEC";

        public const string ADF_100_DEC = "ADF_100_DEC";

        public const string ADF2_100_DEC = "ADF2_100_DEC";

        public const string ADF_1_INC = "ADF_1_INC";

        public const string ADF2_1_INC = "ADF2_1_INC";

        public const string ADF_10_INC = "ADF_10_INC";

        public const string ADF2_10_INC = "ADF2_10_INC";

        public const string ADF_100_INC = "ADF_100_INC";

        public const string ADF2_100_INC = "ADF2_100_INC";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF_ACTIVE_SET = "ADF_ACTIVE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF2_ACTIVE_SET = "ADF2_ACTIVE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF_COMPLETE_SET = "ADF_COMPLETE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF2_COMPLETE_SET = "ADF2_COMPLETE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF_EXTENDED_SET = "ADF_EXTENDED_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF2_EXTENDED_SET = "ADF2_EXTENDED_SET";

        public const string ADF_FRACT_DEC_CARRY = "ADF_FRACT_DEC_CARRY";

        public const string ADF2_FRACT_DEC_CARRY = "ADF2_FRACT_DEC_CARRY";

        public const string ADF_FRACT_INC_CARRY = "ADF_FRACT_INC_CARRY";

        public const string ADF2_FRACT_INC_CARRY = "ADF2_FRACT_INC_CARRY";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF_HIGHRANGE_SET = "ADF_HIGHRANGE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF2_HIGHRANGE_SET = "ADF2_HIGHRANGE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF_LOWRANGE_SET = "ADF_LOWRANGE_SET";

        [Parameter(0, "Frequency value (BCD32 encoded Hz)")]
        public const string ADF2_LOWRANGE_SET = "ADF2_LOWRANGE_SET";

        [Parameter(0, "Needle value")]
        public const string ADF_NEEDLE_SET = "ADF_NEEDLE_SET";

        [Parameter(0, "Needle value")]
        public const string ADF2_NEEDLE_SET = "ADF2_NEEDLE_SET";

        [Parameter(0, "Bool")]
        public const string ADF_OUTSIDE_SOURCE = "ADF_OUTSIDE_SOURCE";

        [Parameter(0, "Bool")]
        public const string ADF2_OUTSIDE_SOURCE = "ADF2_OUTSIDE_SOURCE";

        public const string ADF1_RADIO_SWAP = "ADF1_RADIO_SWAP";

        public const string ADF2_RADIO_SWAP = "ADF2_RADIO_SWAP";

        public const string ADF1_RADIO_TENTHS_DEC = "ADF1_RADIO_TENTHS_DEC";

        public const string ADF2_RADIO_TENTHS_DEC = "ADF2_RADIO_TENTHS_DEC";

        public const string ADF1_RADIO_TENTHS_INC = "ADF1_RADIO_TENTHS_INC";

        public const string ADF2_RADIO_TENTHS_INC = "ADF2_RADIO_TENTHS_INC";

        [Parameter(0, "Frequency value")]
        public const string ADF_SET = "ADF_SET";

        [Parameter(0, "Frequency value")]
        public const string ADF2_SET = "ADF2_SET";

        [Parameter(0, "Frequency value")]
        public const string ADF_STBY_SET = "ADF_STBY_SET";

        [Parameter(0, "Frequency value")]
        public const string ADF2_STBY_SET = "ADF2_STBY_SET";

        public const string ADF_VOLUME_INC = "ADF_VOLUME_INC";

        public const string ADF2_VOLUME_INC = "ADF2_VOLUME_INC";

        public const string ADF_VOLUME_DEC = "ADF_VOLUME_DEC";

        public const string ADF2_VOLUME_DEC = "ADF2_VOLUME_DEC";

        [Parameter(0, "Volume value")]
        public const string ADF_VOLUME_SET = "ADF_VOLUME_SET";

        [Parameter(0, "Volume value")]
        public const string ADF2_VOLUME_SET = "ADF2_VOLUME_SET";

        public const string ADF1_WHOLE_DEC = "ADF1_WHOLE_DEC";

        public const string ADF2_WHOLE_DEC = "ADF2_WHOLE_DEC";

        public const string ADF1_WHOLE_INC = "ADF1_WHOLE_INC";

        public const string ADF2_WHOLE_INC = "ADF2_WHOLE_INC";

        public const string RADIO_ADF_IDENT_DISABLE = "RADIO_ADF_IDENT_DISABLE";

        public const string RADIO_ADF2_IDENT_DISABLE = "RADIO_ADF2_IDENT_DISABLE";

        public const string RADIO_ADF_IDENT_ENABLE = "RADIO_ADF_IDENT_ENABLE";

        public const string RADIO_ADF2_IDENT_ENABLE = "RADIO_ADF2_IDENT_ENABLE";

        [Parameter(0, "True/False (1, 0)")]
        public const string RADIO_ADF_IDENT_SET = "RADIO_ADF_IDENT_SET";

        [Parameter(0, "True/False (1, 0)")]
        public const string RADIO_ADF2_IDENT_SET = "RADIO_ADF2_IDENT_SET";

        public const string RADIO_ADF_IDENT_TOGGLE = "RADIO_ADF_IDENT_TOGGLE";

        public const string RADIO_ADF2_IDENT_TOGGLE = "RADIO_ADF2_IDENT_TOGGLE";

      }
      public static class AircraftAvionics
      {
        [Parameter(0, "Bool")]
        public const string AVIONICS_MASTER_SET = "AVIONICS_MASTER_SET";

        public const string TOGGLE_AVIONICS_MASTER = "TOGGLE_AVIONICS_MASTER";

        public const string AVIONICS_MASTER_1_ON = "AVIONICS_MASTER_1_ON";

        public const string AVIONICS_MASTER_2_ON = "AVIONICS_MASTER_2_ON";

        public const string AVIONICS_MASTER_1_OFF = "AVIONICS_MASTER_1_OFF";

        public const string AVIONICS_MASTER_2_OFF = "AVIONICS_MASTER_2_OFF";

        [Parameter(0, "Bool")]
        public const string AVIONICS_MASTER_1_SET = "AVIONICS_MASTER_1_SET";

        [Parameter(0, "Bool")]
        public const string AVIONICS_MASTER_2_SET = "AVIONICS_MASTER_2_SET";

      }
      public static class Com
      {
        public const string COM_RADIO = "COM_RADIO";

        public const string COM_RADIO_FRACT_DEC = "COM_RADIO_FRACT_DEC";

        public const string COM2_RADIO_FRACT_DEC = "COM2_RADIO_FRACT_DEC";

        public const string COM3_RADIO_FRACT_DEC = "COM3_RADIO_FRACT_DEC";

        public const string COM_RADIO_FRACT_DEC_CARRY = "COM_RADIO_FRACT_DEC_CARRY";

        public const string COM2_RADIO_FRACT_DEC_CARRY = "COM2_RADIO_FRACT_DEC_CARRY";

        public const string COM3_RADIO_FRACT_DEC_CARRY = "COM3_RADIO_FRACT_DEC_CARRY";

        public const string COM_RADIO_FRACT_INC = "COM_RADIO_FRACT_INC";

        public const string COM2_RADIO_FRACT_INC = "COM2_RADIO_FRACT_INC";

        public const string COM3_RADIO_FRACT_INC = "COM3_RADIO_FRACT_INC";

        public const string COM_RADIO_FRACT_INC_CARRY = "COM_RADIO_FRACT_INC_CARRY";

        public const string COM2_RADIO_FRACT_INC_CARRY = "COM2_RADIO_FRACT_INC_CARRY";

        public const string COM3_RADIO_FRACT_INC_CARRY = "COM3_RADIO_FRACT_INC_CARRY";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM_RADIO_SET = "COM_RADIO_SET";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM2_RADIO_SET = "COM2_RADIO_SET";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM3_RADIO_SET = "COM3_RADIO_SET";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM_RADIO_SET_HZ = "COM_RADIO_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM2_RADIO_SET_HZ = "COM2_RADIO_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM3_RADIO_SET_HZ = "COM3_RADIO_SET_HZ";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM_STBY_RADIO_SET = "COM_STBY_RADIO_SET";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM2_STBY_RADIO_SET = "COM2_STBY_RADIO_SET";

        [Parameter(0, "Frequency value (BCD16 encoded Hz)")]
        public const string COM3_STBY_RADIO_SET = "COM3_STBY_RADIO_SET";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM_STBY_RADIO_SET_HZ = "COM_STBY_RADIO_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM2_STBY_RADIO_SET_HZ = "COM2_STBY_RADIO_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM3_STBY_RADIO_SET_HZ = "COM3_STBY_RADIO_SET_HZ";

        public const string COM_STBY_RADIO_SWAP = "COM_STBY_RADIO_SWAP";

        public const string COM_RADIO_WHOLE_DEC = "COM_RADIO_WHOLE_DEC";

        public const string COM2_RADIO_WHOLE_DEC = "COM2_RADIO_WHOLE_DEC";

        public const string COM3_RADIO_WHOLE_DEC = "COM3_RADIO_WHOLE_DEC";

        public const string COM_RADIO_WHOLE_INC = "COM_RADIO_WHOLE_INC";

        public const string COM2_RADIO_WHOLE_INC = "COM2_RADIO_WHOLE_INC";

        public const string COM3_RADIO_WHOLE_INC = "COM3_RADIO_WHOLE_INC";

        public const string COM_RADIO_SWAP = "COM_RADIO_SWAP";

        public const string COM1_RADIO_SWAP = "COM1_RADIO_SWAP";

        public const string COM2_RADIO_SWAP = "COM2_RADIO_SWAP";

        public const string COM3_RADIO_SWAP = "COM3_RADIO_SWAP";

        public const string COM1_RECEIVE_SELECT = "COM1_RECEIVE_SELECT";

        public const string COM2_RECEIVE_SELECT = "COM2_RECEIVE_SELECT";

        public const string COM3_RECEIVE_SELECT = "COM3_RECEIVE_SELECT";

        public const string COM_1_SPACING_MODE_SWITCH = "COM_1_SPACING_MODE_SWITCH";

        public const string COM_2_SPACING_MODE_SWITCH = "COM_2_SPACING_MODE_SWITCH";

        public const string COM_3_SPACING_MODE_SWITCH = "COM_3_SPACING_MODE_SWITCH";

        [Parameter(0, "Frequency value (BCD16 or BCD32 encoded Hz)")]
        public const string COM1_STORED_FREQUENCY_SET = "COM1_STORED_FREQUENCY_SET";

        [Parameter(0, "Frequency value (BCD16 or BCD32 encoded Hz)")]
        public const string COM2_STORED_FREQUENCY_SET = "COM2_STORED_FREQUENCY_SET";

        [Parameter(0, "Frequency value (BCD16 or BCD32 encoded Hz)")]
        public const string COM3_STORED_FREQUENCY_SET = "COM3_STORED_FREQUENCY_SET";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM1_STORED_FREQUENCY_SET_HZ = "COM1_STORED_FREQUENCY_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM2_STORED_FREQUENCY_SET_HZ = "COM2_STORED_FREQUENCY_SET_HZ";

        [Parameter(0, "Frequency value (Hz)")]
        public const string COM3_STORED_FREQUENCY_SET_HZ = "COM3_STORED_FREQUENCY_SET_HZ";

        public const string COM1_STORED_FREQUENCY_INDEX_SET = "COM1_STORED_FREQUENCY_INDEX_SET";

        public const string COM2_STORED_FREQUENCY_INDEX_SET = "COM2_STORED_FREQUENCY_INDEX_SET";

        public const string COM3_STORED_FREQUENCY_INDEX_SET = "COM3_STORED_FREQUENCY_INDEX_SET";

        [Deprecated]
        public const string COM1_TRANSMIT_SELECT = "COM1_TRANSMIT_SELECT";

        [Deprecated]
        public const string COM2_TRANSMIT_SELECT = "COM2_TRANSMIT_SELECT";

        [Parameter(0, "Volume (0 - 1)")]
        public const string COM1_VOLUME_SET = "COM1_VOLUME_SET";

        [Parameter(0, "Volume (0 - 1)")]
        public const string COM2_VOLUME_SET = "COM2_VOLUME_SET";

        [Parameter(0, "Volume (0 - 1)")]
        public const string COM3_VOLUME_SET = "COM3_VOLUME_SET";

        public const string COM1_VOLUME_INC = "COM1_VOLUME_INC";

        public const string COM2_VOLUME_INC = "COM2_VOLUME_INC";

        public const string COM3_VOLUME_INC = "COM3_VOLUME_INC";

        public const string COM1_VOLUME_DEC = "COM1_VOLUME_DEC";

        public const string COM2_VOLUME_DEC = "COM2_VOLUME_DEC";

        public const string COM3_VOLUME_DEC = "COM3_VOLUME_DEC";

        public const string COM_RECEIVE_ALL_SET = "COM_RECEIVE_ALL_SET";

        public const string COM_RECEIVE_ALL_TOGGLE = "COM_RECEIVE_ALL_TOGGLE";

        public const string RADIO_COMMNAV1_TEST_TOGGLE = "RADIO_COMMNAV1_TEST_TOGGLE";

        public const string RADIO_COMMNAV2_TEST_TOGGLE = "RADIO_COMMNAV2_TEST_TOGGLE";

        public const string RADIO_COMMNAV3_TEST_TOGGLE = "RADIO_COMMNAV3_TEST_TOGGLE";

        public const string RADIO_COMM1_AUTOSWITCH_TOGGLE = "RADIO_COMM1_AUTOSWITCH_TOGGLE";

        public const string RADIO_COMM2_AUTOSWITCH_TOGGLE = "RADIO_COMM2_AUTOSWITCH_TOGGLE";

      }
      public static class Dme
      {
        public const string DME = "DME";

        [Parameter(0, "DME ID")]
        public const string DME_SELECT = "DME_SELECT";

        public const string TOGGLE_DME = "TOGGLE_DME";

        public const string DME1_TOGGLE = "DME1_TOGGLE";

        public const string DME2_TOGGLE = "DME2_TOGGLE";

        public const string RADIO_DME1_IDENT_DISABLE = "RADIO_DME1_IDENT_DISABLE";

        public const string RADIO_DME2_IDENT_DISABLE = "RADIO_DME2_IDENT_DISABLE";

        public const string RADIO_DME1_IDENT_ENABLE = "RADIO_DME1_IDENT_ENABLE";

        public const string RADIO_DME2_IDENT_ENABLE = "RADIO_DME2_IDENT_ENABLE";

        [Parameter(0, "Bool")]
        public const string RADIO_DME1_IDENT_SET = "RADIO_DME1_IDENT_SET";

        [Parameter(0, "Bool")]
        public const string RADIO_DME2_IDENT_SET = "RADIO_DME2_IDENT_SET";

        public const string RADIO_DME1_IDENT_TOGGLE = "RADIO_DME1_IDENT_TOGGLE";

        public const string RADIO_DME2_IDENT_TOGGLE = "RADIO_DME2_IDENT_TOGGLE";

        public const string RADIO_SELECTED_DME_IDENT_ENABLE = "RADIO_SELECTED_DME_IDENT_ENABLE";

        public const string RADIO_SELECTED_DME_IDENT_DISABLE = "RADIO_SELECTED_DME_IDENT_DISABLE";

        [Parameter(0, "Bool")]
        public const string RADIO_SELECTED_DME_IDENT_SET = "RADIO_SELECTED_DME_IDENT_SET";

        public const string RADIO_SELECTED_DME_IDENT_TOGGLE = "RADIO_SELECTED_DME_IDENT_TOGGLE";

      }
      public static class Elt
      {
        public const string ELT_OFF = "ELT_OFF";

        public const string ELT_ON = "ELT_ON";

        public const string ELT_SET = "ELT_SET";

        public const string ELT_TOGGLE = "ELT_TOGGLE";

      }
      public static class Gps
      {
        public const string GPS_ACTIVATE_BUTTON = "GPS_ACTIVATE_BUTTON";

        public const string GPS_BUTTON1 = "GPS_BUTTON1";

        public const string GPS_BUTTON2 = "GPS_BUTTON2";

        public const string GPS_BUTTON3 = "GPS_BUTTON3";

        public const string GPS_BUTTON4 = "GPS_BUTTON4";

        public const string GPS_BUTTON5 = "GPS_BUTTON5";

        public const string GPS_CLEAR_BUTTON = "GPS_CLEAR_BUTTON";

        public const string GPS_CLEAR_ALL_BUTTON = "GPS_CLEAR_ALL_BUTTON";

        public const string GPS_CLEAR_BUTTON_DOWN = "GPS_CLEAR_BUTTON_DOWN";

        public const string GPS_CLEAR_BUTTON_UP = "GPS_CLEAR_BUTTON_UP";

        public const string GPS_CURSOR_BUTTON = "GPS_CURSOR_BUTTON";

        public const string GPS_DIRECTTO_BUTTON = "GPS_DIRECTTO_BUTTON";

        public const string GPS_ENTER_BUTTON = "GPS_ENTER_BUTTON";

        public const string GPS_FLIGHTPLAN_BUTTON = "GPS_FLIGHTPLAN_BUTTON";

        public const string GPS_GROUP_KNOB_INC = "GPS_GROUP_KNOB_INC";

        public const string GPS_GROUP_KNOB_DEC = "GPS_GROUP_KNOB_DEC";

        public const string GPS_MENU_BUTTON = "GPS_MENU_BUTTON";

        public const string GPS_MSG_BUTTON = "GPS_MSG_BUTTON";

        public const string GPS_MSG_BUTTON_DOWN = "GPS_MSG_BUTTON_DOWN";

        public const string GPS_MSG_BUTTON_UP = "GPS_MSG_BUTTON_UP";

        public const string GPS_NEAREST_BUTTON = "GPS_NEAREST_BUTTON";

        public const string GPS_OBS = "GPS_OBS";

        public const string GPS_OBS_BUTTON = "GPS_OBS_BUTTON";

        public const string GPS_OBS_DEC = "GPS_OBS_DEC";

        public const string GPS_OBS_INC = "GPS_OBS_INC";

        public const string GPS_OBS_OFF = "GPS_OBS_OFF";

        public const string GPS_OBS_ON = "GPS_OBS_ON";

        [Parameter(0, "Value in degrees")]
        public const string GPS_OBS_SET = "GPS_OBS_SET";

        public const string GPS_PAGE_KNOB_INC = "GPS_PAGE_KNOB_INC";

        public const string GPS_PAGE_KNOB_DEC = "GPS_PAGE_KNOB_DEC";

        public const string GPS_PROCEDURE_BUTTON = "GPS_PROCEDURE_BUTTON";

        public const string GPS_POWER_BUTTON = "GPS_POWER_BUTTON";

        public const string GPS_SETUP_BUTTON = "GPS_SETUP_BUTTON";

        public const string GPS_TERRAIN_BUTTON = "GPS_TERRAIN_BUTTON";

        public const string GPS_VNAV_BUTTON = "GPS_VNAV_BUTTON";

        public const string GPS_ZOOMIN_BUTTON = "GPS_ZOOMIN_BUTTON";

        public const string GPS_ZOOMOUT_BUTTON = "GPS_ZOOMOUT_BUTTON";

        public const string TOGGLE_GPS_DRIVES_NAV1 = "TOGGLE_GPS_DRIVES_NAV1";

      }
      public static class Misc
      {
        public const string COPILOT_TRANSMITTER_SET = "COPILOT_TRANSMITTER_SET";

        public const string FREQUENCY_SWAP = "FREQUENCY_SWAP";

        public const string INTERCOM_MODE_SET = "INTERCOM_MODE_SET";

        public const string MARKER_BEACON_SENSITIVITY_HIGH = "MARKER_BEACON_SENSITIVITY_HIGH";

        public const string MARKER_BEACON_TEST_MUTE = "MARKER_BEACON_TEST_MUTE";

        public const string MARKER_SOUND_TOGGLE = "MARKER_SOUND_TOGGLE";

        [Parameter(0, "Bool")]
        public const string MARKER_SOUND_SET = "MARKER_SOUND_SET";

        [Parameter(0, "The Com channel to select.")]
        public const string PILOT_TRANSMITTER_SET = "PILOT_TRANSMITTER_SET";

        public const string TOGGLE_RADAR = "TOGGLE_RADAR";

        public const string TOGGLE_RADIO = "TOGGLE_RADIO";

        public const string TOGGLE_RAD_INS_SWITCH = "TOGGLE_RAD_INS_SWITCH";

      }
      public static class Nav
      {
        public const string NAV_RADIO = "NAV_RADIO";

        [Parameter(0, "Bool")]
        public const string NAV1_CLOSE_FREQ_SET = "NAV1_CLOSE_FREQ_SET";

        [Parameter(0, "Bool")]
        public const string NAV2_CLOSE_FREQ_SET = "NAV2_CLOSE_FREQ_SET";

        [Parameter(0, "Bool")]
        public const string NAV3_CLOSE_FREQ_SET = "NAV3_CLOSE_FREQ_SET";

        [Parameter(0, "Bool")]
        public const string NAV4_CLOSE_FREQ_SET = "NAV4_CLOSE_FREQ_SET";

        public const string NAV1_RADIO_FRACT_DEC = "NAV1_RADIO_FRACT_DEC";

        public const string NAV2_RADIO_FRACT_DEC = "NAV2_RADIO_FRACT_DEC";

        public const string NAV3_RADIO_FRACT_DEC = "NAV3_RADIO_FRACT_DEC";

        public const string NAV4_RADIO_FRACT_DEC = "NAV4_RADIO_FRACT_DEC";

        public const string NAV1_RADIO_FRACT_DEC_CARRY = "NAV1_RADIO_FRACT_DEC_CARRY";

        public const string NAV2_RADIO_FRACT_DEC_CARRY = "NAV2_RADIO_FRACT_DEC_CARRY";

        public const string NAV3_RADIO_FRACT_DEC_CARRY = "NAV3_RADIO_FRACT_DEC_CARRY";

        public const string NAV4_RADIO_FRACT_DEC_CARRY = "NAV4_RADIO_FRACT_DEC_CARRY";

        public const string NAV1_RADIO_FRACT_INC = "NAV1_RADIO_FRACT_INC";

        public const string NAV2_RADIO_FRACT_INC = "NAV2_RADIO_FRACT_INC";

        public const string NAV3_RADIO_FRACT_INC = "NAV3_RADIO_FRACT_INC";

        public const string NAV4_RADIO_FRACT_INC = "NAV4_RADIO_FRACT_INC";

        public const string NAV1_RADIO_FRACT_INC_CARRY = "NAV1_RADIO_FRACT_INC_CARRY";

        public const string NAV2_RADIO_FRACT_INC_CARRY = "NAV2_RADIO_FRACT_INC_CARRY";

        public const string NAV3_RADIO_FRACT_INC_CARRY = "NAV3_RADIO_FRACT_INC_CARRY";

        public const string NAV4_RADIO_FRACT_INC_CARRY = "NAV4_RADIO_FRACT_INC_CARRY";

        public const string NAV1_RADIO_SET = "NAV1_RADIO_SET";

        public const string NAV2_RADIO_SET = "NAV2_RADIO_SET";

        public const string NAV3_RADIO_SET = "NAV3_RADIO_SET";

        public const string NAV4_RADIO_SET = "NAV4_RADIO_SET";

        public const string NAV1_RADIO_SET_HZ = "NAV1_RADIO_SET_HZ";

        public const string NAV2_RADIO_SET_HZ = "NAV2_RADIO_SET_HZ";

        public const string NAV3_RADIO_SET_HZ = "NAV3_RADIO_SET_HZ";

        public const string NAV4_RADIO_SET_HZ = "NAV4_RADIO_SET_HZ";

        public const string NAV1_RADIO_SWAP = "NAV1_RADIO_SWAP";

        public const string NAV2_RADIO_SWAP = "NAV2_RADIO_SWAP";

        public const string NAV3_RADIO_SWAP = "NAV3_RADIO_SWAP";

        public const string NAV4_RADIO_SWAP = "NAV4_RADIO_SWAP";

        public const string NAV1_RADIO_WHOLE_DEC = "NAV1_RADIO_WHOLE_DEC";

        public const string NAV2_RADIO_WHOLE_DEC = "NAV2_RADIO_WHOLE_DEC";

        public const string NAV3_RADIO_WHOLE_DEC = "NAV3_RADIO_WHOLE_DEC";

        public const string NAV4_RADIO_WHOLE_DEC = "NAV4_RADIO_WHOLE_DEC";

        public const string NAV1_RADIO_WHOLE_INC = "NAV1_RADIO_WHOLE_INC";

        public const string NAV2_RADIO_WHOLE_INC = "NAV2_RADIO_WHOLE_INC";

        public const string NAV3_RADIO_WHOLE_INC = "NAV3_RADIO_WHOLE_INC";

        public const string NAV4_RADIO_WHOLE_INC = "NAV4_RADIO_WHOLE_INC";

        public const string NAV1_STBY_SET = "NAV1_STBY_SET";

        public const string NAV2_STBY_SET = "NAV2_STBY_SET";

        public const string NAV3_STBY_SET = "NAV3_STBY_SET";

        public const string NAV4_STBY_SET = "NAV4_STBY_SET";

        public const string NAV1_STBY_SET_HZ = "NAV1_STBY_SET_HZ";

        public const string NAV2_STBY_SET_HZ = "NAV2_STBY_SET_HZ";

        public const string NAV3_STBY_SET_HZ = "NAV3_STBY_SET_HZ";

        public const string NAV4_STBY_SET_HZ = "NAV4_STBY_SET_HZ";

        public const string NAV1_VOLUME_DEC = "NAV1_VOLUME_DEC";

        public const string NAV2_VOLUME_DEC = "NAV2_VOLUME_DEC";

        public const string NAV3_VOLUME_DEC = "NAV3_VOLUME_DEC";

        public const string NAV4_VOLUME_DEC = "NAV4_VOLUME_DEC";

        public const string NAV1_VOLUME_INC = "NAV1_VOLUME_INC";

        public const string NAV2_VOLUME_INC = "NAV2_VOLUME_INC";

        public const string NAV3_VOLUME_INC = "NAV3_VOLUME_INC";

        public const string NAV4_VOLUME_INC = "NAV4_VOLUME_INC";

        [Deprecated]
        public const string NAV1_VOLUME_SET = "NAV1_VOLUME_SET";

        [Deprecated]
        public const string NAV2_VOLUME_SET = "NAV2_VOLUME_SET";

        [Deprecated]
        public const string NAV3_VOLUME_SET = "NAV3_VOLUME_SET";

        [Deprecated]
        public const string NAV4_VOLUME_SET = "NAV4_VOLUME_SET";

        public const string NAV1_VOLUME_SET_EX1 = "NAV1_VOLUME_SET_EX1";

        public const string NAV2_VOLUME_SET_EX1 = "NAV2_VOLUME_SET_EX1";

        public const string NAV3_VOLUME_SET_EX1 = "NAV3_VOLUME_SET_EX1";

        public const string NAV4_VOLUME_SET_EX1 = "NAV4_VOLUME_SET_EX1";

        public const string RADIO_NAV1_AUTOSWITCH_TOGGLE = "RADIO_NAV1_AUTOSWITCH_TOGGLE";

        public const string RADIO_NAV2_AUTOSWITCH_TOGGLE = "RADIO_NAV2_AUTOSWITCH_TOGGLE";

      }
      public static class Tacan
      {
        [Parameter(0, "Channel value (1 - 127)")]
        public const string TACAN1_ACTIVE_CHANNEL_SET = "TACAN1_ACTIVE_CHANNEL_SET";

        [Parameter(0, "Channel value (1 - 127)")]
        public const string TACAN2_ACTIVE_CHANNEL_SET = "TACAN2_ACTIVE_CHANNEL_SET";

        [Parameter(0, "Channel value (1 - 127)")]
        public const string TACAN1_STANDBY_CHANNEL_SET = "TACAN1_STANDBY_CHANNEL_SET";

        [Parameter(0, "Channel value (1 - 127)")]
        public const string TACAN2_STANDBY_CHANNEL_SET = "TACAN2_STANDBY_CHANNEL_SET";

        [Parameter(0, "Active mode value (0, 1)")]
        public const string TACAN1_ACTIVE_MODE_SET = "TACAN1_ACTIVE_MODE_SET";

        [Parameter(0, "Active mode value (0, 1)")]
        public const string TACAN2_ACTIVE_MODE_SET = "TACAN2_ACTIVE_MODE_SET";

        [Parameter(0, "Standby mode value (0, 1)")]
        public const string TACAN1_STANDBY_MODE_SET = "TACAN1_STANDBY_MODE_SET";

        [Parameter(0, "Standby mode value (0, 1)")]
        public const string TACAN2_STANDBY_MODE_SET = "TACAN2_STANDBY_MODE_SET";

        public const string TACAN1_SWAP = "TACAN1_SWAP";

        public const string TACAN2_SWAP = "TACAN2_SWAP";

        public const string TACAN1_VOLUME_INC = "TACAN1_VOLUME_INC";

        public const string TACAN2_VOLUME_INC = "TACAN2_VOLUME_INC";

        public const string TACAN1_VOLUME_DEC = "TACAN1_VOLUME_DEC";

        public const string TACAN2_VOLUME_DEC = "TACAN2_VOLUME_DEC";

        [Parameter(0, "Volume value (0, 100)")]
        public const string TACAN1_VOLUME_SET = "TACAN1_VOLUME_SET";

        [Parameter(0, "Volume value (0, 100)")]
        public const string TACAN2_VOLUME_SET = "TACAN2_VOLUME_SET";

        [Parameter(0, "Bearing indicator value")]
        public const string TACAN1_SET = "TACAN1_SET";

        [Parameter(0, "Bearing indicator value")]
        public const string TACAN2_SET = "TACAN2_SET";

        public const string TACAN1_OBI_DEC = "TACAN1_OBI_DEC";

        public const string TACAN2_OBI_DEC = "TACAN2_OBI_DEC";

        public const string TACAN1_OBI_INC = "TACAN1_OBI_INC";

        public const string TACAN2_OBI_INC = "TACAN2_OBI_INC";

        public const string TACAN1_OBI_FAST_DEC = "TACAN1_OBI_FAST_DEC";

        public const string TACAN2_OBI_FAST_DEC = "TACAN2_OBI_FAST_DEC";

        public const string TACAN1_OBI_FAST_INC = "TACAN1_OBI_FAST_INC";

        public const string TACAN2_OBI_FAST_INC = "TACAN2_OBI_FAST_INC";

        public const string TOGGLE_TACAN_DRIVES_NAV1 = "TOGGLE_TACAN_DRIVES_NAV1";

      }
      public static class Vor
      {
        public const string RADIO_VOR1_IDENT_DISABLE = "RADIO_VOR1_IDENT_DISABLE";

        public const string RADIO_VOR2_IDENT_DISABLE = "RADIO_VOR2_IDENT_DISABLE";

        public const string RADIO_VOR3_IDENT_DISABLE = "RADIO_VOR3_IDENT_DISABLE";

        public const string RADIO_VOR4_IDENT_DISABLE = "RADIO_VOR4_IDENT_DISABLE";

        public const string RADIO_VOR1_IDENT_ENABLE = "RADIO_VOR1_IDENT_ENABLE";

        public const string RADIO_VOR2_IDENT_ENABLE = "RADIO_VOR2_IDENT_ENABLE";

        public const string RADIO_VOR3_IDENT_ENABLE = "RADIO_VOR3_IDENT_ENABLE";

        public const string RADIO_VOR4_IDENT_ENABLE = "RADIO_VOR4_IDENT_ENABLE";

        [Parameter(0, "Bool")]
        public const string RADIO_VOR1_IDENT_SET = "RADIO_VOR1_IDENT_SET";

        [Parameter(0, "Bool")]
        public const string RADIO_VOR2_IDENT_SET = "RADIO_VOR2_IDENT_SET";

        [Parameter(0, "Bool")]
        public const string RADIO_VOR3_IDENT_SET = "RADIO_VOR3_IDENT_SET";

        [Parameter(0, "Bool")]
        public const string RADIO_VOR4_IDENT_SET = "RADIO_VOR4_IDENT_SET";

        public const string RADIO_VOR1_IDENT_TOGGLE = "RADIO_VOR1_IDENT_TOGGLE";

        public const string RADIO_VOR2_IDENT_TOGGLE = "RADIO_VOR2_IDENT_TOGGLE";

        public const string RADIO_VOR3_IDENT_TOGGLE = "RADIO_VOR3_IDENT_TOGGLE";

        public const string RADIO_VOR4_IDENT_TOGGLE = "RADIO_VOR4_IDENT_TOGGLE";

        public const string VOR_OBS = "VOR_OBS";

        public const string VOR1_OBI_DEC = "VOR1_OBI_DEC";

        public const string VOR2_OBI_DEC = "VOR2_OBI_DEC";

        public const string VOR3_OBI_DEC = "VOR3_OBI_DEC";

        public const string VOR4_OBI_DEC = "VOR4_OBI_DEC";

        public const string VOR1_OBI_FAST_DEC = "VOR1_OBI_FAST_DEC";

        public const string VOR2_OBI_FAST_DEC = "VOR2_OBI_FAST_DEC";

        public const string VOR3_OBI_FAST_DEC = "VOR3_OBI_FAST_DEC";

        public const string VOR4_OBI_FAST_DEC = "VOR4_OBI_FAST_DEC";

        public const string VOR1_OBI_FAST_INC = "VOR1_OBI_FAST_INC";

        public const string VOR2_OBI_FAST_INC = "VOR2_OBI_FAST_INC";

        public const string VOR3_OBI_FAST_INC = "VOR3_OBI_FAST_INC";

        public const string VOR4_OBI_FAST_INC = "VOR4_OBI_FAST_INC";

        public const string VOR1_OBI_INC = "VOR1_OBI_INC";

        public const string VOR2_OBI_INC = "VOR2_OBI_INC";

        public const string VOR3_OBI_INC = "VOR3_OBI_INC";

        public const string VOR4_OBI_INC = "VOR4_OBI_INC";

        [Parameter(0, "Value (0 - 360)")]
        public const string VOR1_SET = "VOR1_SET";

        [Parameter(0, "Value (0 - 360)")]
        public const string VOR2_SET = "VOR2_SET";

        [Parameter(0, "Value (0 - 360)")]
        public const string VOR3_SET = "VOR3_SET";

        [Parameter(0, "Value (0 - 360)")]
        public const string VOR4_SET = "VOR4_SET";

      }
      public static class Transponder
      {
        public const string XPNDR = "XPNDR";

        public const string XPNDR_1000_DEC = "XPNDR_1000_DEC";

        public const string XPNDR_100_DEC = "XPNDR_100_DEC";

        public const string XPNDR_10_DEC = "XPNDR_10_DEC";

        public const string XPNDR_1_DEC = "XPNDR_1_DEC";

        public const string XPNDR_1000_INC = "XPNDR_1000_INC";

        public const string XPNDR_100_INC = "XPNDR_100_INC";

        public const string XPNDR_10_INC = "XPNDR_10_INC";

        public const string XPNDR_1_INC = "XPNDR_1_INC";

        public const string XPNDR_DEC_CARRY = "XPNDR_DEC_CARRY";

        public const string XPNDR_INC_CARRY = "XPNDR_INC_CARRY";

        public const string XPNDR_IDENT_OFF = "XPNDR_IDENT_OFF";

        public const string XPNDR_IDENT_ON = "XPNDR_IDENT_ON";

        [Parameter(0, "Bool")]
        public const string XPNDR_IDENT_SET = "XPNDR_IDENT_SET";

        public const string XPNDR_IDENT_TOGGLE = "XPNDR_IDENT_TOGGLE";

        [Parameter(0, "Frequency value (BCD16 encoded)")]
        public const string XPNDR_SET = "XPNDR_SET";

      }
    }


  }
}

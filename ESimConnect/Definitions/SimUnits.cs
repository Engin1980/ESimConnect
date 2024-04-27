using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Definitions
{
    public static class SimUnits
    {
        public static class Length
        {
            public const string METER = "meter";
            public const string METER_SCALER_256 = "meter scaler 256";
            public const string MILLIMETER = "millimeter";
            public const string CENTIMETER = "centimeter";
            public const string KILOMETER = "kilometer";
            public const string NAUTICAL_MILE = "nautical mile";
            public const string DECINMILE = "decinmile";
            public const string INCH = "inch";
            public const string FOOT = "foot";
            public const string YARD = "yard";
            public const string DECIMILE = "decimile";
            public const string MILE = "mile";
        }

        public static class Volume
        {
            public const string CUBIC_INCH = "cubic inch";
            public const string CUBIC_FOOT = "cubic foot";
            public const string CUBIC_YARD = "cubic yard";
            public const string CUBIC_MILE = "cubic mile";
            public const string CUBIC_MILLIMETER = "cubic millimeter";
            public const string CUBIC_CENTIMETER = "cubic centimeter";
            public const string CUBIC_METER = "cubic meter";
            public const string CUBIC_KILOMETER = "cubic kilometer";
            public const string LITER = "liter";
            public const string GALLON = "gallon";
            public const string QUART = "quart";
            public const string FS7_OIL_QUANTITY = "fs7 oil quantity";
        }

        public static class Area
        {
            public const string SQUARE_INCH = "square inch";
            public const string SQUARE_FEET = "square feet";
            public const string SQUARE_YARD = "square yard";
            public const string SQUARE_MILE = "square mile";
            public const string SQUARE_MILLIMETER = "square millimeter";
            public const string SQUARE_CENTIMETER = "square centimeter";
            public const string SQUARE_METER = "square meter";
            public const string SQUARE_KILOMETER = "square kilometer";
        }

        public static class Temperature
        {
            public const string KELVIN = "kelvin";
            public const string RANKINE = "rankine";
            public const string FARENHEIT = "farenheit";
            public const string CELSIUS = "celsius";
            public const string CELSIUS_FS7_EGT = "celsius fs7 egt";
            public const string CELSIUS_FS7_OIL_TEMP = "celsius fs7 oil temp";
            public const string CELSIUS_SCALER_1BY256 = "celsius scaler 1/256";
            public const string CELSIUS_SCALER_256 = "celsius scaler 256";
            public const string CELSIUS_SCALER_16K = "celsius scaler 16k";
        }

        public static class Angle
        {
            public const string RADIAN = "radian";
            public const string ROUND = "round";
            public const string DEGREE = "degree";
            public const string GRAD = "grad";
            public const string ANGL16 = "angl16";
            public const string ANGL32 = "angl32";
        }

        public static class GlobalPosition
        {
            public const string DEGREE_LATITUDE = "degree latitude";
            public const string DEGREE_LONGITUDE = "degree longitude";
            public const string METER_LATITUDE = "meter latitude";
        }

        public static class AngularVelocity
        {
            public const string RADIAN_PER_SECOND = "radian per second";
            public const string REVOLUTION_PER_MINUTE = "revolution per minute";
            public const string RPM_1_OVER_16K = "rpm 1 over 16k";
            public const string MINUTE_PER_ROUND = "minute per round";
            public const string NICE_MINUTE_PER_ROUND = "nice minute per round";
            public const string DEGREE_PER_SECOND = "degree per second";
            public const string DEGREE_PER_SECOND_ANG16 = "degree per second ang16";
            public const string DEGREES_PER_SECOND_ANG16 = "degrees per second ang16";
        }

        public static class AngularAcceleration
        {
            public const string RADIAN_PER_SECOND_SQUARED = "radian per second squared";
            public const string DEGREE_PER_SECOND_SQUARED = "degree per second squared";
        }

        public static class Speed
        {
            public const string METER_PER_SECOND = "meter per second";
            public const string METER_PER_SECOND_SCALER_256 = "meter per second scaler 256";
            public const string METER_PER_MINUTE = "meter per minute";
            public const string KILOMETERBYHOUR = "kilometer/hour";
            public const string FEETBYSECOND = "feet/second";
            public const string FEETBYMINUTE = "feet/minute";
            public const string MILE_PER_HOUR = "mile per hour";
            public const string KNOT = "knot";
            public const string KNOT_SCALER_128 = "knot scaler 128";
            public const string MACH = "mach";
            public const string MACH_3D2_OVER_64K = "mach 3d2 over 64k";
        }

        public static class Acceleration
        {
            public const string METER_PER_SECOND_SQUARED = "meter per second squared";
            public const string FEET_PER_SECOND_SQUARED = "feet per second squared";
            public const string GFORCE = "Gforce";
            public const string G_FORCE_624_SCALED = "G Force 624 scaled";
        }

        public static class Time
        {
            public const string SECOND = "second";
            public const string MINUTE = "minute";
            public const string HOUR = "hour";
            public const string DAY = "day";
            public const string HOUR_OVER_10 = "hour over 10";
            public const string YEAR = "year";
        }
        public static class Weight
        {
            public const string KILOGRAM = "kilogram";
            public const string POUND = "pound";
            public const string POUND_SCALER_256 = "pound scaler 256";
            public const string SLUG = "slug";
        }

        public static class Power
        {
            public const string WATT = "Watt";
            public const string FT_LB_PER_SECOND = "ft lb per second";
        }

        public static class VolumeRate
        {
            public const string METER_CUBED_PER_SECOND = "meter cubed per second";
            public const string LITER_PER_HOUR = "liter per hour";
            public const string GALLON_PER_HOUR = "gallon per hour";
        }

        public static class WeightRate
        {
            public const string KILOGRAM_PER_SECOND = "kilogram per second";
            public const string POUND_PER_HOUR = "pound per hour";
        }
        public static class ElectricalCurrent
        {
            public const string AMPERE = "ampere";
            public const string FS7_CHARGING_AMPS = "fs7 charging amps";
        }
        public static class ElectricalPotential
        {
            public const string VOLT = "volt";
        }
        public static class Frequency
        {
            public const string HERTZ = "Hertz";
            public const string KILOHERTZ = "Kilohertz";
            public const string MEGAHERTZ = "Megahertz";
            public const string FREQUENCY_BCD16 = "Frequency BCD16";
            public const string FREQUENCY_BCD32 = "Frequency BCD32";
            public const string FREQUENCY_ADF_BCD32 = "Frequency ADF BCD32";
        }
        public static class Density
        {
            public const string KILOGRAM_PER_CUBIC_METER = "kilogram per cubic meter";
            public const string SLUG_PER_CUBIC_FEET = "Slug per cubic feet";
            public const string POUND_PER_GALLON = "pound per gallon";
        }

        public static class Pressure
        {
            public const string PASCAL = "pascal";
            public const string KILOPASCAL = "kilopascal";
            public const string MILLIMETER_OF_MERCURY = "millimeter of mercury";
            public const string CENTIMETER_OF_MERCURY = "centimeter of mercury";
            public const string INCH_OF_MERCURY = "inch of mercury";
            public const string INHG_64_OVER_64K = "inHg 64 over 64k";
            public const string MILLIMETER_OF_WATER = "millimeter of water";
            public const string NEWTON_PER_SQUARE_METER = "Newton per square meter";
            public const string KILOGRAM_FORCE_PER_SQUARE_CENTIMETER = "kilogram force per square centimeter";
            public const string KILOGRAM_METER_SQUARED = "kilogram meter squared";
            public const string ATMOSPHERE = "atmosphere";
            public const string BAR = "bar";
            public const string MILLIBAR = "millibar";
            public const string MILLIBAR_SCALER_16 = "millibar scaler 16";
            public const string POUND_FORCE_PER_SQUARE_INCH = "pound-force per square inch";
            public const string PSI_SCALER_16K = "psi scaler 16k";
            public const string PSI_4_OVER_16K = "psi 4 over 16k";
            public const string PSI_FS7_OIL_PRESSURE = "psi fs7 oil pressure";
            public const string POUND_FORCE_PER_SQUARE_FOOT = "pound-force per square foot";
            public const string PSF_SCALER_16K = "psf scaler 16k";
            public const string SLUG_FEET_SQUARED = "slug feet squared";
            public const string BOOST_CMHG = "boost cmHg";
            public const string BOOST_INHG = "boost inHg";
            public const string BOOST_PSI = "boost psi";
        }

        public static class Torque
        {
            public const string NEWTON_METER = "Newton meter";
            public const string FOOT_POUND = "foot-pound";
            public const string LBF_FEET = "lbf-feet";
            public const string KILOGRAM_METER = "kilogram meter";
            public const string POUNDAL_FEET = "poundal feet";
        }

        public static class Miscelaneous
        {
            public const string FRACTIONALLATLONDIGITS = "FractionalLatLonDigits";
            public const string PART = "part";
            public const string HALF = "half";
            public const string THIRD = "third";
            public const string PERCENT = "percent";
            public const string PERCENT_OVER_100 = "percent over 100";
            public const string PERCENT_SCALER_16K = "percent scaler 16k";
            public const string PERCENT_SCALER_32K = "percent scaler 32k";
            public const string PERCENT_SCALER_2POW23 = "percent scaler 2pow23";
            public const string BEL = "bel";
            public const string DECIBEL = "decibel";
            public const string MORE_THAN_A_HALF = "more_than_a_half";
            public const string TIMES = "times";
            public const string RATIO = "ratio";
            public const string NUMBER = "number";
            public const string SCALER = "scaler";
            public const string POSITION = "position";
            public const string ENUM = "Enum";
            public const string BOOL = "Bool";
            public const string BCO16 = "Bco16";
            public const string MASK = "mask";
            public const string FLAGS = "flags";
        }
    }
}

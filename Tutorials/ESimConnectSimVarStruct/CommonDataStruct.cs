using ESimConnect.Definitions;
using ESimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnectSimVarStruct
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
  public struct CommonDataStruct
  {
    [DataDefinition(SimVars.Aircraft.RadioAndNavigation.ATC_ID)]
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string callsign;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.PLANE_ALTITUDE, SimUnits.Length.FOOT)]
    public int altitude;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.PLANE_BANK_DEGREES, SimUnits.Angle.DEGREE)]
    public double bankAngle;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.PLANE_ALT_ABOVE_GROUND, SimUnits.Length.FOOT)]
    public int height;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.AIRSPEED_INDICATED, SimUnits.Speed.KNOT)]
    public int indicatedSpeed;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.GROUND_VELOCITY, SimUnits.Speed.KNOT)]
    public int groundSpeed;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.VERTICAL_SPEED, SimUnits.Length.FOOT)]
    public int verticalSpeed;

    [DataDefinition(SimVars.Aircraft.Miscelaneous.ACCELERATION_BODY_Z, SimUnits.Acceleration.FEET_PER_SECOND_SQUARED)]
    public double accelerationBodyZ;

    public override string ToString()
    {
      return $"Callsign: {callsign}, Altitude: {altitude} ft, Bank Angle: {bankAngle}°, Height: {height} ft, " +
             $"Indicated Speed: {indicatedSpeed} knots, Ground Speed: {groundSpeed} knots, " +
             $"Vertical Speed: {verticalSpeed} ft/min, Acceleration Body Z: {accelerationBodyZ} ft/s²";
    }
  }
}

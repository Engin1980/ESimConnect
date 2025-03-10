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
  internal struct RareDataStruct
  {
    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "1")]
    public int engineOneCombustion;
    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "2")]
    public int engineTwoCombustion;
    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "3")]
    public int engineThreeCombustion;
    [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "4")]
    public int engineFourCombustion;
    [DataDefinition(SimVars.Aircraft.BrakesAndLandingGear.BRAKE_PARKING_POSITION)]
    public int parkingBrake;
    [DataDefinition(SimVars.Services.PUSHBACK_ATTACHED)]
    public int pushbackTugConnected;
  }
}

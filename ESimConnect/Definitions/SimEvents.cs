using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Enumerations
{
    public static partial class SimEvents
    {
        public static class System
        {
            public const string _1sec = "1sec";
            public const string _4sec = "4sec";
            public const string _6Hz = "6Hz";
            public const string AircraftLoaded = "AircraftLoaded";
            public const string Crashed = "Crashed";
            public const string CrashReset = "CrashReset";
            public const string CustomMissionActionExecuted = "CustomMissionActionExecuted";
            public const string FlightLoaded = "FlightLoaded";
            public const string FlightSaved = "FlightSaved";
            public const string FlightPlanActivated = "FlightPlanActivated";
            public const string FlightPlanDeactivated = "FlightPlanDeactivated";
            public const string Frame = "Frame";
            public const string ObjectAdded = "ObjectAdded";
            public const string ObjectRemoved = "ObjectRemoved";
            public const string Pause = "Pause";
            public const string Pause_EX1 = "Pause_EX1";
            public const string Paused = "Paused";
            public const string PauseFrame = "PauseFrame";
            public const string PositionChanged = "PositionChanged";
            public const string Sim = "Sim";
            public const string SimStart = "SimStart";
            public const string SimStop = "SimStop";
            public const string Sound = "Sound";
            public const string Unpaused = "Unpaused";
            public const string View = "View";
            public const string WeatherModeChanged = "WeatherModeChanged";
        }
    }
}

using ESimConnect;
using ESimConnect.Definitions;
using ESystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest.Tests
{
    internal class StructsTest
    {
        private static readonly Dictionary<int, Type> requests = new();
        private static readonly ESimConnect.ESimConnect eSimCon = new();

        #region Types
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct TypeA
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
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct TypeB
        {
            [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "1")]
            public int engineOneCombustion;
            [DataDefinition(SimVars.Aircraft.Engine.ENG_COMBUSTION__index + "2")]
            public int engineTwoCombustion;
            [DataDefinition(SimVars.Aircraft.BrakesAndLandingGear.BRAKE_PARKING_POSITION)]
            public int parkingBrake;
            [DataDefinition(SimVars.Services.PUSHBACK_ATTACHED)]
            public int pushbackTugConnected;
            [DataDefinition(SimVars.Aircraft.Miscelaneous.VERTICAL_SPEED, SimUnits.Length.FOOT)]
            public int verticalSpeed;
            [DataDefinition(SimVars.Aircraft.Miscelaneous.ACCELERATION_BODY_Z, SimUnits.Acceleration.FEET_PER_SECOND_SQUARED)]
            public double accelerationBodyZ;
        }
        #endregion

        public static void Run()
        {
            Console.WriteLine("Starting non-WPF");
            Open(eSimCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);
            Sleep(500);
            RegisterA();
            RequestOnceA();
            Sleep(3000);
            RequestRepeatelyA();
            Sleep(3000);
            RegisterB();
            RequestRepeatelyB();
            Sleep(10000);
            DeleteA();
            Sleep(10000);
            Close(eSimCon);
            Sleep(1000);
            Console.WriteLine("Done");
        }

        private static void DeleteA()
        {
            Console.WriteLine("Delete A");
            eSimCon.Structs.Unregister<TypeA>();
        }

        private static void RequestRepeatelyB()
        {
            Console.WriteLine("Request B repeatedly");
            eSimCon.Structs.RequestRepeatedly<TypeB>(out int rid, SimConnectPeriod.SECOND, true);
            requests[rid] = typeof(TypeB);
        }

        private static void RegisterB()
        {
            Console.WriteLine("Register B");
            eSimCon.Structs.Register<TypeB>(false);
        }

        private static void RequestRepeatelyA()
        {
            Console.WriteLine("Request A repeatedly");
            eSimCon.Structs.RequestRepeatedly<TypeA>(out int rid, SimConnectPeriod.SECOND, true);
            requests[rid] = typeof(TypeA);
        }

        private static void RequestOnceA()
        {
            Console.WriteLine("Request A once");
            eSimCon.Structs.Request<TypeA>(out int rid);
            requests[rid] = typeof(TypeA);
        }

        private static void RegisterA()
        {
            Console.WriteLine("Register A");
            eSimCon.Structs.Register<TypeA>(false);
        }

        private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
        {
            Type? type = null;
            string? err = null;
            if (e.RequestId != null)
            {
                if (requests.TryGetValue(e.RequestId.Value, out type) == false)
                    err = "??-unrecognized-type-by-request-??";
            }
            else
                err = "??-non-request-id-simvar-??";
            if (err != null)
                Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, err={err}, data={e.Data}");
            else
            {
                Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, type={type!.Name}");
                var fields = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                foreach (var field in fields)
                {
                    object value = field.GetValue(e.Data) ?? throw new UnexpectedNullException();
                    Console.WriteLine($"\t{field.Name} = {value}");
                }
            }
        }
    }
}

﻿using ESimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest.Tests
{
    internal class ValuesTest
    {
        private static readonly ESimConnect.ESimConnect eSimCon = new();
        private static readonly string[] simVars = new string[]
        {
      "PLANE LATITUDE",
      "PLANE LONGITUDE",
      "PLANE ALTITUDE"
        };
        private static readonly Dictionary<string, int> simVarIds = new();
        private static readonly Dictionary<int, string> onceRequestId = new();
        private static readonly Dictionary<int, string> repeatedRequestId = new();

        public static void Run()
        {
            Console.WriteLine("Starting non-WPF");
            Open(eSimCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);
            Sleep(500);
            Register();
            RequestOnce();
            Sleep(3000);
            RequestRepeatedly();
            Sleep(3000);
            DeleteFirstRepeated();
            Sleep(3000);
            DeleteAllRepeated();
            Sleep(10000);
            Close(eSimCon);
            Sleep(1000);
            Console.WriteLine("Done");
        }

        private static void DeleteAllRepeated()
        {
            Console.WriteLine("Deleting all (except first&last) repeated");
            foreach (var simVar in simVars)
            {
                if (simVars[0] == simVar) continue;
                if (simVars.Last() == simVar) continue;
                Console.WriteLine("\t" + simVars[0]);
                int typeId = simVarIds[simVars[0]];
                eSimCon.Values.Unregister(typeId);
            }
        }

        private static void DeleteFirstRepeated()
        {
            Console.WriteLine("Deleting first repeated");
            Console.WriteLine("\t" + simVars[0]);
            int typeId = simVarIds[simVars[0]];
            eSimCon.Values.Unregister(typeId);
        }

        private static void RequestRepeatedly()
        {
            Console.WriteLine("Request repeatedly");
            foreach (var simVar in simVars)
            {
                Console.WriteLine("\t" + simVar);
                int typeId = simVarIds[simVar];
                eSimCon.Values.RequestRepeatedly(typeId, out int rid, SimConnectPeriod.SECOND, true);
                repeatedRequestId[rid] = simVar;
            }
        }

        private static void RequestOnce()
        {
            Console.WriteLine("Request once");
            foreach (var simVar in simVars)
            {
                Console.WriteLine("\t" + simVar);
                int typeId = simVarIds[simVar];
                eSimCon.Values.Request(typeId, out int rid);
                onceRequestId[rid] = simVar;
            }
        }

        private static void Register()
        {
            Console.WriteLine("Registering types");
            foreach (var simVar in simVars)
            {
                Console.WriteLine("\t" + simVar);
                simVarIds[simVar] = eSimCon.Values.Register<double>(simVar);
            }
        }

        private static void ESimCon_DataReceived(ESimConnect.ESimConnect sender, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
        {
            string relatedSimVar;
            if (e.RequestId != null)
            {
                if (onceRequestId.TryGetValue(e.RequestId.Value, out relatedSimVar!) == false)
                    if (repeatedRequestId.TryGetValue(e.RequestId.Value, out relatedSimVar!) == false)
                        relatedSimVar = "??-unrecognized-simvar-??";
            }
            else
                relatedSimVar = "??-non-request-id-simvar-??";
            Console.WriteLine($"ESimCon - DataReceived - requestId={e.RequestId}, simVar={relatedSimVar}, type={e.Type}, data={e.Data}");
        }
    }
}

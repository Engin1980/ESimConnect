using ESimConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static ESimConnect.ESimConnect;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest.Tests
{
    internal class SystemEventsTest
    {
        private static readonly ESimConnect.ESimConnect eSimCon = new();
        private static readonly string[] eventNames = new string[] { "Paused", "Unpaused" };
        private static readonly Dictionary<EventId, string> registeredEvents = new();
        private static readonly object lck = new();

        internal static void Run()
        {
            Console.WriteLine("Starting non-WPF");
            Open(eSimCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);
            Sleep(500);
            RegisterEvent();
            Sleep(1000);
            for (int i = 0; i < 5; i++)
            {
                AskForEvent();
                Sleep(1000);
            }
            Close(eSimCon);
            Sleep(1000);
            Console.WriteLine("Done");
        }

        private static void RegisterEvent()
        {
            Console.WriteLine("Registering events");
            foreach (string eventName in eventNames)
            {
                Console.WriteLine("\t" + eventName);
                EventId eid = eSimCon.SystemEvents.Register(eventName);
                registeredEvents[eid] = eventName;
            }
        }

        private static void AskForEvent()
        {
            Console.WriteLine("Waiting for any event invocation... (do something in the sim)...");
            lock (lck)
            {
                Monitor.Wait(lck);
            }
        }

        private static void ESimCon_EventInvoked(ESimConnect.ESimConnect _, ESimConnectSystemEventInvokedEventArgs e)
        {
            if (registeredEvents.TryGetValue(e.EventId, out string? registeredEventName) == false)
                registeredEventName = "??-unknown-event-requestId--??";

            Console.WriteLine($"ESimCon - Event invoked - event={e.Event}, eventId={e.EventId}, registered-event={registeredEventName}, value={e.Value}");
            lock (lck)
            {
                Monitor.Pulse(lck);
            }
        }
    }
}

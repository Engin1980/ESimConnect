using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ESimConnect.ESimConnect;
using static ESimConnectTest.SharedFunctions;

namespace ESimConnectTest.Tests
{
  internal class RegisterUnregisterTest
  {
    private static readonly string[] simVars =
    {
      "PLANE ALTITUDE", "PLANE LATITUDE", "PLANE LONGITUDE", "GROUND VELOCITY", "PLANE BANK DEGREES", "PLANE HEADING DEGREES TRUE",
      "VELOCITY BODY X", "VELOCITY BODY Y", "VELOCITY BODY Z", "VERTICAL SPEED",
    };
    private const int MIN_INTERVAL_MS = 50;
    private const int MAX_INTERVAL_MS = 300;
    private const int RUN_TIME_S = 60;
    private const int SAFETY_DELAY_MS = 50;

    private static readonly Random rnd = new();
    private static readonly List<string> unregistered = new();
    private static readonly List<RC> registered = new();
    private static readonly ESimConnect.ESimConnect simCon = new();
    private record RC(int typeId, string simVar);

    internal static void Run()
    {
      unregistered.AddRange(simVars);

      Open(simCon, ESimCon_Connected, ESimCon_EventInvoked, ESimCon_DataReceived, ESimCon_Disconnected, ESimCon_ThrowsException);

      Console.WriteLine("Starting");
      RegisterAll();
      DateTime endTime = DateTime.Now.AddSeconds(RUN_TIME_S);
      while (endTime > DateTime.Now)
      {
        Sleep(rnd.Next(MIN_INTERVAL_MS, MAX_INTERVAL_MS));
        MakeChange();
      }
      UnregisterAll();
      Sleep(1000);
      Console.WriteLine("Done");
    }

    private static void ESimCon_DataReceived(ESimConnect.ESimConnect _, ESimConnectDataReceivedEventArgs e)
    {
      Console.Write("*");
    }

    private static void MakeChange()
    {
      if (registered.Count < 2)
        RegisterRandom();
      else if (unregistered.Count < 2)
        UnregisterRandom();
      else if (rnd.NextDouble() < .5)
        RegisterRandom();
      else
        UnregisterRandom();
    }

    private static void UnregisterRandom()
    {
      int index = rnd.Next(registered.Count);
      RC rc = registered[index];
      Unregister(rc);
    }

    private static void RegisterRandom()
    {
      int index = rnd.Next(unregistered.Count);
      string simVar = unregistered[index];
      Register(simVar);
    }

    private static void UnregisterAll()
    {
      while (registered.Any())
        Unregister(registered.First());
    }

    private static void Unregister(RC rc)
    {
      Console.WriteLine("Unregistering " + rc.simVar);
      simCon.Values.UnregisterSafely(rc.typeId, SAFETY_DELAY_MS);
      registered.Remove(rc);
      unregistered.Add(rc.simVar);
    }

    private static void Register(string simVar)
    {
      Console.WriteLine("Registering " + simVar);
      int typeId = simCon.Values.Register<double>(simVar);
      Thread.Sleep(100);
      simCon.Values.RequestRepeatedly(typeId, out int rid, ESimConnect.SimConnectPeriod.SIM_FRAME);
      RC rc = new(typeId, simVar);

      unregistered.Remove(simVar);
      registered.Add(rc);
    }

    private static void RegisterAll()
    {
      while (unregistered.Any())
      {
        Register(unregistered.First());
      }
    }
  }
}

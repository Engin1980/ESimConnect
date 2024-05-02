using ESimConnect.Types;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Linq;
using System.Reflection;

namespace ESimConnect
{
    public partial class ESimConnect
  {
    public class ClientEventsHandler : BaseHandler
    {
      private readonly EEnum GROUP_ID_PRIORITY_STANDARD = (EEnum)1900000000;

      public ClientEventsHandler(ESimConnect parent) : base(parent)
      {
      }

      public void Invoke(string eventName, uint? parameter = null, bool validate = false) =>
        Invoke(eventName, parameter != null ? new uint[] { parameter.Value } : Array.Empty<uint>(), validate);

      public void Invoke(string eventName, int parameter, bool validate = false) =>
        Invoke(eventName, Convert.ToUInt32(parameter), validate);

      private void Invoke(string eventName, uint[]? parameters = null, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        parameters ??= Array.Empty<uint>();

        // up to 5 parameters available, but probably with a different .ddl version
        if (parameters.Length > 1) throw
            new NotImplementedException($"Maximum expected number of parameters is {1} (provided {parameters.Length}).");

        if (validate) Validate(eventName, parameters);

        EEnum eEvent = (EEnum)IdProvider.Next();
        this.parent.simConnect!.MapClientEventToSimEvent(eEvent, eventName);

        uint val = parameters.Length == 0 ? 0 : parameters[0];
        this.parent.Try(() =>
          this.parent.simConnect.TransmitClientEvent(
          SimConnect.SIMCONNECT_OBJECT_ID_USER, eEvent, val, GROUP_ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY),
          ex => new InternalException($"Failed to invoke 'TransmitClientEvent(...)'", ex));
      }

      private static void Validate(string eventName, uint[] parameters)
      {
        FieldInfo? extractEventField(string eventName, Type? cls = null)
        {
          FieldInfo? ret;
          if (cls == null) cls = typeof(SimClientEvents);

          ret = cls.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .FirstOrDefault(q => q.Name == eventName);

          if (ret == null)
          {
            var classes = cls.GetNestedTypes();
            foreach (var c in classes)
            {
              ret = extractEventField(eventName, c);
              if (ret != null) break;
            }
          }
          return ret;
        };

        FieldInfo? eventField = extractEventField(eventName) ?? throw new Exception($"Event '{eventName}' not found in declarations.");

        var paramAttrs = eventField.GetCustomAttributes().Where(q => q is SimClientEvents.Parameter).Cast<SimClientEvents.Parameter>();
        if (paramAttrs.Count() != parameters.Length)
        {
          throw new Exception($"Event '{eventName}' parameter check failed. Expected {paramAttrs.Count()} params, provided {parameters.Length}.");
        }
      }
    }
  }
}

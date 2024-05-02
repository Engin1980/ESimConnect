using ESimConnect.Enumerations;
using ESimConnect.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESimConnect
{
  public partial class ESimConnect
  {
    public class SystemEventsHandler : BaseHandler
    {
      private record EventIdName(EventId EventId, string EventName);
      private readonly List<EventIdName> registeredEvents = new();

      public SystemEventsHandler(ESimConnect parent) : base(parent)
      {
      }

      public EventId Register(string eventName, bool validate = false)
      {
        logger.LogMethodStart();
        parent.EnsureConnected();

        if (validate) ValidateSystemEventName(eventName);

        EventId? eventId = this.registeredEvents.FirstOrDefault(q => q.EventName == eventName)?.EventId;
        if (eventId == null)
        {
          eventId = EventId.Next();
          parent.Try(() =>
          {
            this.parent.simConnect!.SubscribeToSystemEvent(eventId.Value.ToEEnum(), eventName);
            this.registeredEvents.Add(new EventIdName(eventId.Value, eventName));
          },
          ex => new InternalException($"Failed to register sim-event listener for '{eventName}'.", ex));
        }
        logger.LogMethodEnd();

        return eventId.Value;
      }

      public void Unregister(EventId eventId)
      {
        var record = this.registeredEvents.FirstOrDefault(q => q.EventId == eventId) ?? throw new ESimConnectException($"EventId '{eventId}' not registered to any event.");
        parent.Try(() =>
        {
          this.parent.simConnect!.UnsubscribeFromSystemEvent(record.EventId.ToEEnum());
          this.registeredEvents.Remove(record);
        },
          ex => new InternalException($"Failed to unregister sim-event listener for event with id {record.EventId}/{record.EventName}.", ex));
      }

      private static void ValidateSystemEventName(string eventName)
      {
        bool findEvent(string simVarName, Type? cls = null)
        {
          bool ret;
          if (cls == null) cls = typeof(SimEvents);

          ret = cls.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(q => q.GetValue(null))
            .Any(q => Equals(q, simVarName));
          if (!ret)
          {
            var classes = cls.GetNestedTypes();
            foreach (var c in classes)
            {
              ret = findEvent(simVarName, c);
              if (ret) break;
            }
          }

          return ret;
        };

        bool exists = findEvent(eventName);
        if (!exists)
        {
          throw new ESimConnectException($"SystemEvent '{eventName}' check failed. SystemEvent name not found in known values.");
        }
      }

      public void UnregisterAll()
      {
        this.registeredEvents
          .ToList()
          .ForEach(q => this.Unregister(q.EventId));
      }

      internal string GetEventNameByEventId(EventId eventId)
      {
        string ret = this.registeredEvents.FirstOrDefault(q=>q.EventId == eventId)?.EventName 
          ?? throw new ESimConnectException($"System-EventId '{eventId}' not registered.");
        return ret;
      }
    }
  }
}

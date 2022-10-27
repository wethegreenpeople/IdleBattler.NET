using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Arena
{
    public class EventAction
    {
        public string Value { get; private set; }

        public EventAction(string value) { Value = value; }

        
        public static EventAction Movement => new(nameof(Movement));
        public static EventAction Fight => new(nameof(Fight));
        public static EventAction Loot => new(nameof(Loot));
        public static EventAction SpawnFighter => new(nameof(SpawnFighter));
        public static EventAction SpawnTreasure => new(nameof(SpawnTreasure));

        public static bool operator ==(EventAction obj1, EventAction obj2)
        {
            return String.Equals(obj1.Value, obj2.Value);
        }

        public static bool operator !=(EventAction obj1, EventAction obj2)
        {
            return !String.Equals(obj1.Value, obj2.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null) return false;

            return false;
        }
    }

    public class ArenaEvent
    {
        public EventAction EventAction { get; set; }
        public ArenaItemLocation EventLocation { get; set; }
        public Guid ObjectId { get; set; }

        public ArenaEvent()
        {

        }

        public ArenaEvent(EventAction eventAction, ArenaItemLocation eventLocation, Guid objectId)
        {
            EventAction = eventAction;
            EventLocation = eventLocation;
            ObjectId = objectId;
        }
    }
}

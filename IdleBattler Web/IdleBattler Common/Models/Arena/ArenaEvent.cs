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
        public static EventAction EventsNeedToContinue => new(nameof(EventsNeedToContinue));
        public static EventAction Death => new(nameof(Death));
        public static EventAction ArenaTimeUpdate => new(nameof(ArenaTimeUpdate));
        public static EventAction ArenaBattleComplete = new(nameof(ArenaBattleComplete));

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
            if (ReferenceEquals(this.Value, ((EventAction)obj).Value))
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
        public Object EventObject { get; set; }
        public Guid ObjectId { get; set; }

        public ArenaEvent()
        {

        }

        public ArenaEvent(EventAction eventAction, Object eventLocation, Guid objectId)
        {
            EventAction = eventAction;
            EventObject = eventLocation;
            ObjectId = objectId;
        }
    }
}

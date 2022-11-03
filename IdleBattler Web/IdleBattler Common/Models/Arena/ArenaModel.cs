using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Fighter;
using IdleBattler_Common.Shared;
using System.Text.Json.Serialization;

namespace IdleBattler_Common.Models.Arena
{
    public class ArenaModel
    {
        public Guid Id { get; private set; }
        public List<ArenaFighterModel> Fighters { get; private set; } = new List<ArenaFighterModel>();
        public List<TreasureModel> Treasures { get; private set; } = new List<TreasureModel>();
        public DateTime StartedTime { get; private set; }
        public bool HasStarted { get; private set; }
        public bool HasEnded { get; private set; }

        public ArenaModel(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        public ArenaModel(Guid id, List<ArenaFighterModel> fighters, List<TreasureModel> treasures, DateTime startedTime)
        {
            this.Id = id;
            this.Fighters = fighters;
            this.Treasures = treasures;
            this.StartedTime = startedTime;
        }

        public void SetStartTime(DateTime startTime)
        {
            this.StartedTime = startTime;
        }

        public void SetHasStarted(bool hasStarted)
        {
            this.HasStarted = hasStarted;
        }

        public void SetHasEnded(bool hasEnded)
        {
            this.HasEnded = hasEnded;
        }

        public void AddFighter(FighterModel fighter)
        {
            var fighterRand = new Random(this.Id.ToString().GetHashCode() + fighter.Id.ToString().GetHashCode());
            var arenaFighter = new ArenaFighterModel(fighter);
            arenaFighter.SetLocation(new ArenaItemLocation(fighterRand.Next(6, 95), fighterRand.Next(6, 95), VerticalMovementDirection.GetRandomDirection(), HorizontalMovementDirection.GetRandomDirection()));

            this.Fighters.Add(arenaFighter);
        }
    }
}

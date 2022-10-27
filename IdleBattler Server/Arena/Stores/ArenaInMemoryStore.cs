using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Common.Utils;
using IdleBattler_Server.Fighter.Stores;

namespace IdleBattler_Server.Arena.Stores
{
    public class ArenaInMemoryStore : IArenaStore
    {
        private ITreasureStore _treasureStore;
        private IMovementStore _movementStore;
        private IFighterStore _fighterStore;

        public ArenaInMemoryStore(ITreasureStore treasureStore, IMovementStore movementStore, IFighterStore fighterStore)
        {
            _treasureStore = treasureStore;
            _movementStore = movementStore;
            _fighterStore = fighterStore;
        }

        public async Task<ArenaModel> GetArena(Guid arenaId)
        {
            return await GetNewArena();
        }

        public async Task<ArenaModel> GetNewArena()
        {
            var arena = new ArenaModel(Guid.NewGuid());
            arena.Treasures.AddRange(await _treasureStore.Get(arena.Id));

            var fighter = await _fighterStore.Get(Guid.NewGuid());

            var rand = new Random(arena.Id.ToString().GetHashCode() + fighter.Id.ToString().GetHashCode());
            var arenaFighter = new ArenaFighterModel(fighter);
            arenaFighter.SetLocation(new ArenaItemLocation(rand.Next(6, 95), rand.Next(6, 95), VerticalMovementDirection.GetRandomDirection(), HorizontalMovementDirection.GetRandomDirection()));
            arena.Fighters.Add(arenaFighter);
            return arena;
        }

        public async Task<IEnumerable<ArenaEvent>> GetEvents(ArenaModel arena)
        {
            var events = new List<ArenaEvent>();

            foreach (var treasure in arena.Treasures)
            {
                events.Add(new ArenaEvent(EventAction.SpawnTreasure, treasure, treasure.Id));
            }

            foreach (var arenaFighter in arena.Fighters)
            {
                var location = new ArenaItemLocation(arenaFighter.XLocation, arenaFighter.YLocation);
                events.Add(new ArenaEvent(EventAction.SpawnFighter, location, arenaFighter.Fighter.Id));
            }

            while (events.Count < 100)
            {
                foreach (var arenaFighter in arena.Fighters)
                {
                    // Move fighter
                    var movement = await _movementStore.GetNextLocation(arena.Id, arenaFighter);
                    var arenaEvent = new ArenaEvent(EventAction.Movement, movement, arenaFighter.Fighter.Id);
                    events.Add(arenaEvent);
                    arenaFighter.SetLocation(movement);

                    // Check for loot
                    var treasureDistance = 5; // Default distance we can see treasure
                    var treasureInDistance = arena.Treasures.FirstOrDefault(s =>
                    {
                        var xDistance = Math.Abs(s.XLocation - arenaFighter.XLocation) <= treasureDistance;
                        var yDistance = Math.Abs(s.YLocation - arenaFighter.YLocation) <= treasureDistance;
                        return xDistance && yDistance;
                    }, null);
                    if (treasureInDistance != null)
                    {
                        events.Add(new ArenaEvent(EventAction.Loot, treasureInDistance, treasureInDistance.Id));
                        arena.Treasures.Remove(treasureInDistance);
                    }
                }
            }

            return events;
        }
    }
}

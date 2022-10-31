using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Models.Fighter;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Fighter.Stores;

namespace IdleBattler_Server.Arena.Stores
{
    public class ArenaInMemoryStore : IArenaStore
    {
        private readonly ITreasureStore _treasureStore;
        private readonly IMovementStore _movementStore;
        private readonly IFighterStore _fighterStore;
        private static readonly List<ArenaModel> _arenas = new();

        public ArenaInMemoryStore(ITreasureStore treasureStore, IMovementStore movementStore, IFighterStore fighterStore)
        {
            _treasureStore = treasureStore;
            _movementStore = movementStore;
            _fighterStore = fighterStore;
        }

        public async Task<ArenaModel> GetArena(Guid arenaId)
        {
            return _arenas.Single(s => s.Id == arenaId);
        }

        public async Task<ArenaModel> GetNewArena()
        {
            var arena = new ArenaModel(Guid.NewGuid());
            arena.SetStartTime(DateTime.Now);
            arena.Treasures.AddRange(await _treasureStore.Get(arena.Id, 2));

            var fighter = await _fighterStore.Get(Guid.NewGuid());
            fighter.SetInitialStats();
            var fighterRand = new Random(arena.Id.ToString().GetHashCode() + fighter.Id.ToString().GetHashCode());
            var arenaFighter = new ArenaFighterModel(fighter);
            arenaFighter.SetLocation(new ArenaItemLocation(fighterRand.Next(6, 95), fighterRand.Next(6, 95), VerticalMovementDirection.GetRandomDirection(), HorizontalMovementDirection.GetRandomDirection()));
            arena.Fighters.Add(arenaFighter);

            _arenas.Add(arena);
            return arena;
        }

        public async Task<IEnumerable<ArenaEvent>> GetEvents(ArenaModel arena, int amountOfEvents)
        {
            var events = new List<ArenaEvent>();

            foreach (var treasure in arena.Treasures.Where(s => !s.HasSpawned))
            {
                events.Add(new ArenaEvent(EventAction.SpawnTreasure, treasure, treasure.Id));
                treasure.SetHasSpawned(true);
            }

            foreach (var arenaFighter in arena.Fighters.Where(s => !s.HasSpawned))
            {
                events.Add(new ArenaEvent(EventAction.SpawnFighter, ArenaFighterModel.Copy(arenaFighter), arenaFighter.Fighter.Id));
                arenaFighter.SetHasSpawned(true);
            }

            while (events.Count < amountOfEvents)
            {
                if (hasFinishedCondition(arena)) break;

                foreach (var arenaFighter in arena.Fighters.Where(s => s.Fighter.Health > 0))
                {
                    if (!arenaFighter.InBattle)
                    {
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

                        // Initiate battle
                        var fighterInDistance = arena.Fighters.FirstOrDefault(s =>
                        {
                            var xDistance = Math.Abs(s.XLocation - arenaFighter.XLocation) <= arenaFighter.Fighter.VisionDistance;
                            var yDistance = Math.Abs(s.YLocation - arenaFighter.YLocation) <= arenaFighter.Fighter.VisionDistance;
                            return xDistance && yDistance && s.Fighter.Id != arenaFighter.Fighter.Id;
                        }, null);
                        if (fighterInDistance != null)
                        {
                            if (arenaFighter.Fighter.Health >= 0 && fighterInDistance.Fighter.Health >= 0)
                            {
                                fighterInDistance.SetInBattle(true, arenaFighter.Fighter.Id);
                                arenaFighter.SetInBattle(true, fighterInDistance.Fighter.Id);
                                fighterInDistance.Fighter.TakeDamage(1);
                                events.Add(new ArenaEvent(EventAction.Fight, ArenaFighterModel.Copy(arenaFighter), arenaFighter.Fighter.Id));
                            }
                        }

                        // Move fighter
                        // should be the last thing we do so client can draw all other events
                        var rand = new Random(arena.Id.ToString().GetHashCode() + arenaFighter.Fighter.Id.ToString().GetHashCode());
                        var movement = await _movementStore.GetNextLocation(arena.Id, arenaFighter, rand);
                        arenaFighter.SetLocation(movement);
                        var arenaEvent = new ArenaEvent(EventAction.Movement, ArenaFighterModel.Copy(arenaFighter), arenaFighter.Fighter.Id);
                        events.Add(arenaEvent);
                    }

                    // Continue battle
                    else if (arenaFighter.InBattle)
                    {
                        var fighterInBattleWith = arena.Fighters.Single(s => s.Fighter.Id == arenaFighter.InBattleWith);
                        fighterInBattleWith.Fighter.TakeDamage(1);
                        events.Add(new ArenaEvent(EventAction.Fight, ArenaFighterModel.Copy(arenaFighter), arenaFighter.Fighter.Id));

                        // Check for fighter death
                        if (fighterInBattleWith.Fighter.Health <= 0)
                        {
                            arenaFighter.SetInBattle(false, Guid.Empty);
                            arena.Fighters.ForEach(s =>
                            {
                                if (s.InBattleWith == fighterInBattleWith.Fighter.Id) s.SetInBattle(false, Guid.Empty);
                            });
                            events.Add(new ArenaEvent(EventAction.Death, ArenaFighterModel.Copy(fighterInBattleWith), fighterInBattleWith.Fighter.Id));
                        }
                    }
                }
            }

            var totalSecondsSinceCreated = DateTime.Now.Subtract(arena.StartedTime).TotalSeconds;
            events.Add(new ArenaEvent(EventAction.ArenaTimeUpdate, (totalSecondsSinceCreated / 25) * 100, Guid.Empty));

            if (!hasFinishedCondition(arena))
            {
                events.Add(new ArenaEvent(EventAction.EventsNeedToContinue, null, Guid.Empty));
            }

            return events;

            bool hasFinishedCondition(ArenaModel arena)
            {
                var noMoreTreasures = arena.Treasures.Count <= 0;
                var onlyOneFighter = arena.Fighters.Where(s => s.Fighter.Health > 0).Count() <= 1;

                var totalSecondsSinceCreated = DateTime.Now.Subtract(arena.StartedTime).TotalSeconds;
                var timeRanOut = totalSecondsSinceCreated >= 30;

                return noMoreTreasures || onlyOneFighter || timeRanOut;
            }
        }

        public Task<List<ArenaModel>> GetOpenArenas()
        {
            return Task.FromResult(_arenas.Where(s => s.Fighters.Count < 4).ToList());
        }
    }
}

using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Fighter.Stores;
using Quartz;

namespace IdleBattler_Server.Jobs
{
    [DisallowConcurrentExecution]
    public class AddFightersJob : IJob
    {
        private readonly IArenaStore _arenaStore;
        private readonly IFighterStore _fighterStore;

        public AddFightersJob(IArenaStore arenaStore, IFighterStore fighterStore)
        {
            _arenaStore = arenaStore;
            _fighterStore = fighterStore;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var arena in await _arenaStore.GetOpenArenas())
            {
                var fighter = await _fighterStore.CreateNewFighter();
                var arenaFighter = new ArenaFighterModel(fighter);
                var fighterRand = new Random(arena.Id.ToString().GetHashCode() + fighter.Id.ToString().GetHashCode());
                arenaFighter.SetLocation(new ArenaItemLocation(fighterRand.Next(6, 95), fighterRand.Next(6, 95), VerticalMovementDirection.GetRandomDirection(), HorizontalMovementDirection.GetRandomDirection()));
                arena.Fighters.Add(arenaFighter);
            }
        }
    }
}

using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Fighter.Stores;
using IdleBattler_Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace IdleBattler_Server.Jobs
{
    [DisallowConcurrentExecution]
    public class AddFightersJob : IJob
    {
        private readonly IArenaStore _arenaStore;
        private readonly IFighterStore _fighterStore;
        private readonly IHubContext<ArenaHub> _hubContext;

        public AddFightersJob(IArenaStore arenaStore, IFighterStore fighterStore, IHubContext<ArenaHub> hubContext)
        {
            _arenaStore = arenaStore;
            _fighterStore = fighterStore;
            _hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var arena in await _arenaStore.GetOpenArenas())
            {
                var fighter = await _fighterStore.CreateNewFighter();
                arena.AddFighter(fighter);

                await _hubContext.Clients.All.SendAsync(ArenaHubConstants.ArenaUpdate, arena.Id.ToString());

                if (arena.Fighters.Count == 4)
                {
                    await _arenaStore.SetArenaStarted(arena.Id);
                    arena.SetStartTime(DateTime.Now);
                    await _hubContext.Clients.All.SendAsync(ArenaHubConstants.ArenaStartBattle, arena.Id.ToString());
                }
            }
        }
    }
}

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
    public class AddArenasJob : IJob
    {
        private readonly IArenaStore _arenaStore;

        public AddArenasJob(IArenaStore arenaStore)
        {
            _arenaStore = arenaStore;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var openArenaCount = (await _arenaStore.GetOpenArenas()).Count;
            for (int i = openArenaCount; i < 3; ++i)
            {
                await _arenaStore.GetNewArena();
            }
        }
    }
}

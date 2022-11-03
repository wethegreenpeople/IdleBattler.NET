using IdleBattler_Server.Arena.Stores;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace IdleBattler_Server.Hubs
{
    public class ArenaHub : Hub
    {
        private readonly IArenaStore _arenaStore;

        public ArenaHub(IArenaStore arenaStore)
        {
            _arenaStore = arenaStore;
        }

        public async Task ArenaUpdate(Guid arenaId)
        {
            await Clients.All.SendAsync(ArenaHubConstants.ArenaUpdate, arenaId);
        }
    }

    public static class ArenaHubConstants
    {
        public const string ArenaUpdate = nameof(ArenaUpdate);
        public const string ArenaStartBattle = nameof(ArenaStartBattle);
        public const string EventsUpdate = nameof(EventsUpdate);
    }
}

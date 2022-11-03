using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IArenaStore
    {
        Task<ArenaModel> GetNewArena();
        Task<ArenaModel> GetArena(Guid arenaId);
        Task<IEnumerable<ArenaEvent>> GetEvents(ArenaModel arena, int amountOfEvents);
        Task<List<ArenaModel>> GetOpenArenas();
        Task<List<ArenaModel>> GetStartedArenas();
        Task SetArenaStarted(Guid arenaId);
        Task<ArenaModel> AddFighterToArena(Guid arenaId, Guid fighterId);
    }
}

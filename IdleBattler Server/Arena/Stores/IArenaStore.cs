using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IArenaStore
    {
        Task<ArenaModel> GetNewArena();
        Task<ArenaModel> GetArena(Guid arenaId);
        Task<IEnumerable<ArenaEvent>> GetEvents(ArenaModel arena, int amountOfEvents);
    }
}

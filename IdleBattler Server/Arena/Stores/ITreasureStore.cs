using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Stores
{
    public interface ITreasureStore
    {
        Task<List<TreasureModel>> Get(Guid arenaId);
    }
}

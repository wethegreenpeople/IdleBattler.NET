using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Services
{
    public interface ITreasureService
    {
        Task<List<TreasureModel>> GetTreasures(Guid arenaId);
    }
}

using IdleBattler_Server.Arena.Models;

namespace IdleBattler_Server.Arena.Services
{
    public interface IMovementService
    {
        Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId);
    }
}

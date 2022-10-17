using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Services
{
    public interface IMovementService
    {
        Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId);

        Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId, int initialX, int initialY);
    }
}

using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;

namespace IdleBattler_Server.Arena.Services
{
    public interface IMovementService
    {
        Task<ArenaItemLocation> GetNextMovement(Guid arenaId, ArenaFighterModel arenaFighter, Random rand);
    }
}

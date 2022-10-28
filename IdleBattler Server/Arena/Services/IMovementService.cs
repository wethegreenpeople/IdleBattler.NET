using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Fighter.Models;

namespace IdleBattler_Server.Arena.Services
{
    public interface IMovementService
    {
        Task<ArenaItemLocation> GetNextMovement(Guid arenaId, ArenaFighterModel arenaFighter);
    }
}

using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IMovementStore
    {
        Task<ArenaItemLocation> GetNextLocation(Guid arenaId, ArenaFighterModel fighter, Random rand);
    }
}

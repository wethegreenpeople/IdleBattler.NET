using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Fighter.Models;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IMovementStore
    {
        Task<MovementModel> Get(Guid arenaId, Guid fighterId);

        Task<MovementModel> Get(Guid arenaId, Guid fighterId, int initialX, int initialY);

        Task<ArenaItemLocation> GetNextLocation(Guid arenaId, ArenaFighterModel fighter);
    }
}

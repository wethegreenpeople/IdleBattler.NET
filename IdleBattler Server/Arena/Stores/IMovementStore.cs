using IdleBattler_Server.Arena.Models;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IMovementStore
    {
        Task<MovementModel> Get(Guid arenaId, Guid fighterId);
    }
}

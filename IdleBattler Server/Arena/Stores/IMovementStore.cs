using IdleBattler_Common.Models.Arena;

namespace IdleBattler_Server.Arena.Stores
{
    public interface IMovementStore
    {
        Task<MovementModel> Get(Guid arenaId, Guid fighterId);

        Task<MovementModel> Get(Guid arenaId, Guid fighterId, int initialX, int initialY);
    }
}

using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Services;

namespace IdleBattler_Server.Arena.Stores
{
    public class MovementStore : IMovementStore
    {
        private readonly IMovementService _movementService;

        public MovementStore(IMovementService movementService)
        {
            _movementService = movementService;
        }

        public async Task<MovementModel> Get(Guid arenaId, Guid fighterId)
        {
            var movements = await _movementService.GetMovements(arenaId, fighterId);
            return await Task.FromResult(new MovementModel(movements));
        }

        public async Task<MovementModel> Get(Guid arenaId, Guid fighterId, int initialX, int initialY)
        {
            var movements = await _movementService.GetMovements(arenaId, fighterId, initialX, initialY);
            return await Task.FromResult(new MovementModel(movements));
        }
    }
}

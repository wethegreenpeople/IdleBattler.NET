using IdleBattler_Server.Arena.Models;
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
    }
}

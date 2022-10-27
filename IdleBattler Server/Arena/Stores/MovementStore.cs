using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Fighter.Models;

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

        public async Task<ArenaItemLocation> GetNextLocation(Guid arenaId, ArenaFighterModel arenaFighter)
        {
            return await _movementService.GetNextMovement(arenaId, arenaFighter);
        }
    }
}

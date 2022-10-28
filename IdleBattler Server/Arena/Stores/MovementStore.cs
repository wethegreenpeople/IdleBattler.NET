﻿using IdleBattler_Common.Models.Arena;
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

        public async Task<ArenaItemLocation> GetNextLocation(Guid arenaId, ArenaFighterModel arenaFighter)
        {
            return await _movementService.GetNextMovement(arenaId, arenaFighter);
        }
    }
}

using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Common.Utils;
using IdleBattler_Server.Fighter.Models;
using IdleBattler_Server.Fighter.Stores;

namespace IdleBattler_Server.Arena.Services
{
    public class MovementService : IMovementService
    {
        public MovementService()
        {
        }

        public async Task<ArenaItemLocation> GetNextMovement(Guid arenaId, ArenaFighterModel arenaFighter)
        {
            // Switch direction if already at edge
            var verticalDirection = arenaFighter.VerticalMovementDirection;
            verticalDirection = arenaFighter.YLocation <= 5 && arenaFighter.VerticalMovementDirection == VerticalMovementDirection.Up
                ? VerticalMovementDirection.ReverseDirection(arenaFighter.VerticalMovementDirection)
                : verticalDirection;
            verticalDirection = arenaFighter.YLocation >= 95 && arenaFighter.VerticalMovementDirection == VerticalMovementDirection.Down
                ? VerticalMovementDirection.ReverseDirection(arenaFighter.VerticalMovementDirection)
                : verticalDirection;

            var horizontalDirection = arenaFighter.HorizontalMovementDirection;
            horizontalDirection = arenaFighter.XLocation <= 5 && arenaFighter.HorizontalMovementDirection == HorizontalMovementDirection.Left
                ? HorizontalMovementDirection.ReverseDirection(arenaFighter.HorizontalMovementDirection)
                : horizontalDirection;
            horizontalDirection = arenaFighter.XLocation >= 95 && arenaFighter.HorizontalMovementDirection == HorizontalMovementDirection.Right
                ? HorizontalMovementDirection.ReverseDirection(arenaFighter.HorizontalMovementDirection)
                : horizontalDirection;

            return MoveFighter(arenaFighter, verticalDirection, horizontalDirection);
        }

        private ArenaItemLocation MoveFighter(
            ArenaFighterModel fighterModel,
            VerticalMovementDirection verticalMovementDirection,
            HorizontalMovementDirection horizontalMovementDirection
            )
        {
            var newXLocation = horizontalMovementDirection == HorizontalMovementDirection.Right 
                ? fighterModel.XLocation + fighterModel.Fighter.MovementSpeed 
                : fighterModel.XLocation - fighterModel.Fighter.MovementSpeed;
            var newYLocation = verticalMovementDirection == VerticalMovementDirection.Down
                ? fighterModel.YLocation + fighterModel.Fighter.MovementSpeed
                : fighterModel.YLocation - fighterModel.Fighter.MovementSpeed;

            return new ArenaItemLocation(
                newXLocation >= 100 || newXLocation <= 0 ? fighterModel.XLocation : newXLocation,
                newYLocation >= 100 || newYLocation <= 0 ? fighterModel.YLocation : newYLocation,
                verticalMovementDirection,
                horizontalMovementDirection);
        }
    }
}

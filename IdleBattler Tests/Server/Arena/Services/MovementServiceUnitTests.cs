using FluentAssertions;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Fighter.Stores;
using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Tests.Server.Arena.Services
{
    public class MovementServiceUnitTests
    {
        [Fact(DisplayName = "Returns an ArenaLocation given an arenaId and ArenaFighterModel")]
        [Trait("Category", "GetNextMovement")]
        public async Task ReturnArenaLocation()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
            var movementService = new MovementService();

            // Act
            var move = await movementService.GetNextMovement(arenaId, fighter);

            // Assert
            Assert.NotNull(move);
            Assert.True(move.HorizontalMovementDirection == HorizontalMovementDirection.Stationary);
            Assert.True(move.VerticalMovementDirection == VerticalMovementDirection.Stationary);
            Assert.True(move.XLocation == 0);
            Assert.True(move.YLocation == 0);
        }

        [Fact(DisplayName = "Given the same objects, always return the same next location")]
        [Trait("Category", "GetNextMovement")]
        public async Task AlwaysReturnTheSameLocation()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
            fighter.SetLocation(new ArenaItemLocation(20, 25, VerticalMovementDirection.Up, HorizontalMovementDirection.Left));
            var movementService = new MovementService();

            // Act
            var move = await movementService.GetNextMovement(arenaId, fighter);

            // Assert
            for (int i = 0; i < 5; ++i)
            {
                move.Should().BeEquivalentTo(await movementService.GetNextMovement(arenaId, fighter));
            }
        }

        [Fact(DisplayName = "Next location should always continue from the fighter's current location")]
        [Trait("Category", "GetNextMovement")]
        public async Task EnsureNextMovementStartsFromGivenLocation()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighter = new FighterModel(Guid.NewGuid());
            fighter.SetInitialStats();
            var arenaFighter = new ArenaFighterModel(fighter);
            arenaFighter.SetLocation(new ArenaItemLocation(20, 25, VerticalMovementDirection.Up, HorizontalMovementDirection.Left));
            var movementService = new MovementService();

            // Act
            var move = await movementService.GetNextMovement(arenaId, arenaFighter);

            // Assert
            move.XLocation.Should().Be(19);
            move.YLocation.Should().Be(24);
        }

        [Fact(DisplayName = "Next fighter's location is around the edge, we should switch fighter direction")]
        [Trait("Category", "GetNextMovement")]
        public async Task ShouldSwitchDirections()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
            fighter.SetLocation(new ArenaItemLocation(4, 4, VerticalMovementDirection.Up, HorizontalMovementDirection.Left));
            var movementService = new MovementService();

            // Act
           var move = await movementService.GetNextMovement(arenaId, fighter);

            // Assert
            move.HorizontalMovementDirection.Should().Be(HorizontalMovementDirection.Right);
            move.VerticalMovementDirection.Should().Be(VerticalMovementDirection.Down);
        }
    }
}

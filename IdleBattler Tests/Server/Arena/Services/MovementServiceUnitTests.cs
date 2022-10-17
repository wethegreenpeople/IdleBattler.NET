using FluentAssertions;
using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Fighter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Tests.Server.Arena.Services
{
    public class MovementServiceUnitTests
    {
        [Fact]
        public async Task GetMovements_GivenAnArenaId_ReturnAListOfMoves()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighterId = Guid.NewGuid();
            var fighterStore = new InMemoryFighterStore();
            var movementService = new MovementService(fighterStore);

            // Act
            var moves = await movementService.GetMovements(arenaId, fighterId);

            // Assert
            Assert.NotEmpty(moves);
        }

        [Fact]
        public async Task GetMovements_GivenAnArenaId_AlwaysReturnTheSameListOfMoves()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighterId = Guid.NewGuid();
            var fighterStore = new InMemoryFighterStore();
            var movementService = new MovementService(fighterStore);

            // Act
            var moves = await movementService.GetMovements(arenaId, fighterId);

            // Assert
            for (int i = 0; i < 5; ++i)
            {
                moves.Should().BeEquivalentTo(await movementService.GetMovements(arenaId, fighterId));
            }
        }

        [Fact]
        public async Task GetMovements_GivenTwoDifferentArenaIds_ReturnTwoDifferentMoveLists()
        {
            // Arrange
            var arenaId = Guid.NewGuid();
            var fighterId = Guid.NewGuid();
            var anotherArenaId = Guid.NewGuid();
            var fighterStore = new InMemoryFighterStore();
            var movementService = new MovementService(fighterStore);

            // Act
            var moves = await movementService.GetMovements(arenaId, fighterId);
            var anotherMoves = await movementService.GetMovements(anotherArenaId, fighterId);

            // Assert
            moves.Should().NotBeEquivalentTo(anotherMoves);
        }
    }
}

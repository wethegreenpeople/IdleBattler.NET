using IdleBattler_Common.Enums.Arena;
using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Fighter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Tests.Common.Enums.Arena
{
    public class MovementDirectionUnitTests
    {
        [Fact]
        public async Task HorizontalMovementDirection_AreEqualWhenCompared()
        {
            // Arrange
            var directionOne = HorizontalMovementDirection.Left;
            var directionTwo = HorizontalMovementDirection.Left;

            // Act
            var areEqual = directionOne == directionTwo;

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public async Task VerticalMovementDirection_AreEqualWhenCompared()
        {
            // Arrange
            var directionOne = VerticalMovementDirection.Up;
            var directionTwo = VerticalMovementDirection.Up;

            // Act
            var areEqual = directionOne == directionTwo;

            // Assert
            Assert.True(areEqual);
        }
    }
}

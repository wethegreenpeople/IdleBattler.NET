using FluentAssertions;
using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Models.Fighter;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Arena.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Tests.Server.Arena.Stores
{
    public class ArenaStoreUnitTests
    {
        [Fact(DisplayName = "Should spawn the same amount of treasures in arena")]
        [Trait("Category", "GetEvents")]
        public async Task ShouldSpawnTreasures()
        {
            // Arrange
            var arenaStore = new ArenaInMemoryStore(null, null, null);
            var arena = new ArenaModel(Guid.NewGuid());
            arena.Treasures.AddRange(await new TreasureStore(new TreasureService()).Get(arena.Id, 2));

            // Act
            var events = await arenaStore.GetEvents(arena, 2);

            // Assert
            events.Should().HaveCount(2);
            events.Where(s => s.EventAction == EventAction.SpawnTreasure).Should().HaveCount(2);
        }

        [Fact(DisplayName = "Should spawn the same amount of fighters in arena")]
        [Trait("Category", "GetEvents")]
        public async Task ShouldSpawnFighters()
        {
            // Arrange
            var arenaStore = new ArenaInMemoryStore(null, null, null);
            var arena = new ArenaModel(Guid.NewGuid());
            for (int i = 0; i < 2; ++i)
            {
                var fighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
                arena.Fighters.Add(fighter);
            }

            // Act
            var events = await arenaStore.GetEvents(arena, 2);

            // Assert
            events.Should().HaveCount(2);
            events.Where(s => s.EventAction == EventAction.SpawnFighter).Should().HaveCount(2);
        }

        [Fact(DisplayName = "Do not create events for fighters with no health")]
        [Trait("Category", "GetEvents")]
        public async Task DoNotCreateDeadFighterEvents()
        {
            // Arrange
            var mockMovementStore = new Mock<IMovementStore>();
            mockMovementStore.Setup(s => s.GetNextLocation(It.IsAny<Guid>(), It.IsAny<ArenaFighterModel>())).Returns(Task.FromResult(new ArenaItemLocation(25, 25)));
            var arenaStore = new ArenaInMemoryStore(null, mockMovementStore.Object, null);
            var arena = new ArenaModel(Guid.NewGuid());

            var fighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
            fighter.Fighter.SetInitialStats();
            arena.Fighters.Add(fighter);

            var deadFighter = new ArenaFighterModel(new FighterModel(Guid.NewGuid()));
            deadFighter.Fighter.SetInitialStats();
            deadFighter.Fighter.SetHealth(0);
            arena.Fighters.Add(deadFighter);

            var totalEvents = 10;

            // Act
            var events = await arenaStore.GetEvents(arena, totalEvents);

            // Assert
            events.Where(s => s.EventAction == EventAction.SpawnFighter).Should().HaveCount(2);
            events
                .Where(s => s.ObjectId == deadFighter.Fighter.Id && (s.EventAction != EventAction.SpawnFighter && s.EventAction != EventAction.Death))
                .Should()
                .HaveCount(0);
        }

        [Fact(DisplayName = "Only create fight events once fighter is in battle")]
        [Trait("Category", "GetEvents")]
        public async Task OnlyFightEvents()
        {
            // Arrange
            var mockMovementStore = new Mock<IMovementStore>();
            mockMovementStore.Setup(s => s.GetNextLocation(It.IsAny<Guid>(), It.IsAny<ArenaFighterModel>())).Returns(Task.FromResult(new ArenaItemLocation(25, 25)));
            var arenaStore = new ArenaInMemoryStore(null, mockMovementStore.Object, null);
            var arena = new ArenaModel(Guid.NewGuid());

            var fighterGuid = Guid.NewGuid();
            var fightingFighterGuid = Guid.NewGuid();
            var fighter = new ArenaFighterModel(new FighterModel(fighterGuid));
            fighter.Fighter.SetInitialStats();
            fighter.SetInBattle(true, fightingFighterGuid);
            arena.Fighters.Add(fighter);

            var fightingFighter = new ArenaFighterModel(new FighterModel(fightingFighterGuid));
            fightingFighter.Fighter.SetInitialStats();
            fightingFighter.SetInBattle(true, fighterGuid);
            arena.Fighters.Add(fightingFighter);

            var totalEvents = 10;

            // Act
            var events = await arenaStore.GetEvents(arena, totalEvents);

            // Assert
            events
                .Where(s => s.EventAction == EventAction.Fight || s.EventAction == EventAction.Death)
                .Should()
                .HaveCount(totalEvents - 2);
        }

        [Fact(DisplayName = "Create loot event when close enough to the treasure")]
        [Trait("Category", "GetEvents")]
        public async Task CreateLootEvents()
        {
            // Arrange
            var mockMovementStore = new Mock<IMovementStore>();
            mockMovementStore.Setup(s => s.GetNextLocation(It.IsAny<Guid>(), It.IsAny<ArenaFighterModel>())).Returns(Task.FromResult(new ArenaItemLocation(25, 25)));
            var arenaStore = new ArenaInMemoryStore(null, mockMovementStore.Object, null);
            var arena = new ArenaModel(Guid.NewGuid());

            var fighterGuid = Guid.NewGuid();
            var fighter = new ArenaFighterModel(new FighterModel(fighterGuid));
            fighter.Fighter.SetInitialStats();
            fighter.SetLocation(new ArenaItemLocation(25, 25));
            arena.Fighters.Add(fighter);

            arena.Treasures.Add(new TreasureModel(Guid.NewGuid(), "Doot", 25, 25, VerticalMovementDirection.Stationary, HorizontalMovementDirection.Stationary));

            var totalEvents = 10;

            // Act
            var events = await arenaStore.GetEvents(arena, totalEvents);

            // Assert
            events
                .Where(s => s.EventAction == EventAction.Loot)
                .Should()
                .HaveCount(1);
        }
    }
}

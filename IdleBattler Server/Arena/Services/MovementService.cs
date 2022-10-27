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
        private readonly IFighterStore _fighterStore;

        public MovementService(IFighterStore fighterStore)
        {
            _fighterStore = fighterStore;
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

        public async Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId)
        {
            var rand = new Random(arenaId.ToString().GetHashCode() + fighterId.ToString().GetHashCode());
            var minRange = 6;
            var maxRange = 94;

            var fighterMovement = GetRandomInitialFighterMovement(rand, minRange, maxRange, fighterId);
            var fighter = await _fighterStore.Get(fighterId);

            var verticalMovementDirection = EnumUtils.GetRandomEnumValue<VerticalMovementDirection>(rand);
            var horizontalMovementDirection = EnumUtils.GetRandomEnumValue<HorizontalMovementDirection>(rand);

            for (int i = 0; i < 100; ++i)
            {
                // Switch direction if already at edge
                //if (fighterMovement.Locations.Last().XLocation >= 95 || fighterMovement.Locations.Last().XLocation <= 5) horizontalMovementDirection = (HorizontalMovementDirection)(1 & ~((int)horizontalMovementDirection));
                //if (fighterMovement.Locations.Last().YLocation >= 95 || fighterMovement.Locations.Last().YLocation <= 5) verticalMovementDirection = (VerticalMovementDirection)(1 & ~((int)verticalMovementDirection));
                
                //fighterMovement.Locations.Add(MoveFighter(fighter, fighterMovement.Locations.Last(), verticalMovementDirection, horizontalMovementDirection));
            }
            
            return new List<FighterMovementModel>
            {
                fighterMovement
            };
        }

        public async Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId, int initialX, int initialY)
        {
            var rand = new Random(arenaId.ToString().GetHashCode() + fighterId.ToString().GetHashCode());
            var fighterMovement = GetInitialFighterMovement(initialX, initialY, fighterId);
            var fighter = await _fighterStore.Get(fighterId);

            var verticalMovementDirection = EnumUtils.GetRandomEnumValue<VerticalMovementDirection>(rand);
            var horizontalMovementDirection = EnumUtils.GetRandomEnumValue<HorizontalMovementDirection>(rand);

            for (int i = 0; i < 100; ++i)
            {
                // Switch direction if already at edge
                //if (fighterMovement.Locations.Last().XLocation >= 95 || fighterMovement.Locations.Last().XLocation <= 5) horizontalMovementDirection = (HorizontalMovementDirection)(1 & ~((int)horizontalMovementDirection));
                //if (fighterMovement.Locations.Last().YLocation >= 95 || fighterMovement.Locations.Last().YLocation <= 5) verticalMovementDirection = (VerticalMovementDirection)(1 & ~((int)verticalMovementDirection));

                //fighterMovement.Locations.Add(MoveFighter(fighter, fighterMovement.Locations.Last(), verticalMovementDirection, horizontalMovementDirection));
            }

            return new List<FighterMovementModel>
            {
                fighterMovement
            };
        }

        private FighterMovementModel GetRandomInitialFighterMovement(Random rand, int minRange, int maxRange, Guid fighterId)
        {
            var initialLocation = new LocationModel()
            {
                XLocation = rand.Next(minRange, maxRange),
                YLocation = rand.Next(minRange, maxRange)
            };
            var fighterMovement = new FighterMovementModel(fighterId);
            fighterMovement.Locations.Add(initialLocation);
            return fighterMovement;
        }

        private FighterMovementModel GetInitialFighterMovement(int initialX, int initialY, Guid fighterId)
        {
            var initialLocation = new LocationModel()
            {
                XLocation = initialX,
                YLocation = initialY
            };
            var fighterMovement = new FighterMovementModel(fighterId);
            fighterMovement.Locations.Add(initialLocation);
            return fighterMovement;
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

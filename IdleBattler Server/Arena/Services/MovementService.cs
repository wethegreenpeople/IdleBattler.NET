using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Enums;
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

        public async Task<List<FighterMovementModel>> GetMovements(Guid arenaId, Guid fighterId)
        {
            var rand = new Random(arenaId.ToString().GetHashCode() + fighterId.ToString().GetHashCode());
            var minRange = 6;
            var maxRange = 94;

            var fighterMovement = GetRandomInitialFighterMovement(rand, minRange, maxRange, fighterId);
            var fighter = await _fighterStore.Get(fighterId);

            var verticalMovementDirection = GetRandomEnumValue<VerticalMovementDirection>(rand);
            var horizontalMovementDirection = GetRandomEnumValue<HorizontalMovementDirection>(rand);

            for (int i = 0; i < 100; ++i)
            {
                // Switch direction if already at edge
                if (fighterMovement.Locations.Last().XLocation >= 95 || fighterMovement.Locations.Last().XLocation <= 5) horizontalMovementDirection = (HorizontalMovementDirection)(1 & ~((int)horizontalMovementDirection));
                if (fighterMovement.Locations.Last().YLocation >= 95 || fighterMovement.Locations.Last().YLocation <= 5) verticalMovementDirection = (VerticalMovementDirection)(1 & ~((int)verticalMovementDirection));
                
                fighterMovement.Locations.Add(MoveFighter(fighter, fighterMovement.Locations.Last(), verticalMovementDirection, horizontalMovementDirection));
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

            var verticalMovementDirection = GetRandomEnumValue<VerticalMovementDirection>(rand);
            var horizontalMovementDirection = GetRandomEnumValue<HorizontalMovementDirection>(rand);

            for (int i = 0; i < 100; ++i)
            {
                // Switch direction if already at edge
                if (fighterMovement.Locations.Last().XLocation >= 95 || fighterMovement.Locations.Last().XLocation <= 5) horizontalMovementDirection = (HorizontalMovementDirection)(1 & ~((int)horizontalMovementDirection));
                if (fighterMovement.Locations.Last().YLocation >= 95 || fighterMovement.Locations.Last().YLocation <= 5) verticalMovementDirection = (VerticalMovementDirection)(1 & ~((int)verticalMovementDirection));

                fighterMovement.Locations.Add(MoveFighter(fighter, fighterMovement.Locations.Last(), verticalMovementDirection, horizontalMovementDirection));
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

        private LocationModel MoveFighter(
            FighterModel fighter, 
            LocationModel location, 
            VerticalMovementDirection verticalMovementDirection,
            HorizontalMovementDirection horizontalMovementDirection
            )
        {
            var newXLocation = horizontalMovementDirection == HorizontalMovementDirection.Right 
                ? location.XLocation + fighter.MovementSpeed 
                : location.XLocation - fighter.MovementSpeed;
            var newYLocation = verticalMovementDirection == VerticalMovementDirection.Down
                ? location.YLocation + fighter.MovementSpeed
                : location.YLocation - fighter.MovementSpeed;

            return new LocationModel()
            {
                XLocation = newXLocation >= 100 || newXLocation <= 0 ? location.XLocation : newXLocation,
                YLocation = newYLocation >= 100 || newYLocation <= 0 ? location.YLocation : newYLocation
            };
        }

        private T GetRandomEnumValue<T>(Random rand)
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rand.Next(values.Length));
        }
    }
}

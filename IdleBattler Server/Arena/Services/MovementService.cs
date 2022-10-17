using IdleBattler_Server.Arena.Models;
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
            var minRange = 1;
            var maxRange = 100;

            var fighterMovement = GetInitialFighterMovement(rand, minRange, maxRange, fighterId);
            var fighter = await _fighterStore.Get(fighterId);

            for (int i = 0; i < 5; ++i)
            {
                fighterMovement.Locations.Add(MoveFighter(fighter, fighterMovement.Locations.Last()));
            }
            
            return new List<FighterMovementModel>
            {
                fighterMovement
            };
        }

        private FighterMovementModel GetInitialFighterMovement(Random rand, int minRange, int maxRange, Guid fighterId)
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

        private LocationModel MoveFighter(FighterModel fighter, LocationModel location)
        {
            return new LocationModel()
            {
                XLocation = location.XLocation += fighter.MovementSpeed,
                YLocation = location.YLocation += fighter.MovementSpeed
            };
        }
    }
}

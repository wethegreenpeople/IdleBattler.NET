using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Arena;
using IdleBattler_Common.Shared;

namespace IdleBattler_Server.Arena.Services
{
    public class TreasureService : ITreasureService
    {
        public Task<List<TreasureModel>> GetTreasures(Guid arenaId, int amountOfTreasures)
        {
            var treasureList = new List<TreasureModel>();
            var rand = new Random(arenaId.ToString().GetHashCode());

            for (int i = 0; i < amountOfTreasures; ++i)
            {
                var treasureLocation = GetTreasureLocation(rand);
                treasureList.Add(new TreasureModel(Guid.NewGuid(), "Gun", treasureLocation.XLocation, treasureLocation.YLocation, treasureLocation.VerticalMovementDirection, treasureLocation.HorizontalMovementDirection));
            }

            return Task.FromResult(treasureList);
        }

        private ArenaItemLocation GetTreasureLocation(Random rand)
        {
            return new ArenaItemLocation(rand.Next(6, 94), rand.Next(6, 94), VerticalMovementDirection.Stationary, HorizontalMovementDirection.Stationary);
        }
    }
}

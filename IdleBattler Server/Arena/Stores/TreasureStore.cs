using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Services;

namespace IdleBattler_Server.Arena.Stores
{
    public class TreasureStore : ITreasureStore
    {
        private readonly ITreasureService _treasureService;

        public TreasureStore(ITreasureService treasureService)
        {
            _treasureService = treasureService;
        }

        public Task<List<TreasureModel>> Get(Guid arenaId, int amountOfTreasures)
        {
            return _treasureService.GetTreasures(arenaId, amountOfTreasures);
        }
    }
}

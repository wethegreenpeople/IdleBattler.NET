using IdleBattler_Server.Fighter.Models;

namespace IdleBattler_Server.Fighter.Stores
{
    public class InMemoryFighterStore : IFighterStore
    {
        public Task<FighterModel> Get(Guid fighterId)
        {
            return Task.FromResult(new FighterModel(fighterId));
        }
    }
}

using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Fighter.Stores
{
    public class FighterStore : IFighterStore
    {
        public Task<FighterModel> CreateNewFighter()
        {
            throw new NotImplementedException();
        }

        Task<FighterModel> IFighterStore.Get(Guid fighterId)
        {
            throw new NotImplementedException();
        }
    }
}

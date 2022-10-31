using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Fighter.Stores
{
    public class FighterStore : IFighterStore
    {
        Task<FighterModel> IFighterStore.Get(Guid fighterId)
        {
            throw new NotImplementedException();
        }
    }
}

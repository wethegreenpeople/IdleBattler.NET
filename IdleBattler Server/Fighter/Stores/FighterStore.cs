using IdleBattler_Server.Fighter.Models;

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

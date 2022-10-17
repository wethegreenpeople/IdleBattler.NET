using IdleBattler_Server.Fighter.Models;

namespace IdleBattler_Server.Fighter.Stores
{
    public interface IFighterStore
    {
        Task<FighterModel> Get(Guid fighterId);
    }
}

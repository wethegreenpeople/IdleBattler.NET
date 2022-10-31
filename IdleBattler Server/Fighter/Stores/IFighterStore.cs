using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Fighter.Stores
{
    public interface IFighterStore
    {
        Task<FighterModel> Get(Guid fighterId);
        Task<FighterModel> CreateNewFighter();
    }
}

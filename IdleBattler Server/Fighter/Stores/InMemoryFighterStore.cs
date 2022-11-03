using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Fighter.Stores
{
    public class InMemoryFighterStore : IFighterStore
    {
        private static readonly List<FighterModel> _fighters = new List<FighterModel>();

        public Task<FighterModel> CreateNewFighter()
        {
            var fighter = new FighterModel(Guid.NewGuid());
            fighter.SetInitialStats();
            _fighters.Add(fighter);
            return Task.FromResult(fighter);
        }

        public Task<FighterModel> Get(Guid fighterId)
        {
            return Task.FromResult(_fighters.FirstOrDefault(s => s.Id == fighterId));
        }
    }
}

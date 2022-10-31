﻿using IdleBattler_Common.Models.Fighter;

namespace IdleBattler_Server.Fighter.Stores
{
    public class InMemoryFighterStore : IFighterStore
    {
        public Task<FighterModel> Get(Guid fighterId)
        {
            var fighter = new FighterModel(fighterId);
            return Task.FromResult(fighter);
        }
    }
}

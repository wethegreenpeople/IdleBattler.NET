using IdleBattler_Common.Models.Fighter;
using IdleBattler_Server.Fighter.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdleBattler_Server.Fighter.Controllers
{
    [Route("api/[controller]")]
    public class FighterController : Controller
    {
        private readonly IFighterStore _fighterStore;

        public FighterController(IFighterStore fighterStore)
        {
            _fighterStore = fighterStore;
        }

        [HttpGet("{fighterId}")]
        public async Task<FighterModel> GetFighter(Guid fighterId)
        {
            return await _fighterStore.Get(fighterId);
        }

        [HttpPost("create")]
        public async Task<FighterModel> CreateNewFighter()
        {
            return await _fighterStore.CreateNewFighter();
        }
    }
}

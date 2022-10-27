using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Stores;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdleBattler_Server.Arena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreasureController : ControllerBase
    {
        private readonly ITreasureStore _treasureStore;

        public TreasureController(ITreasureStore treasureStore)
        {
            _treasureStore = treasureStore;
        }

        // GET api/<TreasureController>/5
        [HttpGet("{arenaId}")]
        public async Task<List<TreasureModel>> Get(Guid arenaId)
        {
            return await _treasureStore.Get(arenaId);
        }
    }
}

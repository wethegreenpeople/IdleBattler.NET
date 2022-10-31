using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Stores;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdleBattler_Server.Arena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaStore _arenaStore;

        public ArenaController(IArenaStore arenaStore)
        {
            _arenaStore = arenaStore;
        }

        [HttpGet("events/{arenaId}")]
        public async Task<IEnumerable<ArenaEvent>> GetArenaEvents(Guid arenaId)
        {
            var arena = await _arenaStore.GetArena(arenaId);
            var events = await _arenaStore.GetEvents(arena, 100);
            return events;
        }

        // GET: api/<ArenaController>
        [HttpGet("{arenaId}")]
        public async Task<ArenaModel> Get(Guid arenaId)
        {
            return await _arenaStore.GetArena(arenaId);
        }

        // POST api/<ArenaController>
        [HttpPost]
        public async Task<Guid> CreateArena()
        {
            return (await _arenaStore.GetNewArena()).Id;
        }
    }
}

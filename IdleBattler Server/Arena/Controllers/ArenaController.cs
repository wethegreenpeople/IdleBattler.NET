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
            var events = await _arenaStore.GetEvents(arena);
            return events;
        }

        // GET: api/<ArenaController>
        [HttpGet]
        public async Task<IEnumerable<ArenaModel>> Get()
        {
            return new List<ArenaModel>() { await _arenaStore.GetNewArena() };
        }

        // GET api/<ArenaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArenaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArenaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArenaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

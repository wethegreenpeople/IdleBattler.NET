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

        // GET: api/<ArenaController>
        [HttpGet]
        public IEnumerable<ArenaModel> Get()
        {
            return new List<ArenaModel>() { _arenaStore.Get() };
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

using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Stores;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdleBattler_Server.Arena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenaMovementController : ControllerBase
    {
        private readonly IMovementStore _movementStore;

        public ArenaMovementController(IMovementStore movementStore)
        {
            _movementStore = movementStore;
        }

        // GET api/<ArenaMovementController>/5/6
        //[HttpGet("{arenaId}/{fighterId}")]
        //public async Task<MovementModel> Get(Guid arenaId, Guid fighterId, [FromQuery] int? initialX, [FromQuery] int? initialY)
        //{
        //    if (initialX == null && initialY == null) return await _movementStore.Get(arenaId, fighterId);

        //    return await _movementStore.Get(arenaId, fighterId, (int)initialX, (int)initialY);
        //}

        // POST api/<ArenaMovementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArenaMovementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArenaMovementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

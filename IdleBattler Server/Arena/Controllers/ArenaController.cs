using IdleBattler_Common.Models.Arena;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdleBattler_Server.Arena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaStore _arenaStore;
        private readonly IHubContext<ArenaHub> _hubContext;

        public ArenaController(IArenaStore arenaStore, IHubContext<ArenaHub> hubContext)
        {
            _arenaStore = arenaStore;
            _hubContext = hubContext;
        }

        [HttpGet("events/{arenaId}")]
        public async Task<IEnumerable<ArenaEvent>> GetArenaEvents(Guid arenaId)
        {
            var arena = await _arenaStore.GetArena(arenaId);
            var events = await _arenaStore.GetEvents(arena, 100);
            var serializedEvents = JsonSerializer.Serialize<List<ArenaEvent>>(events.ToList());
            await _hubContext.Clients.All.SendAsync(ArenaHubConstants.EventsUpdate, arena.Id.ToString(), serializedEvents);
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

        [HttpGet("open")]
        public async Task<List<ArenaModel>> GetOpenArenas()
        {
            return await _arenaStore.GetOpenArenas();
        }

        [HttpPost("{arenaId}/addfighter/{fighterId}")]
        public async Task<ArenaModel> AddFighterToArena(Guid arenaId, Guid fighterId)
        {
            var updatedArena = await _arenaStore.AddFighterToArena(arenaId, fighterId);
            return updatedArena;
        }
    }
}

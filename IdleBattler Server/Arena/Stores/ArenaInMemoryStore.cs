using IdleBattler_Server.Arena.Models;

namespace IdleBattler_Server.Arena.Stores
{
    public class ArenaInMemoryStore : IArenaStore
    {
        public ArenaModel Get()
        {
            return new ArenaModel(Guid.NewGuid());
        }
    }
}

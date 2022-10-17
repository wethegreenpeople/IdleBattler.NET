using IdleBattler_Common.Models.Arena;

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

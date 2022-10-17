namespace IdleBattler_Server.Arena.Models
{
    public class ArenaModel
    {
        public Guid Id { get; private set; }

        public ArenaModel(Guid id)
        {
            Id = id;
        }
    }
}

namespace IdleBattler_Common.Models.Arena
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

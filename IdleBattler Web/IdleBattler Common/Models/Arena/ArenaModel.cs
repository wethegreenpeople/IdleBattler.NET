namespace IdleBattler_Common.Models.Arena
{
    public class ArenaModel
    {
        public Guid Id { get; private set; }
        public List<ArenaFighterModel> Fighters { get; private set; } = new List<ArenaFighterModel>();
        public List<TreasureModel> Treasures { get; private set; } = new List<TreasureModel>();
        public DateTime StartedTime { get; private set; }

        public ArenaModel(Guid id)
        {
            Id = id;
        }

        public void SetStartTime(DateTime startTime)
        {
            StartedTime = startTime;
        }
    }
}

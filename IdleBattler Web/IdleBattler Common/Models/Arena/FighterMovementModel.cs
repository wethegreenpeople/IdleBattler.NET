namespace IdleBattler_Common.Models.Arena
{
    public class FighterMovementModel
    {
        public Guid FighterId { get; private set; }
        public List<LocationModel> Locations { get; set; } = new List<LocationModel>();

        public FighterMovementModel(Guid fighterId)
        {
            FighterId = fighterId;
        }
    }
}

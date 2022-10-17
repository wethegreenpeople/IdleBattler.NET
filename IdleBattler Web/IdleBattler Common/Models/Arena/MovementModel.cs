namespace IdleBattler_Common.Models.Arena
{
    public class MovementModel
    {
        public List<FighterMovementModel> Movements { get; private set; }

        public MovementModel(List<FighterMovementModel> movements)
        {
            Movements = movements;
        }
    }
}

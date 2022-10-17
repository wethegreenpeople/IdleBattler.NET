namespace IdleBattler_Server.Arena.Models
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

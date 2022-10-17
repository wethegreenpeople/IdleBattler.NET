namespace IdleBattler_Server.Fighter.Models
{
    public class FighterModel
    {
        public Guid Id { get; private set; }
        public int MovementSpeed { get; private set; }

        public FighterModel(Guid id)
        {
            Id = id;
            SetInitialStats();
        }

        private void SetInitialStats()
        {
            SetMovementSpeed(1);
        }

        public void SetMovementSpeed(int movementSpeed)
        {
            this.MovementSpeed = movementSpeed;
        }
    }
}

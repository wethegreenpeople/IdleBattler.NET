using IdleBattler_Common.Shared;
using System.Text.Json.Serialization;

namespace IdleBattler_Common.Models.Fighter
{
    public class FighterModel
    {
        public Guid Id { get; private set; }
        public int MovementSpeed { get; private set; }
        public double Health { get; private set; }
        public double VisionDistance { get; private set; }

        public FighterModel(Guid id)
        {
            Id = id;
        }

        [JsonConstructor]
        public FighterModel(Guid id, int movementSpeed, double health, double visionDistance)
        {
            Id = id;
            MovementSpeed = movementSpeed;
            Health = health;
            VisionDistance = visionDistance;
        }

        public void SetInitialStats()
        {
            SetMovementSpeed(1);
            SetHealth(100);
            SetVisionDistance(10);
        }

        public void SetMovementSpeed(int movementSpeed)
        {
            this.MovementSpeed = movementSpeed;
        }

        public void SetHealth(double health)
        {
            this.Health = health;
        }

        public void TakeDamage(double damage)
        {
            SetHealth(this.Health - damage);
        }

        public void SetVisionDistance(double distance)
        {
            this.VisionDistance = distance;
        }

        public static FighterModel Copy(FighterModel fighter)
        {
            var fighterCopy = new FighterModel(fighter.Id);
            fighterCopy.SetHealth(fighter.Health);
            fighterCopy.SetMovementSpeed(fighter.MovementSpeed);
            fighterCopy.SetVisionDistance(fighter.VisionDistance);
            return fighterCopy;
        }
    }
}

using IdleBattler_Common.Shared;
using System.Text.Json.Serialization;
using IdleBattler_Common.Models.Equipment;

namespace IdleBattler_Common.Models.Fighter
{
    public class FighterModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int MovementSpeed { get; private set; }
        public double Health { get; private set; }
        public double VisionDistance { get; private set; }
        public double Damage { get; private set; }
        public List<EquipmentModel> Equipment { get; private set; } = new List<EquipmentModel>();

        public FighterModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonConstructor]
        public FighterModel(Guid id, string name, int movementSpeed, double health, double visionDistance, List<EquipmentModel> equipment, double damage)
        {
            Id = id;
            Name = name;
            MovementSpeed = movementSpeed;
            Health = health;
            VisionDistance = visionDistance;
            Equipment = equipment;
            Damage = damage;
        }

        public void SetInitialStats()
        {
            SetMovementSpeed(1);
            SetHealth(100);
            SetVisionDistance(10);
            SetDamage(1);
        }

        public void SetMovementSpeed(int movementSpeed)
        {
            this.MovementSpeed = movementSpeed;
        }

        public void SetHealth(double health)
        {
            this.Health = health;
        }

        public void SetDamage(double damage)
        {
            this.Damage = damage;
        }

        public void TakeDamage(double damage)
        {
            SetHealth(this.Health - damage);
        }

        public void SetVisionDistance(double distance)
        {
            this.VisionDistance = distance;
        }

        public void AddEquipment(EquipmentModel equipment)
        {
            this.Equipment.Add(equipment);
            this.SetHealth(this.Health += equipment.HealthChange);
            this.SetDamage(this.Damage += equipment.DamageChange);
            this.SetVisionDistance(this.VisionDistance += equipment.VisionChange);
            this.SetMovementSpeed(this.MovementSpeed += equipment.SpeedChange);
        }

        public void RemoveEquipment(EquipmentModel equipment)
        {
            this.Equipment.Remove(equipment);
            this.SetHealth(this.Health -= equipment.HealthChange);
            this.SetDamage(this.Damage -= equipment.DamageChange);
            this.SetVisionDistance(this.VisionDistance -= equipment.VisionChange);
            this.SetMovementSpeed(this.MovementSpeed -= equipment.SpeedChange);
        }

        public static FighterModel Copy(FighterModel fighter)
        {
            var fighterCopy = new FighterModel(fighter.Id, fighter.Name);
            fighterCopy.SetHealth(fighter.Health);
            fighterCopy.SetMovementSpeed(fighter.MovementSpeed);
            fighterCopy.SetVisionDistance(fighter.VisionDistance);
            fighterCopy.Equipment = fighter.Equipment;
            fighterCopy.SetDamage(fighter.Damage);
            return fighterCopy;
        }
    }
}

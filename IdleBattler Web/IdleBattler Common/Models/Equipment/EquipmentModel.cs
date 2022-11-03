using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Equipment
{
    public class EquipmentModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double HealthChange { get; private set; }
        public double DamageChange { get; private set; }
        public int SpeedChange { get; private set; }
        public int VisionChange { get; private set; }

        public EquipmentModel(int id, string name, double healthChange, double damageChange, int speedChange, int visionChange)
        {
            Id = id;
            Name = name;
            HealthChange = healthChange;
            DamageChange = damageChange;
            SpeedChange = speedChange;
            VisionChange = visionChange;
        }
    }
}

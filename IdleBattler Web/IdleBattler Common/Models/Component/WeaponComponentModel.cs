using IdleBattler_Common.Models.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Component
{
    public class WeaponComponentModel : BaseComponentModel
    {
        public int HealthModifier { get; private set; }
        public int DamageModifier { get; private set; }
        public int MovementSpeedModifier { get; private set; }

        public WeaponComponentModel(string name, int healthMod, int damageMod, int movementMod) : base(name)
        {
            this.HealthModifier = healthMod;
            this.DamageModifier = damageMod;
            this.MovementSpeedModifier = movementMod;
        }
    }
}

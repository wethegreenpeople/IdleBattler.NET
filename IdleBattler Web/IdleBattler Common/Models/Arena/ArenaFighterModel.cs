using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Shared;
using IdleBattler_Server.Fighter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Arena
{
    public class ArenaFighterModel : ArenaItemLocation
    {
        public FighterModel Fighter { get; private set; }

        public ArenaFighterModel(FighterModel fighter)
        {
            Fighter = fighter;
            this.XLocation = 0;
            this.YLocation = 0;
            this.VerticalMovementDirection = VerticalMovementDirection.Stationary;
            this.HorizontalMovementDirection = HorizontalMovementDirection.Stationary;
        }

        public void SetLocation(ArenaItemLocation location)
        {
            this.XLocation = location.XLocation;
            this.YLocation = location.YLocation;
            this.VerticalMovementDirection = location.VerticalMovementDirection;
            this.HorizontalMovementDirection = location.HorizontalMovementDirection;
        }
    }
}

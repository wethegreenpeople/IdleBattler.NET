using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Arena
{
    public class TreasureModel : ArenaItemLocation
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public TreasureModel(Guid id, string name, int xLocation, int yLocation, VerticalMovementDirection verticalMovementDirection, HorizontalMovementDirection horizontalMovementDirection) : base(xLocation, yLocation, verticalMovementDirection, horizontalMovementDirection)
        {
            Id = id;
            Name = name;
        }
    }
}

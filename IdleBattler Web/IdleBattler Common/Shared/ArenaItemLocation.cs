using IdleBattler_Common.Enums.Arena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Shared
{
    public class ArenaItemLocation
    {
        public int XLocation { get; set; }
        public int YLocation { get; set; }
        public VerticalMovementDirection VerticalMovementDirection { get; set; }
        public HorizontalMovementDirection HorizontalMovementDirection { get; set; }

        public ArenaItemLocation()
        {

        }

        public ArenaItemLocation(int xloc, int yloc)
        {
            XLocation = xloc;
            YLocation = yloc;
            VerticalMovementDirection = VerticalMovementDirection.Stationary;
            HorizontalMovementDirection = HorizontalMovementDirection.Stationary;
        }

        public ArenaItemLocation(int xloc, int yloc, VerticalMovementDirection vertDirection, HorizontalMovementDirection horiDirection)
        {
            XLocation = xloc;
            YLocation = yloc;
            VerticalMovementDirection = vertDirection;
            HorizontalMovementDirection = horiDirection;
        }
    }
}

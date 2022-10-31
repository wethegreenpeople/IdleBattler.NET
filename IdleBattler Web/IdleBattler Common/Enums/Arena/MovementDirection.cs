using System.Collections;
using System.Text.Json.Serialization;

namespace IdleBattler_Common.Enums.Arena
{
    public class HorizontalMovementDirection
    {
        public string Value { get; private set; }

        public HorizontalMovementDirection(string value) { Value = value; }

        public static HorizontalMovementDirection Stationary => new(nameof(Stationary));
        public static HorizontalMovementDirection Left => new(nameof(Left));
        public static HorizontalMovementDirection Right => new(nameof(Right));

        public static HorizontalMovementDirection ReverseDirection(HorizontalMovementDirection currentDirection, Random rand)
        {
            var directions = new List<HorizontalMovementDirection>() { Left, Stationary, Right };
            if (currentDirection == Left) return Right;
            else if (currentDirection == Right) return Left;
            else return Stationary;
        }

        public static HorizontalMovementDirection GetRandomDirection()
        {
            var rand = new Random();
            var directions = new List<HorizontalMovementDirection>() { Left, Right };
            return directions.ElementAt(rand.Next(0, 2));
        }

        public static bool operator ==(HorizontalMovementDirection obj1, HorizontalMovementDirection obj2)
        {
            return String.Equals(obj1.Value, obj2.Value);
        }

        public static bool operator !=(HorizontalMovementDirection obj1, HorizontalMovementDirection obj2)
        {
            return !String.Equals(obj1.Value, obj2.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this.Value, ((HorizontalMovementDirection)obj).Value))
            {
                return true;
            }

            if (obj is null) return false;

            return false;
        }
    }

    public class VerticalMovementDirection
    {
        public string Value { get; private set; }

        public VerticalMovementDirection(string value) { Value = value; }

        public static VerticalMovementDirection Stationary => new(nameof(Stationary));
        public static VerticalMovementDirection Up => new(nameof(Up));
        public static VerticalMovementDirection Down => new(nameof(Down));

        public static VerticalMovementDirection ReverseDirection(VerticalMovementDirection currentDirection, Random rand)
        {
            var directions = new List<VerticalMovementDirection>() { Up, Stationary, Down };
            if (currentDirection == Up) return Down;
            else if (currentDirection == Down) return Up;
            else return Stationary;
        }

        public static VerticalMovementDirection GetRandomDirection()
        {
            var rand = new Random();
            var directions = new List<VerticalMovementDirection>() { Up, Down };
            return directions.ElementAt(rand.Next(0, 2));
        }

        public static bool operator ==(VerticalMovementDirection obj1, VerticalMovementDirection obj2)
        {
            return String.Equals(obj1.Value, obj2.Value);
        }

        public static bool operator !=(VerticalMovementDirection obj1, VerticalMovementDirection obj2)
        {
            return !String.Equals(obj1.Value, obj2.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this.Value, ((VerticalMovementDirection)obj).Value))
            {
                return true;
            }

            if (obj is null) return false;

            return false;
        }
    }
}

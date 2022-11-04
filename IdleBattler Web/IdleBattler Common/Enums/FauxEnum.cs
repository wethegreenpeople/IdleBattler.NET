using IdleBattler_Common.Enums.Arena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Enums
{
    public abstract class FauxEnum
    {
        public string Value { get; private set; }

        public FauxEnum(string value) { Value = value; }

        public static bool operator ==(FauxEnum obj1, FauxEnum obj2)
        {
            return String.Equals(obj1.Value, obj2.Value);
        }

        public static bool operator !=(FauxEnum obj1, FauxEnum obj2)
        {
            return !String.Equals(obj1.Value, obj2.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this.Value, ((FauxEnum)obj).Value))
            {
                return true;
            }

            if (obj is null) return false;

            return false;
        }
    }
}

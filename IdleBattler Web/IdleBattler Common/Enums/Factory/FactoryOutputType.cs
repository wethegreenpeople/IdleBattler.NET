using IdleBattler_Common.Enums.Arena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Enums.Factory
{
    public class FactoryOutputType : FauxEnum
    {
        public FactoryOutputType(string value) : base(value) { }

        public static FactoryOutputType Gold => new(nameof(Gold));
        public static FactoryOutputType Silver => new(nameof(Silver));
        public static FactoryOutputType Copper => new(nameof(Copper));
    }
}

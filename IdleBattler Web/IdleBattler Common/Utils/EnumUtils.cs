using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Utils
{
    public class EnumUtils
    {
        public static T GetRandomEnumValue<T>(Random rand)
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rand.Next(values.Length));
        }
    }
}

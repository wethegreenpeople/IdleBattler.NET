using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Component
{
    public abstract class BaseComponentModel
    {
        public string Name { get; private set; }

        public BaseComponentModel(string name)
        {
            this.Name = name;
        }
    }
}

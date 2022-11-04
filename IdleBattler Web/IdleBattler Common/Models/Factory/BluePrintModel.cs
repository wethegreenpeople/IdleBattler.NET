using IdleBattler_Common.Enums.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Factory
{
    public class BluePrintModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int GoldCost { get; private set; }
        public int SilverCost { get; private set; }
        public int CopperCost { get; private set; }

        public BluePrintModel(int id, string name, int goldCost, int silverCost, int copperCost)
        {
            this.Id = id;
            this.Name = name;
            this.GoldCost = GoldCost;
            this.SilverCost = SilverCost;
            this.CopperCost = copperCost;
        }
    }
}

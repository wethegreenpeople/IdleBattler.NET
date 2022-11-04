using IdleBattler_Common.Enums.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Factory
{
    public class FactoryModel : BluePrintModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Output { get; private set; }
        public FactoryOutputType OutputType { get; private set;}

        public FactoryModel(int id, string name, int output, FactoryOutputType outputType, int gold, int silver, int copper) : base(id, name, gold, silver, copper)
        {
            Id = id;
            Name = name;
            Output = output;
            OutputType = outputType;
        }
    }
}

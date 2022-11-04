using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Component
{
    public class ComponentResultModel<T>
    {
        public T FirstComponent { get; private set; }
        public T SecondComponent { get; private set; }
        public T ThirdComponent { get; private set; }
        public T FourthComponent { get; private set; }

        public ComponentResultModel(T firstComponent, T secondComponent, T thirdComponent, T fourthComponent)
        {
            FirstComponent = firstComponent;
            SecondComponent = secondComponent;
            ThirdComponent = thirdComponent;
            FourthComponent = fourthComponent;
        }
    }
}

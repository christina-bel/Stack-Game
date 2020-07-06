using Stack_Game.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Accessory
{
    class PeakDecorator : Decorator
    {
        IUnit unit { get; set; }

        public PeakDecorator(Component comp, IUnit un) : base(comp)
        {
            unit = un;
        }
        public override string AddAccessory()
        {
            unit.Name += " с пикой в руках";
            unit.Attack += 15;
            return unit.Name;
        }

        public override IUnit GetUnit()
        {
            return unit;
        }
    }

}

using Stack_Game.Units;
using System;
using System.Collections.Generic;

namespace Stack_Game.Accessory
{

    class ArmorDecorator : Decorator
    {
        IUnit unit { get; set; }

        public ArmorDecorator(Component comp, IUnit un) : base(comp)
        {
            unit = un;
        }
        public override string AddAccessory()
        {
            unit.Name += " в броне";
            unit.Defence += 15;
            return unit.Name;
        }

        public override IUnit GetUnit()
        {
            return unit;
        }
    }
}

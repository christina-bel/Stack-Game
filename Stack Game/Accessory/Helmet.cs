using Stack_Game.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Accessory
{

    class HelmetDecorator : Decorator
    {
        IUnit unit { get; set; }

        public HelmetDecorator(Component comp, IUnit un) : base(comp)
        {
            unit = un;
        }
        public override string AddAccessory()
        {
            unit.Name += " со шлемом";
            unit.Defence += 15;
            return unit.Name;
        }

        public override IUnit GetUnit()
        {
            return unit;
        }
    }
}

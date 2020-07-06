using Stack_Game.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Accessory
{

    class HorseDecorator : Decorator
    {
        IUnit unit { get; set; }

        public HorseDecorator(Component comp, IUnit un) : base(comp)
        {
            unit = un;
        }
        public override string AddAccessory()
        {
            unit.Name += " с конем Юлием";
            unit.Health += 15;
            return unit.Name;
        }

        public override IUnit GetUnit()
        {
            return unit;
        }
    }
}

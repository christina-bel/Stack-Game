using Stack_Game.Units;
using System;
using System.Collections.Generic;

namespace Stack_Game.Accessory
{
    public abstract class Component
    {
        public abstract string AddAccessory();
        public abstract IUnit GetUnit();
    }

    class AccessoryComponent : Component
    {
        public override string AddAccessory() { return ""; }
        public override IUnit GetUnit()
        {
            return null;
        }
    }
}



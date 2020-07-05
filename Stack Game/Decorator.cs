using Stack_Game.Accessory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game
{
    abstract class Decorator : AccessoryComponent
    {
        protected AccessoryComponent component;

        public Decorator(AccessoryComponent comp)
        {
            this.component = comp;
        }

        public void SetComponent(AccessoryComponent comp)
        {
            this.component = comp;
        }
        public override string AddAccessory()
        {
            if (this.component != null)
            {
                return this.component.AddAccessory();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    class AccessoryDecorator : Decorator
    {
        IUnit unit { get; set; }

        public AccessoryDecorator(AccessoryComponent comp, IUnit un) : base(comp)
        {
            unit = un;
        }
        public override string AddAccessory()
        {
           unit.Name += " " + base.AddAccessory();
           unit.Defence += 15;
           return unit.Name;
        }

        public IUnit GetUnit()
        {
            return unit;
        }
    }
}
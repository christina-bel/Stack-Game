using Stack_Game.Accessory;
using Stack_Game.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game
{
    abstract class Decorator : Component
    {
        protected Component component;

        public Decorator(Component comp)
        {
            this.component = comp;
        }

        public void SetComponent(Component comp)
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
        public override IUnit GetUnit()
        {
            if (this.component != null)
            {
                return this.component.GetUnit();
            }
            else
            {
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{

    class Wizard : Unit, IUnit, ITreatable, ISpecialAction
    {
        public int SpecialActionStrength { get; set; }
        public int SpecialActionRange { get; set; }
        public Wizard()
        {
            Price = DefaultSettings.Wizard.Price;
            Health = DefaultSettings.Wizard.Health;
            Attack = DefaultSettings.Wizard.Attack;
            Defence = DefaultSettings.Wizard.Defence;
            Name = DefaultSettings.Wizard.Name;
            SpecialActionRange = DefaultSettings.Wizard.Range;
            SpecialActionStrength = DefaultSettings.Wizard.Strength;
        }
        public Wizard(Wizard wizard)
        {
            Price = wizard.Price;
            Health = wizard.Health;
            Attack = wizard.Attack;
            Defence = wizard.Defence;
            Name = wizard.Name;
            SpecialActionRange = wizard.SpecialActionRange;
            SpecialActionStrength = wizard.SpecialActionStrength;
        }

        public override IUnit Copy()
        {
            return new Wizard(this);
        }
        public void Treat(int health)
        {
            Health += health;
            if (Health > 100)
                Health = 100;
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            if (unit is ICloneable)
            {
                return ((ICloneable)unit).Copy();
            }
            else return null;
        }
    }
}

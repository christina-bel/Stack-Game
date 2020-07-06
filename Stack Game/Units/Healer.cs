using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{

    class Healer : Unit, IUnit, ISpecialAction, ITreatable
    {
        public int SpecialActionStrength { get; set; }
        public int SpecialActionRange { get; set; }
        public Healer()
        {
            Price = DefaultSettings.Healer.Price;
            Health = DefaultSettings.Healer.Health;
            Attack = DefaultSettings.Healer.Attack;
            Defence = DefaultSettings.Healer.Defence;
            Name = DefaultSettings.Healer.Name;
            SpecialActionRange = DefaultSettings.Healer.Range;
            SpecialActionStrength = DefaultSettings.Healer.Strength;
        }

        public Healer(Healer healer)
        {
            Price = healer.Price;
            Health = healer.Health;
            Attack = healer.Attack;
            Defence = healer.Defence;
            Name = healer.Name;
            SpecialActionRange = healer.SpecialActionRange;
            SpecialActionStrength = healer.SpecialActionStrength;
        }

        public override IUnit Copy()
        {
            return new Healer(this);
        }
        public IUnit DoSpecialAction(IUnit unit)
        {
            if (unit is ITreatable)
            {
                ((ITreatable)unit).Treat(SpecialActionStrength);
                return unit;
            }
            return null;
        }
        public void Treat(int health)
        {
            Health += health;
            if (Health > 100)
                Health = 100;
        }
    }

}

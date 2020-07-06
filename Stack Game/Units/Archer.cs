using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{

    class Archer : Unit, IUnit, ISpecialAction, ITreatable, ICloneable
    {
        public int SpecialActionStrength { get; set; }
        public int SpecialActionRange { get; set; }
        public Archer()
        {
            Price = DefaultSettings.Archer.Price;
            Health = DefaultSettings.Archer.Health;
            Attack = DefaultSettings.Archer.Attack;
            Defence = DefaultSettings.Archer.Defence;
            Name = DefaultSettings.Archer.Name;
            SpecialActionRange = DefaultSettings.Archer.Range;
            SpecialActionStrength = DefaultSettings.Archer.Strength;
        }

        public Archer(Archer archer)
        {
            Price = archer.Price;
            Health = archer.Health;
            Attack = archer.Attack;
            Defence = archer.Defence;
            Name = archer.Name;
            SpecialActionRange = archer.SpecialActionRange;
            SpecialActionStrength = archer.SpecialActionStrength;
        }

        public override IUnit Copy()
        {
            return new Archer(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            if (SpecialActionStrength >= unit.Defence + unit.Health)
                return this; // смерть противника

            unit.Defence -= SpecialActionStrength;
            unit.Health += unit.Defence;
            unit.Defence = 0; // отдадим защиту в пользу здоровья
            if (unit.Health < 0)
                unit.Health = 0;
            return unit;
        }
        public void Treat(int health)
        {
            Health += health;
            if (Health > 100)
                Health = 100;
        }
    }

}

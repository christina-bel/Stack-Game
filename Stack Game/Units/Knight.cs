using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{
    class Knight : Unit, IUnit, IAmmunition
    {
        public Dictionary<int, string> Access { get; set; }
        public Knight()
        {
            Price = DefaultSettings.Knight.Price;
            Health = DefaultSettings.Knight.Health;
            Attack = DefaultSettings.Knight.Attack;
            Defence = DefaultSettings.Knight.Defence;
            Name = DefaultSettings.Knight.Name;
        }
        public Knight(Knight knight)
        {
            Price = knight.Price;
            Health = knight.Health;
            Attack = knight.Attack;
            Defence = knight.Defence;
            Name = knight.Name;
        }

        public override IUnit Copy()
        {
            return new Knight(this);

        }
    }
}

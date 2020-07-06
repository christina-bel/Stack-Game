using Stack_Game.Accessory;
using Stack_Game.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{
    public interface IUnit 
    {
        int Price { get; } //стоиомость
        int Health { get; set; } //здоровье
        int Attack { get; set; } //сила атаки
        int Defence { get; set; } //уровень защиты
        string Name { get; set; }
        IUnit Fight(IUnit unit); // битва (итог для противника)
        IUnit Copy(); // отвчает за реализацию прототипа
        string GetInfo();
    }

    public abstract class Unit : IUnit
    {
        public int Price { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public string Name { get; set; }
        public virtual IUnit Copy()
        {
            return null;
        }
        public virtual string GetInfo()
        {
            return $"{Name} имеет Здоровье {Health}, Силу атаки {Attack}, Уровень защиты {Defence}";
        }

        public virtual IUnit Fight(IUnit unit)
        {
            if (unit.Defence + unit.Health <= Attack) // смерть
                return unit;

            else if (unit.Defence > Attack) // воин жив, нанесение урона влияет на защиту
            {   
                unit.Defence -= Attack;
                return null;
            }

            unit.Health -= Attack - unit.Defence; // воин жив, нанесение урона влияет на здоровье
            unit.Defence = 0;
            return null;
        }
    }
    public interface ISpecialAction
    {
        IUnit DoSpecialAction(IUnit unit); // особые умения (итог для противника)
        int SpecialActionStrength { get; set; }
        int SpecialActionRange { get; set; }
    }
    public interface ITreatable
    {
        void Treat(int health);
    }
    public interface ICloneable
    {
        IUnit Copy();
    }

    public interface IAmmunition
    {
        Dictionary<int, string> Access { get; set; }
    }
   
}

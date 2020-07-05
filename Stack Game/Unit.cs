using Stack_Game.Accessory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game
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
  
    class Infantry : Unit, IUnit, ITreatable, ICloneable, ISpecialAction
    {
        public int SpecialActionStrength { get; set; }
        public int SpecialActionRange { get; set; }
        public Infantry()
        {
            Price = DefaultSettings.Infantry.Price;
            Health = DefaultSettings.Infantry.Health;
            Attack = DefaultSettings.Infantry.Attack;
            Defence = DefaultSettings.Infantry.Defence;
            Name = DefaultSettings.Infantry.Name;
            SpecialActionRange = DefaultSettings.Infantry.Range;
        }
        public Infantry(Infantry infantry)
        {
            Price = infantry.Price;
            Health = infantry.Health;
            Attack = infantry.Attack;
            Defence = infantry.Defence;
            Name = infantry.Name;
            SpecialActionRange = infantry.SpecialActionRange;
        }
        public override IUnit Copy()
        {
            return new Infantry(this);
        }
        public void Treat(int health)
        {
            Health += health;
            if (Health > 100)
                Health = 100;
        }

        public IUnit DoSpecialAction(IUnit unit)
        {

            if (unit is IAmmunition)
            {

                AccessoryComponent accessory = GetAccessory((IAmmunition)(unit));
                if (accessory == null)
                    return unit;

                var decorAmmun = new AccessoryDecorator(accessory, unit);

                decorAmmun.AddAccessory();
                return decorAmmun.GetUnit();
            }
            return unit;
        }

        private AccessoryComponent GetAccessory(IAmmunition unit)
        {
            Random random = new Random();
            AccessoryComponent accessory = null;
            int cloth = 0;
            bool end = true;

            if (unit.Access == null)
                unit.Access = new Dictionary<int, string>();

            while (end)
            {
                if (unit.Access.Count == 4)
                    return null;
                cloth = random.Next(0, 4);
                if (unit.Access.ContainsKey(cloth))
                    continue;
                else
                    end = false;
            }
            switch (cloth)
            {
                case 0:
                    accessory = new HelmetComponent();
                    break;

                case 1:
                    accessory = new ArmorComponent();
                    break;

                case 2:
                    accessory = new HorseComponent();
                    break;

                case 3:
                    accessory = new PeakComponent();
                    break;
            }
            unit.Access.Add(cloth, accessory.ToString());
            return accessory;
        }
    }

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

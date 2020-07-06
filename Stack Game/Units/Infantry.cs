using Stack_Game.Accessory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game.Units
{
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
                if (((IAmmunition)unit).Access == null)
                    ((IAmmunition)unit).Access = new Dictionary<int, string>();

                if (((IAmmunition)unit).Access.Count == 4)
                    return null;

                Component accessory = new AccessoryComponent();

                Random random = new Random();
                int cloth = 0;
                bool end = true;

                while (end)
                {
                    cloth = random.Next(0, 4);
                    if (((IAmmunition)unit).Access.ContainsKey(cloth))
                        continue;
                    else
                        end = false;
                }
                var nameAmmunition = "";
                switch (cloth)
                {
                    case 0:
                        nameAmmunition = "Helmet";
                        accessory = new HelmetDecorator(accessory, unit);
                        accessory.AddAccessory();
                        break;

                    case 1:
                        nameAmmunition = "Armor";
                        accessory = new ArmorDecorator(accessory, unit);
                        accessory.AddAccessory();
                        break;

                    case 2:
                        nameAmmunition = "Horse";
                        accessory = new HorseDecorator(accessory, unit);
                        accessory.AddAccessory();
                        break;

                    case 3:
                        nameAmmunition = "Peak";
                        accessory = new PeakDecorator(accessory, unit);
                        accessory.AddAccessory();
                        break;
                }
                ((IAmmunition)unit).Access.Add(cloth, nameAmmunition);
                return accessory.GetUnit();

            }

            return null;
        }
       
    }

}

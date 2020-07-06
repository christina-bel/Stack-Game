using Stack_Game.Units;
using System;
using System.Collections.Generic;

namespace Stack_Game
{
    public interface IArmy
    {
        List<IUnit> ArmyList { get; }
        string ArmyName { get; }
        List<IUnit> ConstructArmy(int money, string name);
        int Count();
        bool IsEmpty();
        IUnit this[int i] { get; }
        IArmy ArmyDeepCopy();
        int Remove(IUnit unit);
        int IndexOf(IUnit unit);
        string NotifyAboutDeath();
        void Push(IUnit unit);

    }
    public class Army : IArmy
    {
        public Army(int money, string armyName)
        {
            ArmyList = new List<IUnit>();
            ArmyName = armyName;
            ArmyList = ConstructArmy(money, armyName);
        }
        public Army(Army copyArmy)
        {
            ArmyName = copyArmy.ArmyName;
            ArmyList = new List<IUnit>();
            foreach (IUnit unit in copyArmy.ArmyList)
            {
                Push(unit.Copy());
            }
        }

        public List<IUnit> ArmyList { get; set; }
        public string ArmyName { get; set; }

        public List<IUnit> ConstructArmy(int money, string name)
        {
            Random random = new Random();

            var infantryFactory = new InfantryFactory();
            var knightFactory = new KnightFactory();
            var archerFactory = new ArcherFactory();
            var healerFactory = new HealerFactory();
            var wizardFactory = new WizardFactory();

            while (money > 0)
            {
                int character = random.Next(1, 6);

                if (character == 1)
                {
                    if (money >= DefaultSettings.Infantry.Price)
                    {
                        var newUnit = infantryFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else
                        break;
                }
                else if (character == 2)
                {
                    if (money >= DefaultSettings.Knight.Price)
                    {
                        var newUnit = knightFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else
                    {
                        if (money >= DefaultSettings.Infantry.Price)
                        {
                            var newUnit = infantryFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                            break;
                    }
                }
                else if (character == 3)
                {
                    if (money >= DefaultSettings.Archer.Price)
                    {
                        var newUnit = archerFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else
                    {
                        if (money >= DefaultSettings.Infantry.Price)
                        {
                            var newUnit = infantryFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                            break;
                    }

                }
                else if (character == 4)
                {
                    if (money >= DefaultSettings.Healer.Price)
                    {
                        var newUnit = healerFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else if (money >= DefaultSettings.Archer.Price)
                    {
                        Random between = new Random();
                        if (between.Next(1, 11) % 2 == 0)
                        {
                            var newUnit = archerFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                        {
                            var newUnit = knightFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                    }
                    else
                    {
                        if (money >= DefaultSettings.Infantry.Price)
                        {
                            var newUnit = infantryFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    if (money >= DefaultSettings.Wizard.Price)
                    {
                        var newUnit = wizardFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else if(money >= DefaultSettings.Healer.Price)
                    {
                        var newUnit = healerFactory.CreateUnit();
                        ArmyList.Add(newUnit);
                        money -= newUnit.Price;
                    }
                    else if (money >= DefaultSettings.Archer.Price)
                    {
                        Random between = new Random();
                        if (between.Next(1, 11) % 2 == 0)
                        {
                            var newUnit = archerFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                        {
                            var newUnit = knightFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                    }
                    else
                    {
                        if (money >= DefaultSettings.Infantry.Price)
                        {
                            var newUnit = infantryFactory.CreateUnit();
                            ArmyList.Add(newUnit);
                            money -= newUnit.Price;
                        }
                        else
                            break;
                    }
                }
            }
            return ArmyList;
        }

        public IUnit this[int i]
        {
            get
            {
                return ArmyList[i];
            }
        }

        public void Push(IUnit unit)
        {
            ArmyList.Add(unit);
        }
        public int Count()
        {
            return ArmyList.Count;
        }

        public bool IsEmpty()
        {
            if (ArmyList.Count == 0) 
                return true;
            else
                return false;
        }
        public IArmy ArmyDeepCopy()
        {
            return new Army(this);
        }
        public int Remove(IUnit removeUnit)
        {
            var ind = ArmyList.IndexOf(removeUnit);
            ArmyList.Remove(removeUnit);
            return ind;
        }
        public int IndexOf(IUnit unit)
        {
            return ArmyList.IndexOf(unit);
        }
        public string NotifyAboutDeath()
        {
            IFactoryUnit factoryUnit = new NotifierOfUnit();
            return factoryUnit.DeathNotifier();
        }
    }

}

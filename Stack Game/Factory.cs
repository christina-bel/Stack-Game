using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game
{
    public interface IArmyFactory
    {
        IArmy CreateArmy(int money, string armyName);
    }

   public class ArmyFactory : IArmyFactory
    {
        public IArmy CreateArmy(int money, string armyName)
        {
            return new Army(money, armyName);
        }
    }

    public interface IFactoryUnit
    {
        IUnit CreateUnit();
        string DeathNotifier();
    }
    class NotifierOfUnit : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return null;
        }
        public string DeathNotifier()
        {
            return " B-E-E-E-E-E-P";
        }
    }

    class InfantryFactory : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return new Infantry();
        }
        public string DeathNotifier()
        {
            return "";
        }
    }

    class KnightFactory : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return new Knight();
        }
        public string DeathNotifier()
        {
            return "";
        }
    }


    class ArcherFactory : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return new Archer();
        }
        public string DeathNotifier()
        {
            return "";
        }
    }

    class HealerFactory : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return new Healer();
        }
        public string DeathNotifier()
        {
            return "";
        }
    }

    class WizardFactory : IFactoryUnit
    {
        public IUnit CreateUnit()
        {
            return new Wizard();
        }
        public string DeathNotifier()
        {
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Stack_Game
{ 
    public interface IStrategy
    {
        public int SizeOfRow { get; }
        string Info(IArmy army);

        public List<IUnit> GetTargetsForArcher(IArmy firstArmy, IArmy secondArmy, ISpecialAction unitSpecial)
        {
            var targetUnits = new List<IUnit>();
            int row = (firstArmy.Count() - firstArmy.IndexOf((IUnit)unitSpecial) - 1) % SizeOfRow;
            int column = (firstArmy.Count() - firstArmy.IndexOf((IUnit)unitSpecial) - 1) / SizeOfRow;
            if (column >= unitSpecial.SpecialActionRange)
                return targetUnits;
            for (int i = secondArmy.Count() - 1 - row, targetsCount = unitSpecial.SpecialActionRange - column; i >= 0 && targetsCount > 0; i -= SizeOfRow, targetsCount--)
                targetUnits.Add(secondArmy[i]);
            return targetUnits;
        }

        public List<IUnit> GetTargetsForInfantry(IArmy army, ISpecialAction unit)
        {
            var targetUnits = new List<IUnit>();
            int index = army.IndexOf((IUnit)unit);

            int row = (army.Count() - index - 1) % SizeOfRow;
            int column = (army.Count() - index - 1) / SizeOfRow;

            for (int i = 0; i < army.Count(); i++)
            {
                int r = (army.Count() - i - 1) % SizeOfRow;
                int c = (army.Count() - i - 1) / SizeOfRow;
                if (Math.Sqrt((r - row) * (r - row) + (c - column) * (c - column)) <= unit.SpecialActionRange)
                    if (army[i] is IAmmunition)
                        targetUnits.Add(army[i]);
            }
            return targetUnits;
        }

        public List<IUnit> GetTargetsForHealer(IArmy army, ISpecialAction unit)
        {
            var targetUnits = new List<IUnit>();
            int index = army.IndexOf((IUnit)unit);

            int row = (army.Count() - index - 1) % SizeOfRow;
            int column = (army.Count() - index - 1) / SizeOfRow;

           for (int i = 0; i < army.Count(); i++)
            {
                int r = (army.Count() - i - 1) % SizeOfRow;
                int c = (army.Count() - i - 1) / SizeOfRow;
                if (Math.Sqrt((r - row) * (r - row) + (c - column) * (c - column)) <= unit.SpecialActionRange)
                    targetUnits.Add(army[i]);
            }
            return targetUnits;
        }
    }

    class OneToOneStrategy : IStrategy
    {
        public int SizeOfRow { get; set; } = 1;
        public string Info(IArmy army)
        {
            var armyInfo = $"Армия: {army.ArmyName}";
            int num = 1;
            for (int i = army.Count() - 1; i >= 0; i--)
            {
                armyInfo += $"\n{num}. {army[i].GetInfo()}";
                num++;
            }
            return armyInfo;
        }

    }


    class ThreeToThreeStrategy : IStrategy
    {
        public int SizeOfRow { get; set; } = 3;
        public string Info(IArmy army)
        {
            var armyInfo = $"Армия: {army.ArmyName}";
            for (int j = 0; j < SizeOfRow; j++)
            {
                armyInfo += $"\nКолонна {j + 1}:";
                for (int line = 1, i = army.Count() - j - 1; i >= 0; line++, i -= SizeOfRow)
                {
                    armyInfo += $"\nРяд {line}. {army[i].GetInfo()}";
                }
            }
            return armyInfo;

        }
    }


    class NToNStrategy : IStrategy
    {
        public int SizeOfRow { get; set; }
        public NToNStrategy(IArmy firstArmy, IArmy secondArmy)
        {
            SizeOfRow = Math.Min(firstArmy.Count(), secondArmy.Count());
        }
        public string Info(IArmy army)
        {

            var info = String.Format("Армия {0}:", army.ArmyName);

            for (int j = 0; j < SizeOfRow; j++)
            {
                info += String.Format("\nКолонна {0}:", j + 1);
                for (int line = 1, i = army.Count() - SizeOfRow + j; i >= 0; line++, i -= SizeOfRow)
                {
                    info += String.Format("\nРяд {0}. {1}", line, army[i].GetInfo());
                }
            }
            return info;
        }

    }
}


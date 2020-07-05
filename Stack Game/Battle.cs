using System;
using System.Collections.Generic;

namespace Stack_Game
{
    class Battle
    {
        public IStrategy GameStrategy { get; set; }
        public IArmy FirstArmy { get; set; }
        public IArmy SecondArmy { get; set; }
        public string StepInfo { get; set; }
        public string TillEndInfo { get; set; }
        public bool GameOver { get; set; } = false;
        bool Subscribed { get; set; } = false;
        public Battle(IArmy firstArmy, IArmy secondArmy, IStrategy strategy)
        {
            FirstArmy = firstArmy;
            SecondArmy = secondArmy;
            GameStrategy = strategy;
        }
        public string ArmyInfo()
        {
            return $"{GameStrategy.Info(FirstArmy)}\n\n\n\t\t\t------VS------\n\n\n{GameStrategy.Info(SecondArmy)}";
        }
        public void Subscribe()
        {
            Subscribed = true;
        }

        public void UnSubscribe()
        {
            Subscribed = false;
        }

        private void DoSpecialAction(IArmy first, IArmy second)
        {
            if (EndGame())
                return;

            for (int i = 0; i < GameStrategy.SizeOfRow; i++)
            {
                var specialUnits = GetSpecialUnitsInRow(first, i);
                if (specialUnits.Count == 0)
                    continue;
                
                Random random = new Random();
                int indexSpecial = random.Next(0, specialUnits.Count - 1);
                var targetUnits = GetTargets(first, second, specialUnits[indexSpecial]);
                
                if (targetUnits.Count == 0)
                    continue;

                int indexTargets = random.Next(0, targetUnits.Count - 1);

                IUnit beforeSpecialAction = targetUnits[indexTargets].Copy();
                IUnit afterSpecialAction = specialUnits[indexSpecial].DoSpecialAction(targetUnits[indexTargets]);

                StepInfo += "\n\n\t\t\t------ОСОБОЕ ДЕЙСТВИЕ------";

                if (specialUnits[indexSpecial] is Archer)
                {
                    StepInfo += $"\n\nв Армии {first.ArmyName} - {((IUnit)specialUnits[indexSpecial]).GetInfo()}\n\n\t\t\t------СТРЕЛЯЕТ ИЗ ЛУКА В------\n\n в Армию {second.ArmyName} - {beforeSpecialAction.GetInfo()}";

                    if (afterSpecialAction == specialUnits[indexSpecial])
                    {
                        StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nв Армии {second.ArmyName} ПОГИБАЕТ {targetUnits[indexTargets].Name}\n";
                        if (Subscribed == true)
                            StepInfo += $"\n\n\t\t\t{second.NotifyAboutDeath()}\n\n";
                        second.Remove(targetUnits[indexTargets]);
                    }
                    else
                        StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nв Армии {second.ArmyName} РАНЕН {targetUnits[indexTargets].GetInfo()}\n";
                }

                else if (specialUnits[indexSpecial] is Healer)
                {
                    if (afterSpecialAction != null)
                    {
                        StepInfo += $"\n\nв Армии{first.ArmyName} - {((IUnit)specialUnits[indexSpecial]).GetInfo()}\n\n\t\t\t------ЛЕЧИТ------\n\n в Армии {first.ArmyName} - {beforeSpecialAction.GetInfo()}";
                        StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nв Армии {first.ArmyName} - {targetUnits[indexTargets].GetInfo()} ВЫЛЕЧЕН\n";
                    }
                    else
                        StepInfo += $"\n\nЦелитель уснул после ночного дежурства! Никто НЕ ВЫЛЕЧЕН в Армии {first.ArmyName}\n";
                    
                }

                else if (specialUnits[indexSpecial] is Wizard)
                {
                    if (afterSpecialAction != null)
                    {
                        StepInfo += $"\n\nв Армии {first.ArmyName} - {((IUnit)specialUnits[indexSpecial]).GetInfo()}\n\n\t\t\t------КЛОНИРУЕТ------\n\n в Армии {first.ArmyName} - {beforeSpecialAction.GetInfo()}";
                        StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nв Армии {first.ArmyName} - {targetUnits[indexTargets].GetInfo()} УСПЕШНО КЛОНИРОВАН.\n";
                        first.Push(afterSpecialAction);
                    }
                    else
                    {
                        StepInfo += $"\n\nМаг столкнулся с кражей зелий. НИКТО НЕ КЛОНИРОВАН в Армии {first.ArmyName}\n";
                    }
                }
                else if (specialUnits[indexSpecial] is Infantry)
                    {
                    if (afterSpecialAction.Name != beforeSpecialAction.Name)
                    {
                        targetUnits[indexTargets] = afterSpecialAction;
                        StepInfo += $"\n\nв Армии {first.ArmyName} - {((IUnit)specialUnits[indexSpecial]).GetInfo()}\n\n\t\t\t------ОДЕВАЕТ------\n\n в Армии {first.ArmyName} - {beforeSpecialAction.GetInfo()}";
                        StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nв Армии {first.ArmyName} - {targetUnits[indexTargets].GetInfo()} ОДЕТ В СПЕЦИАЛЬНУЮ АМУНИЦИЮ!\n";

                    }
                    else
                    {
                        StepInfo += $"\n\nХодоки украли амуницию! НИКТО НЕ ОДЕТ в Армии {first.ArmyName}\n";
                    }
                }
            }    
        }
        private List<ISpecialAction> GetSpecialUnitsInRow(IArmy army, int Column)
        {
            var specialActions = new List<ISpecialAction>();
            for (int i = army.Count() - Column - 1; i >= 0; i -= GameStrategy.SizeOfRow)
            {
                if (army[i] is ISpecialAction)
                    specialActions.Add(army[i] as ISpecialAction);
            }
            return specialActions;
        }
        private List<IUnit> GetTargets(IArmy first, IArmy second, ISpecialAction unitSpecial)
        {
            if (unitSpecial is Archer)
                return GameStrategy.GetTargetsForArcher(first, second, unitSpecial);
            else if (unitSpecial is Infantry)
                return GameStrategy.GetTargetsForInfantry(first, unitSpecial);
           else
               return GameStrategy.GetTargetsForHealer(first, unitSpecial);
        }

        public void Step()
        {
            if (GameOver)
            {
                StepInfo = "\n\n\t\t\t------ИГРА ОКОНЧЕНА!!! Создайте новые армии!------";
                return;
            }
            else
            {
                StepInfo = "\n\t\t\t------FIGHT!------\n";
                Fight(FirstArmy, SecondArmy);
                Fight(SecondArmy, FirstArmy);
                DoSpecialAction(FirstArmy, SecondArmy);
                DoSpecialAction(SecondArmy, FirstArmy);

                if (FirstArmy.Count() == 0 || SecondArmy.Count() == 0)
                    GameOver = true;
            }
        }
        public void PlayTillEnd()
        {

            if (GameOver)
            {
                StepInfo = "\n\n\t\t\t------ИГРА ОКОНЧЕНА!!! Создайте новые армии!------";
                return;
            }
            else
            {
                while (!GameOver)
                {
                    Step();
                    TillEndInfo += StepInfo;
                }
            }
        }

        private void Fight(IArmy firstArmy, IArmy secondArmy)
        {
            if (EndGame())
                return;

            List<IUnit> firstLineFirstArmy = GetFirstLine(firstArmy);
            List<IUnit> firstLineSecondArmy = GetFirstLine(secondArmy);

            var minPossiblePairs = Math.Min(GameStrategy.SizeOfRow, Math.Min(firstArmy.Count(), secondArmy.Count())); //Сражаются бойцы, у которых есть противник

            for (int i = 0; i < minPossiblePairs; i++)
            {
                IUnit Attecker = firstLineFirstArmy[i];
                IUnit Defender = firstLineSecondArmy[i];
                StepInfo += $"\n\nАрмия {firstArmy.ArmyName} - {Attecker.GetInfo()}\n\n\t\t\t------СРАЖАЕТСЯ С------\n\nАрмией {secondArmy.ArmyName} - {Defender.GetInfo()}\n";
                IUnit attackedUnit = Attecker.Fight(Defender);
                if (attackedUnit == null)
                    StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nАрмия {secondArmy.ArmyName} АТТАКОВАНА! Пострадал {Defender.GetInfo()}!\n";
                else
                {
                    StepInfo += $"\n\n\t\t\t------ИТОГ------\n\nВ армии {secondArmy.ArmyName} СМЕРТЬ! Погиб {Defender.Name}!\n";
                    if (Subscribed == true)
                        StepInfo += $"\n\t\t\t{secondArmy.NotifyAboutDeath()}\n";
                    secondArmy.Remove(attackedUnit);
                }
            }
            if (EndGame())
                return;
        }
        private List<IUnit> GetFirstLine(IArmy army)
        {
            var firstLine = new List<IUnit>();
            var unitsInRow = Math.Min(GameStrategy.SizeOfRow, army.Count());
            for (int i = 0; i < unitsInRow; i++)
                firstLine.Add(army[army.Count() - 1 - i]);
            return firstLine;
        }
        private bool EndGame()
        {
            if (GameOver)
                return true;

            if (FirstArmy.IsEmpty() || SecondArmy.IsEmpty())
            {
                GameOver = true;
                StepInfo += "\n\n\t\t\t------ИГРА ОКОНЧЕНА------\n\n";
                if (FirstArmy.IsEmpty())
                    StepInfo += $"ПОБЕДИЛА вторая армия {SecondArmy.ArmyName}! \n";
                else
                    StepInfo += $"ПОБЕДИЛА первая армия {FirstArmy.ArmyName}! \n";
                return true;
            }
            return false;
        }
    }
}

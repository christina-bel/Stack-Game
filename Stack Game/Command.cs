using System;
using System.Collections.Generic;

namespace Stack_Game
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }

    class OneStepCommand : ICommand
    {
        Battle battle;
        IArmy firstArmyBefore;
        IArmy secondArmyBefore;
        IArmy firstArmyAfter;
        IArmy secondArmyAfter;

        public OneStepCommand(Battle newBattle)
        {
            battle = newBattle;
        }

        public void Undo()
        {
            battle.FirstArmy = firstArmyBefore;
            battle.SecondArmy = secondArmyBefore;
        }

        public void Execute()
        {
            firstArmyBefore = battle.FirstArmy.ArmyDeepCopy();
            secondArmyBefore = battle.SecondArmy.ArmyDeepCopy();
            battle.Step();
            firstArmyAfter = battle.FirstArmy.ArmyDeepCopy();
            secondArmyAfter = battle.SecondArmy.ArmyDeepCopy();
        }

        public void Redo()
        {
            battle.FirstArmy = firstArmyAfter;
            battle.SecondArmy = secondArmyAfter;
        }
    }

    class PlayTillEndCommand : ICommand
    {
        Battle battle;
        IArmy firstArmyBefore;
        IArmy secondArmyBefore;
        IArmy firstArmyAfter;
        IArmy secondArmyAfter;
        public PlayTillEndCommand(Battle newBattle)
        {
            battle = newBattle;
        }

        public void Execute()
        {
            firstArmyBefore = battle.FirstArmy.ArmyDeepCopy();
            secondArmyBefore = battle.SecondArmy.ArmyDeepCopy();
            battle.PlayTillEnd();
            firstArmyAfter = battle.FirstArmy.ArmyDeepCopy();
            secondArmyAfter = battle.SecondArmy.ArmyDeepCopy();
        }

        public void Undo()
        {
            battle.FirstArmy = firstArmyBefore;
            battle.SecondArmy = secondArmyBefore;
        }

        public void Redo()
        {
            battle.FirstArmy = firstArmyAfter;
            battle.SecondArmy = secondArmyAfter;
        }
    }

    class CommandManager
    {
        Battle battle;
        private Stack<ICommand> UndoStack = new Stack<ICommand>();
        private Stack<ICommand> RedoStack = new Stack<ICommand>();
        public CommandManager(Battle newBattle)
        {
            battle = newBattle;
        }

        public void Step()
        {
            Invoke(new OneStepCommand(battle));
        }

        public void PlayTillEnd()
        {
            Invoke(new PlayTillEndCommand(battle));
        }
        public void Undo(out bool pos)
        {
            if (UndoStack.Count != 0 && !battle.GameOver)
            {
                ICommand command = UndoStack.Pop();
                command.Undo();
                RedoStack.Push(command);
                pos = true;
            }
            else
            {
                pos = false;
            }
        }
        public void Redo(out bool pos)
        {
            if (RedoStack.Count != 0)
            {
                ICommand command = RedoStack.Pop();
                command.Redo();
                UndoStack.Push(command);
                pos = true;
            }
            else
            {
                pos = false;
            }
        }
        private void Invoke(ICommand newCommand)
        {
            newCommand.Execute();
            UndoStack.Push(newCommand);
            RedoStack.Clear();
        }

    }
}

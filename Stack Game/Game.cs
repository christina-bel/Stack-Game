﻿using System;
using System.IO;

namespace Stack_Game
{
    class Game
    {
        int strategy;
        IArmy firstArmy, secondArmy;
        IStrategy strt;
        Battle battle;
        bool possible = false;
        CommandManager commandManager;
        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t##############################");
            Console.WriteLine("\t\t\t\t\t#                            #");
            Console.WriteLine("\t\t\t\t\t#                            #");
            Console.WriteLine("\t\t\t\t\t#       STACKWAR GAME!!!     #");
            Console.WriteLine("\t\t\t\t\t#                            #");
            Console.WriteLine("\t\t\t\t\t#                            #");
            Console.WriteLine("\t\t\t\t\t##############################");
            Console.WriteLine();
            Console.WriteLine("Выберите стратегию игры:");
            Console.WriteLine("1. Один на один");
            Console.WriteLine("2. Три на три");
            Console.WriteLine("3. Стенка на стенку\n");
            while (!int.TryParse(Console.ReadLine(), out strategy))
                Console.WriteLine("Необходимо ввести число от 1 до 3");
            Menu();
        }

        public void Menu() {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Создать армию");
            Console.WriteLine("2. Показать состав армии");
            Console.WriteLine("3. Сделать ход");
            Console.WriteLine("4. Отменить ход");
            Console.WriteLine("5. Вернуть ход обратно");
            Console.WriteLine("6. Играть до конца");
            Console.WriteLine("7. Уведомлять о гибели бойцов");
            Console.WriteLine("8. Отменить уведомления о гибели бойцов");
            Console.WriteLine();
            var answ = Console.ReadLine();
            Console.WriteLine();
            switch (answ)
            {
                case "1":
                    var armyFactory = new ArmyFactory();
                    Console.WriteLine("Введите сумму, на которую необходимо создать первую армию:");
                    int price;
                    while (!int.TryParse(Console.ReadLine(), out price))
                        Console.WriteLine("Необходимо ввести целочисленоое значение");
                    firstArmy = armyFactory.CreateArmy(price, "ЛАННИСТЕРЫ");
                    Console.WriteLine("\nВведите сумму, на которую необходимо создать вторую армию:");
                    while (!int.TryParse(Console.ReadLine(), out price))
                        Console.WriteLine("Необходимо ввести целочисленоое значение");
                    secondArmy = armyFactory.CreateArmy(price, "СТАРКИ");
                    if (strategy == 1)
                        strt = new OneToOneStrategy();
                    else if (strategy == 2)
                        strt = new ThreeToThreeStrategy();
                   else
                    {
                      strt = new NToNStrategy(firstArmy, secondArmy);
                    }
                    battle = new Battle(firstArmy, secondArmy, strt);
                    commandManager = new CommandManager(battle);
                    Logger($"\n\n\t\t\t------Армии СОЗДАНЫ------\n\n{battle.ArmyInfo()}");
                    Console.ReadLine();
                    Menu();
                    break;

                case "2":
                    Console.WriteLine();
                    Console.WriteLine(battle.ArmyInfo());
                    Console.ReadLine();
                    Menu();
                    break;
                
                case "3":
                    commandManager.Step();
                    Logger(battle.StepInfo);
                    if (!battle.GameOver) 
                        Console.ReadLine();
                    Menu();
                    break;

                case "4":
                    commandManager.Undo(out possible);
                    if (possible)
                        Logger("Отмена предыдущего хода");
                    else
                        Console.WriteLine("\nОтмена хода не может быть выполнена\n");
                    Console.ReadLine();
                    Menu();
                    break;

                case "5":
                    commandManager.Redo(out possible);
                    if (possible)
                        Logger("Ход снова выполнен");
                    else
                        Console.WriteLine("\nВозврат хода не может быть выполнен\n");
                    Console.ReadLine();
                    Menu();
                    break;

                case "6":
                    commandManager.PlayTillEnd();
                    Logger(battle.TillEndInfo);
                    Console.ReadLine();
                    Menu();
                    break;

                case "7":
                    battle.Subscribe();
                    Console.WriteLine("Подписка на уведомления успешно оформлена");
                    Console.ReadLine();
                    Menu();
                    break;

                case "8":
                    battle.UnSubscribe();
                    Console.WriteLine("Подписка на уведомления успешно отменена");
                    Console.ReadLine();
                    Menu();
                    break;


                default:
                    Console.WriteLine("Неверный ввод! Данного пункта не существует! Попробуйте еще раз!");
                    Console.ReadLine();
                    Menu();
                    break;
            }
        }

        public void newLog()
        {
            File.WriteAllText("STACKWAR.txt", "");
        }
        private void Logger(string txt)
        {
            Console.WriteLine(txt);
            using (StreamWriter sw = new StreamWriter("STACKWAR.txt", true))
            {
                sw.WriteLine(txt);
            }
        }
    }
}
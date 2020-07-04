using System;
using System.Collections.Generic;
using System.Text;

namespace Stack_Game
{
    public static class DefaultSettings
    {
        public static class Infantry
        {
            public static readonly int Health = 100;
            public static readonly int Price = 15;
            public static readonly int Attack = 35;
            public static readonly int Defence = 30;
            public static readonly string Name = "Пехотинец";
            public static readonly int Range = 1;
        }

        public static class Knight
        {
            public static readonly int Health = 100;
            public static readonly int Price = 60;
            public static readonly int Attack = 65;
            public static readonly int Defence = 80;
            public static readonly string Name = "Рыцарь";
        }

        public static class Archer
        {
            public static readonly int Health = 100;
            public static readonly int Price = 60;
            public static readonly int Attack = 40;
            public static readonly int Defence = 20;
            public static readonly string Name = "Лучник";
            public static readonly int Range = 5;
            public static readonly int Strength = 65;
        }

        public static class Healer
        {
            public static readonly int Health = 100;
            public static readonly int Price = 80;
            public static readonly int Attack = 30;
            public static readonly int Defence = 30;
            public static readonly string Name = "Доктор";
            public static readonly int Range = 1;
            public static readonly int Strength = 50;
        }

        public static class Wizard
        {
            public static readonly int Health = 100;
            public static readonly int Price = 90;
            public static readonly int Attack = 40;
            public static readonly int Defence = 40;
            public static readonly string Name = "Маг";
            public static readonly int Range = 1;
            public static readonly int Strength = 50;
        }
    }
}

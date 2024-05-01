using System;
using System.Collections.Generic;

namespace ContainerSystem
{
    class Table
    {
        public char[,] Grid { get; } // свойство двумерный массив 
        public Dictionary<char, Item> ItemsMap { get; } // словарик для того, чтобы смотреть
        // какие символы использовались

        // этот конструктор помогает создавать новые экземпляры класса Table 
        public Table(int width, int height)
        {
            Grid = new char[height, width]; // создаём двумерный массив символов Grid (по сути таблица)
            ItemsMap = new Dictionary<char, Item>(); //  отслеживаем соответствия между символами и объектами Item

            InitializeGrid(); // заполняем созданный двумерный массив Grid пробелами
        }
        
        // оч просто создаётся таблица:
        // int width = 5;
        // int height = 3;
        // Table table = new Table(width, height);

        private void InitializeGrid()
        {
            // проходимся по индексам [i] первого измерения двумерного массива Grid
            for (int i = 0; i < Grid.GetLength(0); i++) 
            {
                // проходимся по индексам [j] второго измерения массива Grid
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    // ставим пробел в каждую ячейку [i, j]
                    Grid[i, j] = ' ';
                }
            }
        }

        public void AddItem(Item item)
        {
            if (item.Width <= 0 || item.Height <= 0) // чтобы предметы с 0 шириной или 0 высотой не шли
            {
                Console.WriteLine("Размер итема должен быть больше 0. Итем " + item.Symbol + " не добавлен.");
                return;
            }

            if (!ItemsMap.ContainsKey(item.Symbol)) // не сущесствует ли уже Item с таким символом ???
            {
                // перебираем строки, соответствующие Y элемента Item
                for (int i = item.Y; i < item.Y + item.Height; i++)
                {
                    // перебираем столбцы, соответствующие X элемента Item
                    for (int j = item.X; j < item.X + item.Width; j++)
                    {
                        // элемент запихивается НЕ в пустую ячейку или ячейку ВНЕ таблицы???
                        if (i < 0 || i >= Grid.GetLength(0) || j < 0 || j >= Grid.GetLength(1) || Grid[i, j] != ' ')
                        {
                            Console.WriteLine("Не может положить итем " + item.Symbol + " в позицию (" + item.X + ", " + item.Y + ")");
                            return;
                        }
                    }
                }
                
                // проходимся по каждой строке, соответствующей Y Item
                for (int i = item.Y; i < item.Y + item.Height; i++)
                {
                    // заполняем ячейки внутри каждой строки, соответствующие X Item.
                    for (int j = item.X; j < item.X + item.Width; j++)
                    {
                        // на каждой итерации заполняем символом
                        Grid[i, j] = item.Symbol;
                    }
                }
                ItemsMap[item.Symbol] = item; // ключ - символ, значение - сам item
                PrintTable();  // то же, что и Console.WriteLine(table);
            }
            else
            {
                Console.WriteLine("Символ " + item.Symbol + " уже использован");
            }
        }

        private void PrintTable() // то же, что и Console.WriteLine(table);
        {
            // выводим номера столбцов
            Console.Write("   "); // обязательно 3 пробела, чтобы не было криво-косо
            for (int i = 0; i < Grid.GetLength(1); i++)
            {
                Console.Write(i % 10);
            }
            Console.WriteLine();
            
            // выводим таблицу с номерами строк и содержимым
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                Console.Write(i % 10 + "  ");
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
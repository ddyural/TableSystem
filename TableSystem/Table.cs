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

        /// <summary>
        /// добавляем новый предмет
        /// </summary>
        /// <param name="item">сам предмет</param>
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
                StickIn(item);
                ItemsMap[item.Symbol] = item; // ключ - символ, значение - сам item
                PrintTable();  // то же, что и Console.WriteLine(table);
            }
            else
            {
                Console.WriteLine("Символ " + item.Symbol + " уже использован");
            }
        }

        /// <summary>
        /// вывод таблицы в консоль
        /// </summary>
        private void PrintTable() 
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
        
        /// <summary>
        /// передвижение предмета
        /// </summary>
        /// <param name="symbol">ну ипа уникальный айди предмета</param>
        /// <param name="newX">новый Х</param>
        /// <param name="newY">новый Y</param>
        public void MoveItem(char symbol, int newX, int newY)
        {
            if (ItemsMap.ContainsKey(symbol))
            {
                Item item = ItemsMap[symbol];

                // проверяем, что новые координаты не выходят за границы таблицы
                if (newX >= 0 && newY >= 0 && newX + item.Width <= Grid.GetLength(1) && newY + item.Height <= Grid.GetLength(0))
                {
                    // удаляем старое положение предмета
                    StickOut(item);

                    // обновляем координаты предмета
                    item.Move(newX, newY);

                    // размещаем предмет в новом положении
                    StickIn(item);
                    
                    PrintTable(); // выводим таблицу после перемещения
                }
                else
                {
                    Console.WriteLine("Новые координаты выходят за границы таблицы");
                }
            }
            else
            {
                Console.WriteLine("Предмет с символом " + symbol + " не найден");
            }
        }

        /// <summary>
        /// удаляем предмет
        /// </summary>
        /// <param name="symbol">символ - по нему находим предмет</param>
        public void RemoveItem(char symbol)
        {
            Console.WriteLine("Текущая таблица:");
            if (ItemsMap.ContainsKey(symbol))
            {
                Item item = ItemsMap[symbol];

                // удаляем предмет из таблицы
                StickOut(item);

                // удаляем предмет из словаря
                ItemsMap.Remove(symbol);

                PrintTable(); // Выводим таблицу после удаления
            }
            else
            {
                Console.WriteLine("Предмет с символом " + symbol + " не найден");
            }
        }
        
        /// <summary>
        /// поворот итема
        /// </summary>
        /// <param name="symbol">символ - по нему находим предмет</param>
        public void RotateItem(char symbol)
        {
            if (ItemsMap.ContainsKey(symbol))
            {
                Item item = ItemsMap[symbol];
        
                // удалаяем предмет перед тем, как покрутить
                StickOut(item);
        
                // непосредственно сам поворотик
                item.Rotate();
        
                // располагаем предмет на сетке
                StickIn(item);

                Console.WriteLine("Итем " + symbol + " был повёрнут");
                PrintTable();
            }
            else
            {
                Console.WriteLine("Итем " + symbol + " не был найден");
            }
        }
        
        /// <summary>
        /// переразмер объекта
        /// </summary>
        /// <param name="symbol">id уникальный</param>
        /// <param name="newWidth">новая ширина</param>
        /// <param name="newHeight">новая высота</param>
        public void ResizeItem(char symbol, int newWidth, int newHeight)
        {
            if (ItemsMap.ContainsKey(symbol))
            {
                Item item = ItemsMap[symbol];
                
                StickOut(item);
                
                item.Resize(newWidth, newHeight);
                
                StickIn(item);

                PrintTable();
            }
            else
            {
                Console.WriteLine("Предмет с символом " + symbol + " не найден");
            }
        }
        
        /// <summary>
        /// просто снос всего
        /// </summary>
        public void ClearTable()
        {
            // проходимся по всем строкам таблицы (первое измерение массива Grid)
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                // проходимся по всем столбцам таблицы (второе измерение массива Grid)
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    // в каждую ячейку - пробел
                    Grid[i, j] = ' ';
                }
            }
            ItemsMap.Clear(); // очистка словаря элементов
            PrintTable(); // выводим таблицу после перемещения ... а показывать то нечего ))))
        }

        /// <summary>
        /// вытаскиваем из таблицы
        /// </summary>
        /// <param name="item">наш айтем</param>
        public void StickOut(Item item)
        {
            // проходимся по каждой строке, соответствующей Y Item
            for (int i = item.Y; i < item.Y + item.Height; i++)
            {
                // заполняем ячейки внутри каждой строки, соответствующие X Item.
                for (int j = item.X; j < item.X + item.Width; j++)
                {
                    // на каждой итерации заполняем пустотой ( а можем и смимволом )
                    Grid[i, j] = ' '; // = item.Symbol // = ' ';
                }
            }
        }

        /// <summary>
        /// засовываем в таблицу
        /// </summary>
        /// <param name="item">наш айтем, который решили взять</param>
        public void StickIn(Item item)
        {
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
        }
    }
}
﻿using System;
using System.Collections.Generic;

namespace ContainerSystem
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите ширину таблицы: ");
            int width = int.Parse(Console.ReadLine());
            
            Console.Write("Введите высоту таблицы: ");
            int height = int.Parse(Console.ReadLine());

            Table table = new Table(width, height);

            char symbol = 'A';

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить новый предмет");
                Console.WriteLine("2. Передвинуть предмет");
                Console.WriteLine("3. Удалить предмет");
                Console.WriteLine("4. Повернуть предмет");
                Console.WriteLine("5. Удалить всё вообще");
                Console.WriteLine("6. Переделать в список");
                Console.WriteLine("7. Изменить размер объекта");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    {
                        Console.WriteLine("Введите x, y, ширину, высоту для нового предмета:");
                        string itemInput = Console.ReadLine();
                        try
                        {
                            string[] parts = itemInput.Split(','); // когда вводим 5,6,1,2
                            int x = int.Parse(parts[0]); // 5 сюда (X)
                            int y = int.Parse(parts[1]); // 6 сюда (Y)
                            int itemWidth = int.Parse(parts[2]); // 1 сюда (Ширира)
                            int itemHeight = int.Parse(parts[3]); // 2 сюда (Высота)

                            table.AddItem(new Item(symbol, x, y, itemWidth, itemHeight)); // <- идут сюда 5,6,1,2
                            symbol = (char)(symbol + 1); // перемещаемся дальше по алфавиту
                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine(e);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }
                        
                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("Введите символ предмета для перемещения:");
                        try
                        {
                            char moveSymbol = char.Parse(Console.ReadLine());

                            if (table.ItemsMap.ContainsKey(moveSymbol))
                            {
                                Console.WriteLine("Введите новые координаты x, y для перемещения:");
                                string[] newPosStr = Console.ReadLine().Split(','); // когда вводим 7,8
                                int newX = int.Parse(newPosStr[0]); // 7 сюда (X)
                                int newY = int.Parse(newPosStr[1]); // 8 сюда (Y)

                                table.MoveItem(moveSymbol, newX, newY); // <- идут сюда 7,8
                            
                            }
                            else
                            {
                                Console.WriteLine("Предмет с таким символом не найден");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }
                        
                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine("Введите символ предмета для удаления:");
                        try
                        {
                            char deleteSymbol = char.Parse(Console.ReadLine());

                            if (table.ItemsMap.ContainsKey(deleteSymbol))
                            {
                                table.RemoveItem(deleteSymbol);
                            }
                            else
                            {
                                Console.WriteLine("Предмет с таким символом не найден");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }
                        
                        break;
                    }
                    case "4":
                    {
                        Console.WriteLine("Введите символ предмета для поворота:");
                        try
                        {
                            char rotateSymbol = char.Parse(Console.ReadLine());

                            if (table.ItemsMap.ContainsKey(rotateSymbol))
                            {
                                table.RotateItem(rotateSymbol);
                            }
                            else
                            {
                                Console.WriteLine("Предмет с таким символом не найден");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        
                        break;
                    }
                    case "5":
                    {
                        table.ClearTable(); // отвал башки
                        break;
                    }
                    case "6":
                    {
                        //  странно очень выглядит
                        
                        List<(char, int, int, int, int)> itemInfoList = new List<(char, int, int, int, int)>();
                        
                        foreach (var kvp in table.ItemsMap)
                        {
                            Item item = kvp.Value;
                            itemInfoList.Add((item.Symbol, item.X, item.Y, item.Width, item.Height));
                        }

                        Console.WriteLine("Список всех имеющихся предметов:");
                        
                        foreach (var itemInfo in itemInfoList)
                        {
                            Console.WriteLine($"Символ: {itemInfo.Item1}, X: {itemInfo.Item2}, Y: {itemInfo.Item3}, Ширина: {itemInfo.Item4}, Высота: {itemInfo.Item5}");
                        }
                        
                        break;
                    }
                    case "7":
                    {
                        Console.WriteLine("Введите id предмета для изменения размера:");
                        try
                        {
                            char resizeSymbol = char.Parse(Console.ReadLine());

                            if (table.ItemsMap.ContainsKey(resizeSymbol))
                            {
                                Console.WriteLine("Введите новую ширину и новую высоту:");
                                string[] newSizeStr = Console.ReadLine().Split(',');
                                int newWidth = int.Parse(newSizeStr[0]);
                                int newHeight = int.Parse(newSizeStr[1]);

                                table.ResizeItem(resizeSymbol, newWidth, newHeight);
                            }
                            else
                            {
                                Console.WriteLine("Предмет с таким символом не найден");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                    
                    default:
                    {
                        Console.WriteLine("Некорректный выбор. Попробуйте ещё раз");
                        break;
                    }
                }
            }
        }
    }
}

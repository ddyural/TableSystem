using System;

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
                Console.WriteLine("Введите x,y, ширину, высоту для предмета");
                Console.Write("(не вводите ничего, чтобы остановиться): ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) 
                {
                    break; // если пусто, то таблица готова
                }

                string[] parts = input.Split(','); // когда вводим 5,6,1,2
                int x = int.Parse(parts[0]); // 5 сюда (X)
                int y = int.Parse(parts[1]); // 6 сюда (Y)
                int itemWidth = int.Parse(parts[2]); // 1 сюда (Ширира)
                int itemHeight = int.Parse(parts[3]); // 2 сюда (Высота)

                table.AddItem(new Item(symbol, x, y, itemWidth, itemHeight)); // <- идут сюда 5,6,1,2

                symbol = (char)(symbol + 1); // перемещаемся дальше по алфавиту
            }

            Console.WriteLine("Конечная таблица:");
            Console.WriteLine(table); // то же, что и PrintTable()
            
        }
    }
}

namespace ContainerSystem
{
    class Item
    {
        public char Symbol { get; } // так отображается в таблице
        public int X { get; set; } // координата Х
        public int Y { get; set; } // координата Y
        public int Width { get; set; } // ширина
        public int Height { get; set; } // высота

        // этот конструктор помогает создавать новые экземпляры класса Item 
        public Item(char symbol, int x, int y, int width, int height)
        {
            Symbol = symbol;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        // перекидываем Item в новые координаты
        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        // смещение
        public void MoveBy(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
        
        // поворот оллееееооп
        public void Rotate()
        {
            (Width, Height) = (Height, Width);
        }
        
        // создание
        // Item item = new Item(symbol, x, y, itemWidth, itemHeight);
    }
}
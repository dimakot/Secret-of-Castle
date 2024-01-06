using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class ObjectRandomGeneration
    {
        Random rand = new Random(); //Рандом
        Canvas CanvasGame; //Канвас
        List<Image> objectlist = new List<Image>(); //Список для объектов
        Image player; //Игрок
        int ObjectCanvasTop, ObjectCanvasLeft; //Координаты для обьектов
        int SellectOBJ; //Выбор объекта

        public ObjectRandomGeneration(Canvas CanvasGame, List<Image> objectlist, Image player) //Конструктор
        {
            this.CanvasGame = CanvasGame;
            this.objectlist = objectlist;
            this.player = player;
        }
        public void Test() //Рандомная генерация
        {
            do
            {
                ObjectCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width)); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height));
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Image collisionobj = new Image();
            collisionobj.Tag = "objects";
            collisionobj.Source = new BitmapImage(new Uri("pack://application:,,,/castle_1.jpeg", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(collisionobj, ObjectCanvasLeft);
            Canvas.SetTop(collisionobj, ObjectCanvasTop);
            collisionobj.Height = 75; collisionobj.Width = 75;
            objectlist.Add(collisionobj);
            CanvasGame.Children.Add(collisionobj);
            Canvas.SetZIndex(player, 1);
        }
        public void XmasTree()
        {
            Image xmast = new Image();
            xmast.Tag = "objects";
            xmast.Source = new BitmapImage(new Uri("pack://application:,,,/Texture\\Objects\\Xmas\\Xmas_0.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width)); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height));
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(xmast, ObjectCanvasLeft);
            Canvas.SetTop(xmast, ObjectCanvasTop);
            xmast.Height = 300; xmast.Width = 150;
            objectlist.Add(xmast);
            CanvasGame.Children.Add(xmast);
            Canvas.SetZIndex(player, 1);
        }

        public void objectGeneration()
        {
            SellectOBJ = rand.Next(1, 3); //Случайное число для выбора объекта
            foreach (Image x in objectlist)
            {
                CanvasGame.Children.Remove(x);
            }
            objectlist.Clear();
            if (SellectOBJ == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Test();
                }
            }
            if (SellectOBJ == 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    XmasTree();
                }
            }
        }
    }
}

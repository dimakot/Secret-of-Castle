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
        public ObjectRandomGeneration(Canvas CanvasGame, List<Image> objectlist, Image player) //Конструктор
        {
            this.CanvasGame = CanvasGame;
            this.objectlist = objectlist;
            this.player = player;
        }
        public void Test() //Рандомная генерация
        {
            Image collisionobj = new Image();
            collisionobj.Tag = "objects";
            collisionobj.Source = new BitmapImage(new Uri("castle_1.jpeg", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(collisionobj, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(collisionobj, rand.Next(85, Convert.ToInt32(CanvasGame.Height)));
            collisionobj.Height = 75; collisionobj.Width = 75;
            objectlist.Add(collisionobj);
            CanvasGame.Children.Add(collisionobj);
            Canvas.SetZIndex(player, 1);
        }
        public void XmasTree()
        {
            Image xmast = new Image();
            xmast.Tag = "objects";
            xmast.Source = new BitmapImage(new Uri("Texture\\Objects\\Xmas\\Xmas_0.png", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(xmast, rand.Next(0, Convert.ToInt32(CanvasGame.Width - 500)));
            Canvas.SetTop(xmast, rand.Next(85, Convert.ToInt32(CanvasGame.Height - 500)));
            xmast.Height = 300; xmast.Width = 150;
            objectlist.Add(xmast);
            CanvasGame.Children.Add(xmast);
            Canvas.SetZIndex(player, 1);
        }
    }
}

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
        public void Net() //Рандомная генерация
        {
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и объектом меньше 200, объект не появится
            Image net = new Image(); //Создание объекта
            net.Tag = "objects"; //Тег для объекта
            net.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Furniture/Net.png", UriKind.RelativeOrAbsolute)); //Изображение объекта
            Canvas.SetLeft(net, ObjectCanvasLeft); //Установка координат
            Canvas.SetTop(net, ObjectCanvasTop);
            net.Height = 175; net.Width = 120; //Размеры
            objectlist.Add(net); //Добавление в список
            CanvasGame.Children.Add(net); //Добавление на канвас
            Canvas.SetZIndex(player, 1); //Установка слоя
        }
        public void XmasTree()
        {
            Image xmast = new Image();
            xmast.Tag = "objects";
            xmast.Source = new BitmapImage(new Uri("pack://application:,,,/Texture\\Objects\\Xmas\\Xmas_0.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(xmast, ObjectCanvasLeft);
            Canvas.SetTop(xmast, ObjectCanvasTop);
            xmast.Height = 300; xmast.Width = 150;
            objectlist.Add(xmast);
            CanvasGame.Children.Add(xmast);
            Canvas.SetZIndex(player, 1);
        }
        public void TorchLight()
        {
            Image TorchLight = new Image();
            TorchLight.Tag = "objects";
            TorchLight.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Furniture/Torch_red.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(TorchLight, ObjectCanvasLeft);
            Canvas.SetTop(TorchLight, ObjectCanvasTop);
            TorchLight.Height = 100; TorchLight.Width = 50;
            objectlist.Add(TorchLight);
            CanvasGame.Children.Add(TorchLight);
            Canvas.SetZIndex(player, 1);
        }
        public void TorchLightBlue()
        {
            Image TorchLightBlue = new Image();
            TorchLightBlue.Tag = "objects";
            TorchLightBlue.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Furniture/Torch_blue.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(TorchLightBlue, ObjectCanvasLeft);
            Canvas.SetTop(TorchLightBlue, ObjectCanvasTop);
            TorchLightBlue.Height = 100; TorchLightBlue.Width = 50;
            objectlist.Add(TorchLightBlue);
            CanvasGame.Children.Add(TorchLightBlue);
            Canvas.SetZIndex(player, 1);
        }
        public void Table()
        {
            Image Table = new Image();
            Table.Tag = "objects";
            Table.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Furniture/Table.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Table, ObjectCanvasLeft);
            Canvas.SetTop(Table, ObjectCanvasTop);
            Table.Height = 150; Table.Width = 150;
            objectlist.Add(Table);
            CanvasGame.Children.Add(Table);
            Canvas.SetZIndex(player, 1);
        }
        public void Chair()
        {
            Image Table = new Image();
            Table.Tag = "objects";
            Table.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Furniture/Table.png", UriKind.RelativeOrAbsolute));
            do
            {
                ObjectCanvasLeft = rand.Next(200, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ObjectCanvasTop = rand.Next(200, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ObjectCanvasLeft) < 200 && Math.Abs(Canvas.GetTop(player) - ObjectCanvasTop) < 200); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Table, ObjectCanvasLeft);
            Canvas.SetTop(Table, ObjectCanvasTop);
            Table.Height = 100; Table.Width = 50;
            objectlist.Add(Table);
            CanvasGame.Children.Add(Table);
            Canvas.SetZIndex(player, 1);
        }
        public void objectGeneration()
        {
            SellectOBJ = rand.Next(1, 11); //Случайное число для выбора объекта
            objectlist.Clear();
            if (SellectOBJ == 1) //Выбор объекта
            {
                for (int i = 0; i < 1; i++) //Количество объектов
                {
                    Net(); //Вызов метода
                }
            }
            if (SellectOBJ == 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    XmasTree();
                }
            }
            if (SellectOBJ == 3)
            {
                for (int i = 0; i < 1; i++)
                {
                    TorchLight();
                }
            }
            if (SellectOBJ == 4)
            {
                for (int i = 0; i < 1; i++)
                {
                    TorchLightBlue();
                }
            }
            if (SellectOBJ == 5)
            {
                for (int i = 0; i < 1; i++)
                {
                    Table();
                }
            }
            if (SellectOBJ == 6)
            {
                for (int i = 0; i < 1; i++)
                {
                    Chair();
                }
            }
            if (SellectOBJ == 7)
            {
                for (int i = 0; i < 2; i++)
                {
                    Chair();
                    Table();
                }
            }
            if (SellectOBJ == 8)
            {
                for (int i = 0; i < 2; i++)
                {
                    TorchLight();
                    XmasTree();
                }
            }
            if (SellectOBJ == 9)
            {
                for (int i = 0; i < 2; i++)
                {
                    Net();
                    Chair();
                }
            }
            if (SellectOBJ == 10)
            {
                for (int i = 0; i < 2; i++)
                {
                    TorchLight();
                    TorchLightBlue();
                }
            }
        }
    }
}

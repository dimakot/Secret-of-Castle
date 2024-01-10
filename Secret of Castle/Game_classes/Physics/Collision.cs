using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Secret_of_Castle
{
    class Collision
    {
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown; //Кнопки
        Image player; //Игрок
        public static int Speed = 7; //Скорость игрока
        public List<UIElement> elc; //Список объектов

        public Collision(Image player, List<UIElement> elc) //Конструктор
        {
            this.elc = elc;
            this.player = player;
        }
        public void Collision_physics() //Коллизия для игрока
        {
            foreach (UIElement u in elc) //Проверка на столкновение с объектами
            {
                if (u is Image collisionobj && (string)collisionobj.Tag == "objects") //Проверка на столкновение с объектами
                {
                    Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height); //Получаем координаты игрока
                    Rect rect6 = new Rect(Canvas.GetLeft(collisionobj), Canvas.GetTop(collisionobj), collisionobj.RenderSize.Width, collisionobj.RenderSize.Height); //Получаем координаты объекта
                    if (playerRect.IntersectsWith(rect6)) //Если игрок столкнулся с объектом
                    {
                        if (Player.UpKeyDown)  //Если игрок нажал кнопку вверх
                        {
                            UpKeyDown = false; //Обнуляем кнопки
                            Canvas.SetTop(player, Canvas.GetTop(player) + Speed); //Возвращаем игрока на прежнее место
                        }
                        if (Player.DownKeyDown) //Если игрок нажал кнопку вниз
                        {
                            DownKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                        }
                        if (Player.LeftKeyDown) //Если игрок нажал кнопку влево
                        {
                            LeftKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                        }
                        if (Player.RightKeyDown) //Если игрок нажал кнопку вправо
                        {
                            RightKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                        }
                    }
                }
            }
        }
    }
}

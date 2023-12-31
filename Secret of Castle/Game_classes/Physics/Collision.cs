using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Secret_of_Castle
{
    class Collision
    {
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Image player;
        public static int Speed = 7;
        public List<UIElement> elc;

        public Collision(Image player, List<UIElement> elc)
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
                    Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect6 = new Rect(Canvas.GetLeft(collisionobj), Canvas.GetTop(collisionobj), collisionobj.RenderSize.Width, collisionobj.RenderSize.Height);
                    if (playerRect.IntersectsWith(rect6))
                    {
                        if (Player.UpKeyDown) 
                        {
                            UpKeyDown = false; //Обнуляем кнопки
                            Canvas.SetTop(player, Canvas.GetTop(player) + Speed); //Возвращаем игрока на прежнее место
                        }
                        if (Player.DownKeyDown)
                        {
                            DownKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                        }
                        if (Player.LeftKeyDown)
                        {
                            LeftKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                        }
                        if (Player.RightKeyDown)
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

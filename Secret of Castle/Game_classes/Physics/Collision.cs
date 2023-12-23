using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Collision_class
{
    internal class Collision
    {
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Canvas CanvasGame;
        Image player;
        public static int Speed = 7;
        public List<UIElement> elc;

        public Collision(Canvas CanvasGame, Image player, List<UIElement> elc)
        {
            this.CanvasGame = CanvasGame;
            this.player = player;
            this.elc = elc;
        }
        public void Collision_physics(string dir)
        {
            foreach (UIElement u in elc)
            {
                if (u is Image obj && (string)obj.Tag == "objects")
                {
                    Rect CollisionPlayer = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                    Rect CollisionObj = new Rect(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if (CollisionPlayer.IntersectsWith(CollisionObj))
                    {
                        // Используем параметр dir для определения направления движения
                        switch (dir)
                        {
                            case "up":
                                // Блокируем только движение вверх
                                if (UpKeyDown)
                                {
                                    UpKeyDown = false;
                                    Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
                                }
                                break;
                            case "down":
                                // Блокируем только движение вниз
                                if (DownKeyDown)
                                {
                                    DownKeyDown = false;
                                    Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                                }
                                break;
                            case "left":
                                // Блокируем только движение влево
                                if (LeftKeyDown)
                                {
                                    LeftKeyDown = false;
                                    Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                                }
                                break;
                            case "right":
                                // Блокируем только движение вправо
                                if (RightKeyDown)
                                {
                                    RightKeyDown = false;
                                    Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                                }
                                break;
                            default:
                                // Выводим сообщение об ошибке, если параметр dir неизвестен
                                Console.WriteLine("Неверное значение параметра dir");
                                break;
                        }
                    }
                }

            }
        }

        /*        public void Collision_physics(string dir)
                {
                    foreach (UIElement u in elementsCopy)
                    {
                        if (u is Image obj && (string)obj.Tag == "objects") 
                        {
                            Rect CollisionPlayer = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                            Rect CollisionObj = new Rect(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                            if (CollisionPlayer.IntersectsWith(CollisionObj))
                            {
                                if (UpKeyDown)
                                {
                                    UpKeyDown = false;
                                    Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
                                }

                                if (DownKeyDown)
                                {
                                    DownKeyDown = false;
                                    Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                                }
                                if (LeftKeyDown)
                                {
                                    LeftKeyDown = false;
                                    Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                                }

                                if (RightKeyDown)
                                {
                                    RightKeyDown = false;
                                    Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                                }
                            }
                        }

                    }
                 }*/
    }
}

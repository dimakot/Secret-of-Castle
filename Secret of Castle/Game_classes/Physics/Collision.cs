using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Secret_of_Castle
{
    class Collision
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
        public void Collision_physics()
        {
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            foreach (UIElement u in elc)
            {
                if (u is Image collisionobj && (string)collisionobj.Tag == "objects" && u is Image ZombieMob && (string)ZombieMob.Tag == "Zombie")
                {
                    Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect6 = new Rect(Canvas.GetLeft(collisionobj), Canvas.GetTop(collisionobj), collisionobj.RenderSize.Width, collisionobj.RenderSize.Height);
                    Rect rect7 = new Rect(Canvas.GetLeft(ZombieMob), Canvas.GetTop(ZombieMob), ZombieMob.RenderSize.Width, ZombieMob.RenderSize.Height);
                    //Проверка на столкновение с объектами
                    if (playerRect.IntersectsWith(rect6))
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
                    if (playerRect.IntersectsWith(rect7))
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
        }
    }
}

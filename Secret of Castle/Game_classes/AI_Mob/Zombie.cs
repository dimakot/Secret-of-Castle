using Control_player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Secret_of_Castle.Game_classes;

namespace Secret_of_Castle.Game_classes.IO_Mob
{
    internal class Zombie
    {
        Image player;
        Random rand = new Random();
        Canvas CanvasGame;
        List<Image> zombiesList; //Список для моба
        ProgressBar health_bar;
        public List<UIElement> elc;
        public int Speed_Zombie;
        public Zombie(Image player, Canvas CanvasGame, List<Image> zombiesList, List<UIElement> elc, int Speed_Zombie = 2, ProgressBar hp_bar = null)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.zombiesList = zombiesList;
            this.elc = elc;
            this.Speed_Zombie = Speed_Zombie;
        }

        public void MobSpawn()
        {
            Image Zombies = new Image();
            Zombies.Tag = "Zombie";
            Zombies.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Height)));
            Zombies.Height = 200; Zombies.Width = 200;
            zombiesList.Add(Zombies);
            CanvasGame.Children.Add(Zombies);
            Canvas.SetZIndex(player, 1);
        }
        public void ZombieMovement()
        {
            foreach (UIElement w in elc)
            {
                if (w is Image ImgZomb && (string)ImgZomb.Tag == "Zombie")
                {
                    if (Canvas.GetLeft(ImgZomb) > Canvas.GetLeft(player)) //Движение зомби
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) - Speed_Zombie);
                    }

                    if (Canvas.GetLeft(ImgZomb) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) + Speed_Zombie);
                    }

                    if (Canvas.GetTop(ImgZomb) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) - Speed_Zombie);
                    }

                    if (Canvas.GetTop(ImgZomb) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) + Speed_Zombie);
                    }
                }
            }
        }
        public void GameLose()
        {
            player.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/zombie_right.png", UriKind.RelativeOrAbsolute));
            foreach (Image i in zombiesList)
            {
                            CanvasGame.Children.Remove(i);
            }

            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MobSpawn();
            }
        }

    }
}

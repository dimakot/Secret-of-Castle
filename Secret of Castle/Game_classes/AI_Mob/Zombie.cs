using Control_player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle.Game_classes.IO_Mob
{
    internal class Zombie
    {
        Image player;
        Image Zombies;
        Random rand = new Random();
        Canvas CanvasGame;
        List<Image> zombiesList = new List<Image>(); //Список для моба
        int Speed_Zombie = 2;
        public Zombie(Image player, Canvas CanvasGame, Image Zombies, List<Image> zombiesList)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.Zombies = Zombies;
            this.zombiesList = zombiesList;
        }

        public void MobSpawn()
        {
            Image Zombies = new Image();
            Zombies.Tag = "Zombie";
            Zombies.Source = new BitmapImage(new Uri("Icon.jpeg", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Height)));
            Zombies.Height = 200; Zombies.Width = 200;
            zombiesList.Add(Zombies);
            CanvasGame.Children.Add(Zombies);
            Canvas.SetZIndex(player, 1);
        }
        public void ZombieMovement()
        {
                if (Canvas.GetLeft(Zombies) > Canvas.GetLeft(player)) //Движение зомби
                {
                    Canvas.SetLeft(Zombies, Canvas.GetLeft(Zombies) - Speed_Zombie);
                }

                if (Canvas.GetLeft(Zombies) < Canvas.GetLeft(player))
                {
                    Canvas.SetLeft(Zombies, Canvas.GetLeft(Zombies) + Speed_Zombie);
                }

                if (Canvas.GetTop(Zombies) > Canvas.GetTop(player))
                {
                    Canvas.SetTop(Zombies, Canvas.GetTop(Zombies) - Speed_Zombie);
                }

                if (Canvas.GetTop(Zombies) < Canvas.GetTop(player))
                {
                    Canvas.SetTop(Zombies, Canvas.GetTop(Zombies) + Speed_Zombie);
                }
        }
        public void GameLose()
        {
            player.Source = new BitmapImage(new Uri("Player.png", UriKind.RelativeOrAbsolute));
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

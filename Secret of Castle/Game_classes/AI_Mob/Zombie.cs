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
using System.Windows.Media;

namespace Secret_of_Castle.Game_classes.IO_Mob
{
    internal class Zombie
    {
        Image player;
        Random rand = new Random();
        Canvas CanvasGame;
        List<Image> zombiesList; //Список для моба
        public List<UIElement> elc;
        public int Speed_Zombie;
        ProgressBar hp_bar;
        public Zombie(Image player, Canvas CanvasGame, List<Image> zombiesList, List<UIElement> elc, int Speed_Zombie = 2)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.zombiesList = zombiesList;
            this.elc = elc;
            this.Speed_Zombie = Speed_Zombie;
        }

        public void MobSpawn() //Создаем зомби
        {
            Image Zombies = new Image();
            Zombies.Tag = "Zombie";
            Zombies.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Width))); //Использует случайное значение от 0 до крайней точки разрешения экрана
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
                        ImgZomb.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_left.png", UriKind.Relative)); //Текстура зомби, идущего влево
                    }

                    if (Canvas.GetLeft(ImgZomb) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) + Speed_Zombie);
                    }

                    if (Canvas.GetTop(ImgZomb) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) - Speed_Zombie);
                        ImgZomb.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.Relative)); // Текстура зомби, идущего вправо
                    }

                    if (Canvas.GetTop(ImgZomb) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) + Speed_Zombie);
                    }
/*                    if (Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(ImgZomb) && Canvas.GetLeft(player) < Canvas.GetLeft(ImgZomb) + ImgZomb.ActualWidth && Canvas.GetTop(player) < Canvas.GetTop(ImgZomb) + ImgZomb.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(ImgZomb))
                    {
                        hp_bar.Value -= 0.5; //Если зомби прикосается к коллизии игрока, то из хп бара вычтется 1 хп
                        if (hp_bar.Value > 50)
                        {
                            hp_bar.Foreground = Brushes.Green; //если здоровья больше 50, то ProgressBar окрашен в зеленый 
                        }
                        else if (hp_bar.Value > 25)
                        {
                            hp_bar.Foreground = Brushes.Yellow; //если здоровья больше 25, то ProgressBar окрашен в желтый
                        }
                        else
                        {
                            hp_bar.Foreground = Brushes.Red; //в иных случаях ProgressBar красный 
                        }
                    }*/
                }
            }
        }
        public void GameLose()
        {
            player.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/zombie_right.png", UriKind.RelativeOrAbsolute));
            foreach (Image i in zombiesList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                            CanvasGame.Children.Remove(i);
            }

            zombiesList.Clear();

            for (int i = 0; i < 3; i++) //если мобов меньше чем 3, спавнится еще один
            {
                MobSpawn();
            }
        }

    }
}

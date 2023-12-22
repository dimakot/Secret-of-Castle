using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Control_player
{
    internal class Player
    {
        public static bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        public static int Speed = 7;
        public static int HealthPlayer = 100;
        Image player;
        Canvas CanvasGame;
        ProgressBar hp_bar;

        public Player(Image player, Canvas CanvasGame, ProgressBar hp_bar)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.hp_bar = hp_bar;
        }
        public void kbup(object sender, KeyEventArgs e) //Кнопка поднята
        {
            if (e.Key == Key.W)
            {
                UpKeyDown = false;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = false;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.A)
            {
                LeftKeyDown = false;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = false;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 7;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
        }
        public void kbdown(object sender, KeyEventArgs e) //Кнопка опущена
        {
            if (e.Key == Key.W)
            {
                UpKeyDown = true;
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = true;
            }
            if (e.Key == Key.A)
            {
                LeftKeyDown = true;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_left.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = true;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 12;
                player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_berserk.png", UriKind.RelativeOrAbsolute));
            }
        }
        public void Control()
        {
            if (LeftKeyDown == true && Canvas.GetLeft(player) > 0) //Движения игрока
            { 
                Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
            }

            if (RightKeyDown == true && Canvas.GetLeft(player) + player.Width < this.CanvasGame.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
            }

            if (UpKeyDown == true && Canvas.GetTop(player) > 85)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
            }

            if (DownKeyDown == true && Canvas.GetTop(player) + player.Height < this.CanvasGame.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
            }
            if (HealthPlayer > 1)
            {
                hp_bar.Value = HealthPlayer;
            }
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
        }
    }
}
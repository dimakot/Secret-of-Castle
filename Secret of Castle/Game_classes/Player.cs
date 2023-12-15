using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Control_player
{
    internal class Player
    {
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        public static int Speed = 10;
        Image player;
        Canvas CanvasGame;
        public Player(Image player, Canvas CanvasGame)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
        }
        public void kbup(object sender, KeyEventArgs e) //Кнопка поднята
        {
            if (e.Key == Key.W)
            {
                UpKeyDown = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyDown = false;
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = false;
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 7;
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
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = true;
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 12;
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
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        private DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        int Speed = 15;

        private void kbup(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W) {
                UpKeyDown = false;
            }
            if (e.Key == Key.S) {
                DownKeyDown = false;
            }
            if (e.Key == Key.A) {
                LeftKeyDown = false;
            }
            if (e.Key == Key.D) {
                RightKeyDown = false;
            }
        }
        private void kbdown(object sender, KeyEventArgs e)
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
        }
        public Game() {
            InitializeComponent(); //Таймер
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
        }
        private void Game1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Pause To_pause = new Pause(); //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e) {
            if (LeftKeyDown == true && Canvas.GetLeft(Player) > 0)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - Speed);
            }

            if (RightKeyDown == true && Canvas.GetLeft(Player) + Player.Width < this.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + Speed);
            }

            if (UpKeyDown == true && Canvas.GetTop(Player) > 0)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - Speed);
            }

            if (DownKeyDown == true && Canvas.GetTop(Player) + Player.Height < this.Height)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + Speed);
            }
        }
    }
 }
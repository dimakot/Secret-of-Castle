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

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        private DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        private float SpeedX, SpeedY, Friction = 0.75f, Speed = 1;
        public Game() {
            InitializeComponent();
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(1);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
        }
        private void Game1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Pause To_pause = new Pause(); //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
                To_pause.Show();
            }
        }
        private void kbup(object sender, KeyEventArgs e) {
            if (e.Key == Key.W) {
                UpKeyDown = true;
            }
            if (e.Key == Key.S) {
                DownKeyDown = true;
            }
            if (e.Key == Key.A) {
                LeftKeyDown = true;
            }
            if (e.Key == Key.D) {
                RightKeyDown = true;
            }
        }
        private void kbdown(object sender, KeyEventArgs e) {
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
        private void GameTickTimer(object sender, EventArgs e) {
            if (UpKeyDown) {
                SpeedY -= Speed; 
            }
            if (DownKeyDown)
            {
                SpeedY -= Speed;
            }
            if (LeftKeyDown)
            {
                SpeedX -= Speed;
            }
            if (RightKeyDown)
            {
                SpeedX += Speed;
            }
            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;
            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + SpeedX);
            Canvas.SetTop(Player, Canvas.GetTop(Player) - SpeedX);
        }
    }
}

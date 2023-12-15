using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;


namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        private DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        int Speed = 7;
        int Speed_Zombie = 2;
        Rectangle Zombie = new Rectangle();
        Random rand = new Random();

        private void kbup(object sender, KeyEventArgs e) { //Кнопка поднята
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
            if (e.Key == Key.LeftShift) {
                Speed = 7;
            }
        }
        private void kbdown(object sender, KeyEventArgs e) { //Кнопка опущена
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
            if (e.Key == Key.LeftShift)
            {
                Speed = 12;
            }
        }
        public Game() {
            InitializeComponent(); //Таймер
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(7);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
            Zombie.Width = 50; //Враг
            Zombie.Height = 50;
            Zombie.Fill = Brushes.Red;
            CanvasGame.Children.Add(Zombie); //враг на канвасе
            Canvas.SetLeft(Zombie, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(Zombie, rand.Next(80, Convert.ToInt32(CanvasGame.Height))); ;

        }
        private void Game1_KeyDown(object sender, KeyEventArgs e) { //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
            if (e.Key == Key.Escape) {
                Pause To_pause = new Pause(); 
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e) {
            if (LeftKeyDown == true && Canvas.GetLeft(Player) > 0) { //Движения игрока
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - Speed);
            }

            if (RightKeyDown == true && Canvas.GetLeft(Player) + Player.Width < this.ActualWidth) {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + Speed);
            }

            if (UpKeyDown == true && Canvas.GetTop(Player) > 85) {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - Speed);
            }

            if (DownKeyDown == true && Canvas.GetTop(Player) + Player.Height < this.ActualHeight) {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + Speed);
            }
                if (Canvas.GetLeft(Zombie) > Canvas.GetLeft(Player)) //Движение зомби
                {
                    Canvas.SetLeft(Zombie, Canvas.GetLeft(Zombie) - Speed_Zombie);
                }

                if (Canvas.GetLeft(Zombie) < Canvas.GetLeft(Player))
                {
                    Canvas.SetLeft(Zombie, Canvas.GetLeft(Zombie) + Speed_Zombie);
                }

                if (Canvas.GetTop(Zombie) > Canvas.GetTop(Player))
                {
                    Canvas.SetTop(Zombie, Canvas.GetTop(Zombie) - Speed_Zombie);
                }

                if (Canvas.GetTop(Zombie) < Canvas.GetTop(Player))
                {
                    Canvas.SetTop(Zombie, Canvas.GetTop(Zombie) + Speed_Zombie);
                }
                if (Canvas.GetLeft(Player) + Player.ActualWidth > Canvas.GetLeft(Zombie) && Canvas.GetLeft(Player) < Canvas.GetLeft(Zombie) + Zombie.ActualWidth && Canvas.GetTop(Player) < Canvas.GetTop(Zombie) + Zombie.ActualHeight && Canvas.GetTop(Player) + Player.ActualHeight > Canvas.GetTop(Zombie))
            {
                hp_bar.Value -= 1; //Если зомби прикосается к коллизии игрока, то из хп бара вычется 1 хп
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
 }
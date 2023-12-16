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
using Control_player;
using Collision_class;
using Secret_of_Castle.Game_classes.IO_Mob;


namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        private DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Player Player_Controller;
        Collision col_obj;
        int Speed = 7;
        Random rand = new Random();
        List<Image> zombiesList = new List<Image>();
        Zombie zombieai;

        private void kbup(object sender, KeyEventArgs e) {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e) {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
        }
        public Game() {
            InitializeComponent(); //Таймер
            Player_Controller = new Player(player, CanvasGame);
            zombieai = new Zombie(player, CanvasGame, zombiesList);
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
            zombieai.MobSpawn();
        }
        private void Game1_KeyDown(object sender, KeyEventArgs e) { //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
            if (e.Key == Key.Escape) {
                Pause To_pause = new Pause(); 
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e) {
            Player_Controller.Control(); //Движение игрока
            zombieai.ZombieMovement();
            /*if (Canvas.GetLeft(zombieai) > Canvas.GetLeft(player)) //Движение зомби
            {
                Canvas.SetLeft(Zombie, Canvas.GetLeft(Zombie) - Speed_Zombie);
            }

            if (Canvas.GetLeft(Zombie) < Canvas.GetLeft(player))
            {
                Canvas.SetLeft(Zombie, Canvas.GetLeft(Zombie) + Speed_Zombie);
            }

            if (Canvas.GetTop(Zombie) > Canvas.GetTop(player))
            {
                Canvas.SetTop(Zombie, Canvas.GetTop(Zombie) - Speed_Zombie);
            }

            if (Canvas.GetTop(Zombie) < Canvas.GetTop(player))
            {
                Canvas.SetTop(Zombie, Canvas.GetTop(Zombie) + Speed_Zombie);
            }
            if (Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Zombie) && Canvas.GetLeft(player) < Canvas.GetLeft(Zombie) + Zombie.ActualWidth && Canvas.GetTop(player) < Canvas.GetTop(Zombie) + Zombie.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Zombie))
            {
                hp_bar.Value -= 0.5; //Если зомби прикосается к коллизии игрока, то из хп бара вычется 1 хп
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
        private void GameLose()
        {
            zombieai.GameLose();
        }
    }
 }
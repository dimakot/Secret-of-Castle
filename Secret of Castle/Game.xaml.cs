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
using Secret_of_Castle.Game_classes;


namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Player Player_Controller;
        health_bar health_Bar_Controller;
        Collision col_obj;
        int Speed = 7;
        int Speed_Zombie = 2;
        Random rand = new Random();
        Zombie zombieai;
        List<Image> zombiesList = new List<Image>();

        private void kbup(object sender, KeyEventArgs e) {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e) {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
        }
        public Game() {
            InitializeComponent(); //Таймер
            List<UIElement> elc= CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, Speed_Zombie);
/*          health_Bar_Controller = new health_bar(player, Zombies, hp_bar);*/
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
            GameLose();
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
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            zombieai.elc = elc;
/*            health_Bar_Controller.damage();*/
        }
        private void GameLose()
        {
            zombieai.GameLose();
        }
    }
 }
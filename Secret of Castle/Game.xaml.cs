using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Media.Animation;
using Control_player;
using Secret_of_Castle.Game_classes.IO_Mob;
using Secret_of_Castle.Game_classes;
using Secret_of_Castle.Level;


namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        DispatcherTimer gametimer = new DispatcherTimer();
        Player Player_Controller;
        int Speed_Zombie = 2;
        Random rand = new Random();
        Zombie zombieai;
        List<Image> zombiesList = new List<Image>();
        List<Image> objectlist = new List<Image>();

        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        public static int Speed = 7;
        private string dir;

        private void kbup(object sender, KeyEventArgs e) {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e) {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
        }
        public Game() {
            InitializeComponent(); //Таймер
            List<UIElement> elc= CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, Speed_Zombie);
/*          health_Bar_Controller = new health_bar(player, Zombies, hp_bar);*/
            CanvasGame.Focus();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            GameLose();
    }
        private void Game1_KeyDown(object sender, KeyEventArgs e) { //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
            if (e.Key == Key.Escape) {
                Pause To_pause = new Pause(); 
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e)
        {
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller.Control(); //Движение игрока
            zombieai.ZombieMovement();
            zombieai.elc = elc;
            foreach (UIElement u in elc)
            {
                if (u is Image objc && (string)objc.Tag == "objects") // Коллизия
                {
                    Rect rect1 = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                    Rect rect2 = new Rect(Canvas.GetLeft(objc), Canvas.GetTop(objc), objc.Width, objc.Height);

                    if (rect1.IntersectsWith(rect2))
                    {
                        if (LeftKeyDown)
                        {
                            LeftKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                        }

                        if (RightKeyDown)
                        {
                            RightKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                        }

                        if (DownKeyDown)
                        {
                            DownKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                        }

                        if (UpKeyDown)
                        {
                            UpKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
                        }
                    }
                }
                if (Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
                {
                    level2 ChangeLevel = new level2();
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel.Show();
                }
            }
        }
        private void OBJGeneration() //Рандомная генерация
        {
            Image collisionobj = new Image();
            collisionobj.Tag = "objects";
            collisionobj.Source = new BitmapImage(new Uri("castle_1.jpeg", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(collisionobj, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(collisionobj, rand.Next(85, Convert.ToInt32(CanvasGame.Width)));
            collisionobj.Height = 75; collisionobj.Width = 75;
            objectlist.Add(collisionobj);
            CanvasGame.Children.Add(collisionobj);
            Canvas.SetZIndex(player, 1);
        }
        private void GameLose()
        {
            zombieai.GameLose();

            foreach (Image x in objectlist)
            {
                CanvasGame.Children.Remove(x);
            }
            objectlist.Clear();
            for (int i = 0; i < 3; i++)
            {
                OBJGeneration();
            }
        }
    }
}
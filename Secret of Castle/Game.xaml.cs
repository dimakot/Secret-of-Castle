using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
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
        List<object> zombiesList = new List<object>();
        List<Image> objectlist = new List<Image>();
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        public static int Speed = 7;
        private string dir;
        private string ControlWeapon = "Right";
        int HP_Zombie = 100;
        private void kbup(object sender, KeyEventArgs e) {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e) {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.E)
            {
                ShootMagicBasic(ControlWeapon);
            }
            if (e.Key == Key.Q)
            {
                SwordStroke();
            }
        }
        public Game() {
            InitializeComponent(); //Таймер
            List<UIElement> elc= CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer, ControlWeapon);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, Speed_Zombie);
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
                foreach (UIElement j in elc)
                {
                        if (j is Image BasicMagSphere && (string)BasicMagSphere.Tag == "BasicMagicSphere" && u is Image ZombieMob && (string)ZombieMob.Tag == "Zombie")
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(BasicMagSphere), Canvas.GetTop(BasicMagSphere), BasicMagSphere.RenderSize.Width, BasicMagSphere.RenderSize.Height);
                            Rect rect2 = new Rect(Canvas.GetLeft(ZombieMob), Canvas.GetTop(ZombieMob), ZombieMob.RenderSize.Width, ZombieMob.RenderSize.Height);
                            if (MagicSphere.IntersectsWith(rect2))
                            {
                            CanvasGame.Children.Remove(BasicMagSphere);
                                BasicMagSphere.Source = null;
                                HP_Zombie -= 30;
                                CanvasGame.Children.Remove(ZombieMob);
                                ZombieMob.Source = null;
                                zombiesList.Remove(ZombieMob);
                                zombieai.MobSpawn();
                            }
                        }
                    }
                Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                Rect portal = new Rect(Canvas.GetLeft(Portal), Canvas.GetTop(Portal), Portal.RenderSize.Width, Portal.RenderSize.Height);
                if (playerRect.IntersectsWith(portal))
                {
                    level1 ChangeLevel = new level1();
                    this.Hide(); // скрываем текущее окно
                    gametimer.Stop();
                    ChangeLevel.ShowDialog(); // показываем новое окно как диалоговое
                    this.Close(); // закрываем текущее окно после закрытия нового
                }
            }
        }
        private void OBJGeneration() //Рандомная генерация
        {
            Image collisionobj = new Image();
            collisionobj.Tag = "objects";
            collisionobj.Source = new BitmapImage(new Uri("castle_1.jpeg", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(collisionobj, rand.Next(0, Convert.ToInt32(CanvasGame.Width)));
            Canvas.SetTop(collisionobj, rand.Next(85, Convert.ToInt32(CanvasGame.Height)));
            collisionobj.Height = 75; collisionobj.Width = 75;
            objectlist.Add(collisionobj);
            CanvasGame.Children.Add(collisionobj);
            Canvas.SetZIndex(player, 1);
        }
        public void ShootMagicBasic(string Controlmagic)
        {
            Magic ShootBasicWeapon = new Magic();
            ShootBasicWeapon.ControlWeapon = Controlmagic;
            ShootBasicWeapon.MagicHorisontal = (int)(Canvas.GetLeft(player) + (player.Width / 2));
            ShootBasicWeapon.MagicVertical = (int)(Canvas.GetTop(player) + (player.Height / 2));
            ShootBasicWeapon.SphereMagicNew(CanvasGame);

        }
        public void SwordStroke()
        {
            Sword sword = new Sword();
            sword.playerVertical = (int)(Canvas.GetLeft(player) - (player.Width / 2));
            sword.playerHorizontal = (int)(Canvas.GetTop(player) - (player.Height / 2));
            sword.powerWaveCreator(CanvasGame);
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
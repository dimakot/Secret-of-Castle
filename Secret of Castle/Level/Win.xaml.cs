using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
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
using Weapon;

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Win.xaml
    /// </summary>
    public partial class Win : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        Random manna = new Random(); //рандом для маны

        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList(); //Список для элементов
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.E && Perks.star > 0 && Player.Lose == false && PauseCanvas.Visibility == Visibility.Hidden)
            {
                Perks.star--;
                ShootMagicBasic(Player.ControlWeapon);

                if (Perks.star < 1)
                {
                    GenerateStars();
                }
            }
            foreach (UIElement i in elc)
            {
                if (i is Image Chest && (string)Chest.Tag == "Chest")
                {
                    if (e.Key == Key.F && (Canvas.GetLeft(player) < Canvas.GetLeft(Chest) + Chest.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Chest) && Canvas.GetTop(player) < Canvas.GetTop(Chest) + Chest.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Chest)))
                    {
                        CanvasGame.Children.Remove(Chest);
                        Chest.Source = null;
                        Perks perk = new Perks();
                        perk.Perks_choose();
                        Player.UpKeyDown = false; //Обнуляем кнопки
                        Player.DownKeyDown = false;
                        Player.LeftKeyDown = false;
                        Player.RightKeyDown = false;
                    }
                }
            }
            if (e.Key == Key.Q) //Удар мечом
            {
                SwordStroke();
            }
            if (e.Key == Key.Escape)
            {
                PauseCanvas.Visibility = Visibility.Visible;
                gametimer.Stop();
                Canvas.SetZIndex(PauseCanvas, 1000);
            }
        }

        public void GenerateStars() // Появление маны
        {
            Image StarsManna = new Image();
            StarsManna.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Star.png", UriKind.RelativeOrAbsolute));
            StarsManna.Height = 80; StarsManna.Width = 80;
            int StarsCanvasTop, StarsCanvasLeft; //Координаты зомби
            do
            {
                StarsCanvasLeft = manna.Next(0, Convert.ToInt32(CanvasGame.Width) - 200);
                StarsCanvasTop = manna.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - StarsCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - StarsCanvasTop) < 800);
            Canvas.SetLeft(StarsManna, StarsCanvasLeft);
            Canvas.SetTop(StarsManna, StarsCanvasTop);
            StarsManna.Tag = "Manna";
            CanvasGame.Children.Add(StarsManna);
            Canvas.SetZIndex(player, 1);
            Canvas.SetZIndex(StarsManna, 1);
        }

        public void ChestGenerate()
        {
            Image Chest = new Image();
            Chest.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Chest.png", UriKind.RelativeOrAbsolute));
            Chest.Height = 100; Chest.Width = 100;
            int ChestCanvasTop, ChestCanvasLeft; // Координаты зомби
            do
            {
                ChestCanvasLeft = manna.Next(0, Convert.ToInt32(CanvasGame.Width) - 200);
                ChestCanvasTop = manna.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ChestCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - ChestCanvasTop) < 800);
            Canvas.SetLeft(Chest, ChestCanvasLeft);
            Canvas.SetTop(Chest, ChestCanvasTop);
            Chest.Tag = "Chest";
            CanvasGame.Children.Add(Chest);
            Canvas.SetZIndex(player, 1);
            Canvas.SetZIndex(Chest, 1);
        }

        public Win()
        {
            InitializeComponent(); //Таймер
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            GameLose();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = Perks.star;
            Player_Controller.Control(); //Движение игрока
            if ((Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal)))
            {
                WinCanvas.Visibility = Visibility.Visible;
                gametimer.Stop();
                Canvas.SetZIndex(WinCanvas, 1000);
            }
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            foreach (UIElement j in elc) //Проверяет элементы в списке
            {
                Collision collision = new Collision(player, elc); //Класс для коллизии
                collision.Collision_physics(); //Физика коллизии
                if (j is Image StarManna && (string)StarManna.Tag == "Manna") // Использование маны
                {
                    if (Canvas.GetLeft(player) < Canvas.GetLeft(StarManna) + StarManna.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(StarManna) && Canvas.GetTop(player) < Canvas.GetTop(StarManna) + StarManna.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(StarManna)) //Проверка на столкновение
                    {
                        CanvasGame.Children.Remove(StarManna); //Удаление маны
                        StarManna.Source = null; //Удаление картинки маны
                        Perks.star += 10; //Добавление маны
                    }
                }
            }
        }
        public void ShootMagicBasic(string Controlmagic) //Выстрел магией
        {
            Magic ShootBasicWeapon = new Magic();
            ShootBasicWeapon.ControlWeapon = Controlmagic;
            ShootBasicWeapon.MagicHorisontal = (int)(Canvas.GetLeft(player) + (player.Width / 2));
            ShootBasicWeapon.MagicVertical = (int)(Canvas.GetTop(player) + (player.Height / 2));
            ShootBasicWeapon.SphereMagicNew(CanvasGame);
        }
        public void SwordStroke() //Удар мечом
        {
            Sword sword = new Sword();
            sword.swordVertical = (int)(Canvas.GetLeft(player) - (player.Width / 2));
            sword.swordHorizontal = (int)(Canvas.GetTop(player) - (player.Height / 2));
            sword.WaveSwordCreator(CanvasGame, player);
        }
        public void ShootBowBasic(string ControlBow) //Выстрел магией
        {
            Bow ShootBowWeapon = new Bow();
            ShootBowWeapon.ControlWeapon = ControlBow;
            ShootBowWeapon.BowHorisontal = (int)(Canvas.GetLeft(player) + (player.Width / 2));
            ShootBowWeapon.BowVertical = (int)(Canvas.GetTop(player) + (player.Height / 2));
            ShootBowWeapon.BowNew(CanvasGame);
        }


        private void GameLose()
        {
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseCanvas.Visibility = Visibility.Hidden;
            gametimer.Start();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}

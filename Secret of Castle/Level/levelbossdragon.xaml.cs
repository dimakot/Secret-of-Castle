﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Weapon;
namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для levelbossdragon.xaml
    /// </summary>
    public partial class levelbossdragon : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Random manna = new Random(); //рандом для маны
        Player Player_Controller; //Класс игрока
        int zombieKilles = 0; //Количество убитых зомби
        int wizardKilles = 0; //Количество убитых магов
        Dragon dragon;
        Random rand = new Random(); //Рандом
        DarkWizard darkWizard; //Класс мага
        List<Image> DragonList = new List<Image>(); //Список для Дракона
        public static Window Menu_to;
        List<Image> objectlist = new List<Image>(); //Список для объектов
        ObjectRandomGeneration objectRandomGeneration; //Класс для генерации объектов
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
        public levelbossdragon()
        {
            InitializeComponent();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            dragon = new Dragon(player, CanvasGame, DragonList, elc, DragonHPBar);
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player);
            GameLose();
            gametimer.Tick += (sender, e) => { GameTickTimer(sender, e); }; //Таймер игры
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
        }
        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = Perks.star;
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller.Control(); //Движение игрока
            if (DragonList.Count == 0 && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                    Win ChangeLevel = new Win(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel.Show();
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    Player.Speed = 7;
                    Perks.Speed_boosting = 0;
            }
            dragon.DragonMove(); //AI Дракона
            dragon.elc = elc ; //Список для Дракона
            foreach (UIElement j in elc)
            {
                Collision collision = new Collision(player, elc);
                collision.Collision_physics();
            }
            if (Player.HealthPlayer < 1)
            {
                LoseCanvas.Visibility = Visibility.Visible;
                gametimer.Stop();
                Canvas.SetZIndex(LoseCanvas, 1500);
            }
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
        public void GenerateStars() // Появление маны
        {
            Image StarsManna = new Image();
            StarsManna.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Star.png", UriKind.RelativeOrAbsolute));
            StarsManna.Height = 80; StarsManna.Width = 80;
            int StarsCanvasTop, StarsCanvasLeft; //Координаты маны
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
            dragon.DragonLose();
            objectRandomGeneration.objectGeneration();
        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            PauseCanvas.Visibility = Visibility.Hidden;
            gametimer.Start();
            CanvasGame.Focus();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}

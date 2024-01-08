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
    /// Логика взаимодействия для level1.xaml
    /// </summary>
    public partial class level1 : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        int zombieKilles = 0; //Количество убитых зомби
        int wizardKilles = 0; //Количество убитых магов
        Random rand = new Random(); //Рандом
        /*        Zombie zombieai; //Класс зомби*/
        DarkWizard darkWizard; //Класс мага
        List<Image> zombiesList = new List<Image>(); //Список для моба
        List<Image> wizardList = new List<Image>(); //Список для мага
        List<Image> objectlist = new List<Image>(); //Список для объектов        int gameTimerTick = 0;
        ObjectRandomGeneration objectRandomGeneration; //Класс для генерации объектов
        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.E)
            {
                //Случайное число для выбора оружия
                int random = rand.Next(1, 2);
                if (random == 1) //Если 1, то стреляет магией
                {
                    ShootMagicBasic(Player.ControlWeapon);
                }
                if (random == 2) //Если 2, то стреляет луком
                {
                    ShootBowBasic(Player.ControlWeapon);
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
                Canvas.SetZIndex(PauseCanvas, 1);
            }
        }
        public level1()
        {
            InitializeComponent(); //Таймер
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            darkWizard = new DarkWizard(player, CanvasGame, wizardList, elc, wizardKilles);
            /*            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, zombieKilles);*/
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player);
            GameLose();
            gametimer.Tick += (sender, e) => { GameTickTimer(sender, e); }; //Таймер игры
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller.Control(); //Движение игрока
            int prt1 = rand.Next(1, 3); //Случайное число для генерации объектов
            if (zombiesList.Count == 0 || wizardList.Count == 0 && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                if (prt1 == 1)
                {
                    level1 ChangeLevel = new level1(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel.Show();
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    DarkWizard.wizardKilles = 0;
                    prt1 = 0;
                }
                else if (prt1 == 2)
                {
                    Game ChangeLevel = new Game(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel.Show();
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    DarkWizard.wizardKilles = 0;
                    prt1 = 0;
                }
                /*                if (prt1 == 3)
                                {
                                    Game ChangeLevel = new Game(); //При входе в портал, происходит переход на другой уровень
                                    this.Hide();
                                    gametimer.Stop();
                                    ChangeLevel.Show();
                                    Player.UpKeyDown = false; //Обнуляем кнопки
                                        Player.DownKeyDown = false;
                                    Player.LeftKeyDown = false;
                                    Player.RightKeyDown = false;
                                }*/
            }
            darkWizard.WizardAI(); //ИИ мага
                darkWizard.elc = elc; //Список для мага
                /*          zombieai.ZombieMovement(); //Движение зомби
                            zombieai.elc = elc; //Список для зомби*/
                foreach (UIElement j in elc)
                {
                    Collision collision = new Collision(player, elc);
                    collision.Collision_physics();
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
            darkWizard.GameLose();
            /*            zombieai.GameLose();*/
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
    }
}

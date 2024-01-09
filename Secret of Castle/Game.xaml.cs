﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        int zombieKilles = 0; //Количество убитых зомби
        Random rand = new Random(); //Рандом
        Random manna = new Random (); //рандом для маны
        Zombie zombieai; //Класс зомби
        List<Image> zombiesList = new List<Image>(); //Список для моба
        List<Image> objectlist = new List<Image>(); //Список для объектов
        ObjectRandomGeneration objectRandomGeneration; //Класс для генерации объектов
        int star = 10;
        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.E)
            {
                if (e.Key == Key.E && star > 0 && Player.Lose == false)
                {
                    star--;
                    ShootMagicBasic(Player.ControlWeapon);


                    if (star < 1)
                    {
                        GenerateStars();
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
                Canvas.SetZIndex(PauseCanvas, 1);
            }
        }

        private void GenerateStars() // Появление маны
        {
            Image StarsManna = new Image();
            StarsManna.Source = new BitmapImage(new Uri("castle_1.jpeg", UriKind.RelativeOrAbsolute));
            StarsManna.Height = 50; StarsManna.Width = 50;
            Canvas.SetLeft(StarsManna, manna.Next(10, Convert.ToInt32(CanvasGame.Width - 200)));
            Canvas.SetTop(StarsManna, manna.Next(80, Convert.ToInt32(CanvasGame.Height - 200)));
            StarsManna.Tag = "Manna";
            CanvasGame.Children.Add(StarsManna);
            Canvas.SetZIndex(player, 1);
        }

        public Game()
        {
            InitializeComponent(); //Таймер
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, zombieKilles);
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player);
            GameLose();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = star;
            Player_Controller.Control(); //Движение игрока
            if (zombiesList.Count == 0 && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                int prt1 = rand.Next(1, 3); //Случайное число для выбора уровня
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
                    Zombie.zombieKilles = 0;
                    DarkWizard.wizardNeeded = 0;
                    Zombie.zombiesNeeded = 0;
                }
                if (prt1 == 2)
                {
                    Game ChangeLevel1 = new Game(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel1.Show();
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    Zombie.zombieKilles = 0;
                    DarkWizard.wizardNeeded = 0;
                    Zombie.zombiesNeeded = 0;
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
                                    Zombie.zombieKilles = 0;
                                }*/
            }
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            zombieai.ZombieMovement(); //Движение зомби
            zombieai.elc = elc; //Список для зомби
            foreach (UIElement j in elc)
            {
                Collision collision = new Collision(player, elc);
                collision.Collision_physics();
                    if (j is Image StarManna && (string)StarManna.Tag == "Manna") // Трата патронов
                    {
                        if (Canvas.GetLeft(player) < Canvas.GetLeft(StarManna) + StarManna.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(StarManna) && Canvas.GetTop(player) < Canvas.GetTop(StarManna) + StarManna.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(StarManna))
                        {
                            CanvasGame.Children.Remove(StarManna);
                            StarManna.Source = null;
                            star += 10;
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
            zombieai.GameLose();
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Secret_of_Castle.Level;
using Weapon;


namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        int Speed_Zombie = 2; //Скорость зомби
        int zombieKilles = 0; //Количество убитых зомби
        Random rand = new Random(); //Рандом
        Zombie zombieai; //Класс зомби
        Collision collision; //Класс коллизий
        List<object> zombiesList = new List<object>(); //Список для моба
        List<Image> objectlist = new List<Image>(); //Список для объектов
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown; //Кнопки
        public static int Speed = 7; //Скорость игрока
        public string ControlWeapon = "Right"; //Направление магии
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
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, Speed_Zombie, zombieKilles);
            Player_Controller.ControlWeapon = ControlWeapon; //Направление магии
            collision = new Collision(CanvasGame, player, elc);
            CanvasGame.Focus();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            GameLose();
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
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
                        if (j is Image BasicMagSphere && (string)BasicMagSphere.Tag == "BasicMagicSphere" && u is Image ZombieMobAttack && (string)ZombieMobAttack.Tag == "Zombie")
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(BasicMagSphere), Canvas.GetTop(BasicMagSphere), BasicMagSphere.RenderSize.Width, BasicMagSphere.RenderSize.Height);
                            Rect rect2 = new Rect(Canvas.GetLeft(ZombieMobAttack), Canvas.GetTop(ZombieMobAttack), ZombieMobAttack.RenderSize.Width, ZombieMobAttack.RenderSize.Height);
                            if (MagicSphere.IntersectsWith(rect2))
                            {
                            CanvasGame.Children.Remove(BasicMagSphere);
                                BasicMagSphere.Source = null;
                                CanvasGame.Children.Remove(ZombieMobAttack);
                                ZombieMobAttack.Source = null;
                                zombiesList.Remove(ZombieMobAttack);
                                zombieai.MobSpawn();
                                zombieKilles++;
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
                    UpKeyDown = false; //Обнуляем кнопки
                    DownKeyDown = false;
                    LeftKeyDown = false;
                    RightKeyDown = false;
                    ChangeLevel.ShowDialog(); // показываем новое окно как диалоговое
                    this.Close(); // закрываем текущее окно после закрытия нового
                }
/*                collision.elc = elc;
                collision.Collision_physics();*/
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
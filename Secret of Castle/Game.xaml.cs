using Secret_of_Castle.Level;
using System;
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
        int Speed_Zombie = 2; //Скорость зомби
        int zombieKilles = 0; //Количество убитых зомби
        Random rand = new Random(); //Рандом
        Zombie zombieai; //Класс зомби
        List<object> zombiesList = new List<object>(); //Список для моба
        List<Image> objectlist = new List<Image>(); //Список для объектов
        public static int Speed = 7; //Скорость игрока
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
                Pause To_pause = new Pause();
                To_pause.Show();
            }
        }
        public Game()
        {
            InitializeComponent(); //Таймер
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, Speed_Zombie, zombieKilles);
            GameLose();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            Player_Controller.Control(); //Движение игрока
            if (Portal.Visibility == Visibility.Visible && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                level1 ChangeLevel = new level1(); //При входе в портал, происходит переход на другой уровень 
                this.Hide();
                gametimer.Stop();
                ChangeLevel.Show();
                Player.UpKeyDown = false; //Обнуляем кнопки
                Player.DownKeyDown = false;
                Player.LeftKeyDown = false;
                Player.RightKeyDown = false;
            }
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            zombieai.ZombieMovement(); //Движение зомби
            zombieai.elc = elc; //Список для зомби
            foreach (UIElement u in elc) //Проверка на столкновение
            {
                foreach (UIElement j in elc)
                {
                    string currentDifficulty = difficult.Instance.CurrentDifficulty;
                    if (j is Image BasicMagSphere && (string)BasicMagSphere.Tag == "Damage" && u is Image ZombieMobAttack && (string)ZombieMobAttack.Tag == "Mob")
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
                            zombieKilles++;
                            if (currentDifficulty == "Hard") //В зависимости от сложности, спавнится определенное кол-во зомби
                            {
                                if (zombieKilles < 50)
                                {
                                    zombieai.MobSpawn();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (currentDifficulty == "Medium")
                            {
                                if (zombieKilles < 25)
                                {
                                    zombieai.MobSpawn();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (currentDifficulty == "Lite")
                            {
                                if (zombieKilles < 10)
                                {
                                    zombieai.MobSpawn();
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                Collision collision = new Collision(player, elc);
                collision.Collision_physics();
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
        private void CristmasTreeGeneration() //Рандомная генерация
        {
            Image xmast = new Image();
            xmast.Tag = "objects";
            List<string> XmasAnimation = new List<string>() { //Создаем лист из картинок для анимации при помощи таймера
                "Texture/Objects/Xmas/Xmas_0.png",
                "Texture/Objects/Xmas/Xmas_1.png",
                "Texture/Objects/Xmas/Xmas_2.png",
            }; int animationCurrentImage = 0;
            int counter = 0; //Счетчик
            gametimer.Tick += (sender, e) => //Таймер
            {
                counter++;
                if (counter >= 50) //Скорость анимации
                {
                    xmast.Source = new BitmapImage(new Uri(XmasAnimation[animationCurrentImage], UriKind.RelativeOrAbsolute)); //анимация воспроизводится со скоростью таймера
                    animationCurrentImage = (animationCurrentImage + 1) % XmasAnimation.Count;
                    counter = 0;
                }
            };
            Canvas.SetLeft(xmast, rand.Next(0, Convert.ToInt32(CanvasGame.Width - 500)));
            Canvas.SetTop(xmast, rand.Next(85, Convert.ToInt32(CanvasGame.Height - 500)));
            xmast.Height = 300; xmast.Width = 150;
            objectlist.Add(xmast);
            CanvasGame.Children.Add(xmast);
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

            foreach (Image x in objectlist)
            {
                CanvasGame.Children.Remove(x);
            }
            objectlist.Clear();
            for (int i = 0; i < 3; i++)
            {
                OBJGeneration();
            }
            for (int i = 0; i < 1; i++)
            {
                CristmasTreeGeneration();
            }
        }
    }
}
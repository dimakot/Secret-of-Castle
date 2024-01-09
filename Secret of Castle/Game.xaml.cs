using System;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        int zombieKilles = 0; //Количество убитых зомби
        int wizardKilles = 0; //Количество убитых магов
        int skeletonKilles = 0; //Количество убитых скелетов
        int DogKilles = 0; //Количество убитых собак
        Random rand = new Random(); //Рандом
        Random manna = new Random(); //рандом для маны
        Random mob = new Random(); //рандом для моба
        List<Image> wizardList = new List<Image>(); //Список для мага
        Zombie zombieai; //Класс зомби
        Skeleton skeleton; //Класс скелета
        DarkWizard darkWizard; //Класс мага
        Dog dog; //Класс собаки
        List<Image> zombiesList = new List<Image>(); //Список для моба
        List<Image> skeletonList = new List<Image>(); //Список для скелета
        List<Image> objectlist = new List<Image>(); //Список для объектов
        List<Image> DogList = new List<Image>(); //Список для собаки
        ObjectRandomGeneration objectRandomGeneration; //Класс для генерации объектов
        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.E || PauseCanvas.Visibility == Visibility)
            {
                if (e.Key == Key.E && Perks.star > 0 && Player.Lose == false)
                {
                    Perks.star--;
                    ShootMagicBasic(Player.ControlWeapon);

                    if (Perks.star < 1)
                    {
                        GenerateStars();
                    }
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
            StarsManna.Source = new BitmapImage(new Uri("pack://application:,,,/castle_1.jpeg", UriKind.RelativeOrAbsolute));
            StarsManna.Height = 50; StarsManna.Width = 50;
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

        public Game()
        {
            InitializeComponent(); //Таймер
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, zombieKilles);
            skeleton = new Skeleton(player, CanvasGame, skeletonList, elc, skeletonKilles);
            darkWizard = new DarkWizard(player, CanvasGame, wizardList, elc, wizardKilles);
            dog = new Dog(player, CanvasGame, DogList, elc, DogKilles);
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player);
            GameLose();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = Perks.star;
            Player_Controller.Control(); //Движение игрока
            if ((zombiesList.Count == 0 && wizardList.Count == 0) && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                int prt1 = rand.Next(1, 3); //Случайное число для выбора уровня
                if (RandomLevel.CurrentLevel == 5)
                {
                    levelmadscientist ChangeLevel = new levelmadscientist(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide();
                    gametimer.Stop();
                    ChangeLevel.Show();
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    Zombie.zombieKilles = 0;
                    DarkWizard.wizardKilles = 0;
                    DarkWizard.wizardNeeded = 0;
                    Skeleton.skeletonKilles = 0;
                    Zombie.zombiesNeeded = 0;
                    Zombie.zombiesNeeded = 0;
                    Player.Speed = 7;
                    Perks.Speed_boosting = 0;
                    RandomLevel.CurrentLevel++;
                }
                else if (prt1 == 1)
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
                    DarkWizard.wizardKilles = 0;
                    DarkWizard.wizardNeeded = 0;
                    Skeleton.skeletonNeeded = 0;
                    Skeleton.skeletonKilles = 0;
                    Zombie.zombiesNeeded = 0;
                    Player.Speed = 7;
                    Perks.Speed_boosting = 0;
                    RandomLevel.CurrentLevel++;
                }

                else if (prt1 == 2)
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
                    DarkWizard.wizardKilles = 0;
                    DarkWizard.wizardNeeded = 0;
                    Skeleton.skeletonNeeded = 0;
                    Skeleton.skeletonKilles = 0;
                    Zombie.zombiesNeeded = 0;
                    Player.Speed = 7;
                    Perks.Speed_boosting = 0;
                    RandomLevel.CurrentLevel++;
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
            darkWizard.WizardAI(); //ИИ мага
            darkWizard.elc = elc; //Список для мага
            skeleton.SkeletonMovement();
            skeleton.elc = elc;
            dog.DogMovement();
            dog.elc = elc;
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
                        Perks.star += 10;
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
            int RandomMob = mob.Next(1, 9);
            if (RandomMob == 1)
            {
                zombieai.GameLose();
                darkWizard.GameLose();
                skeleton.GameLose();
                dog.GameLose();
            }
            if (RandomMob == 2)
            {
                zombieai.GameLose();
                skeleton.GameLose();
            }
            if (RandomMob == 3)
            {
                darkWizard.GameLose();
            }
            if (RandomMob == 5)
            {
                zombieai.GameLose();
                skeleton.GameLose();
            }
            if (RandomMob == 6)
            {
                skeleton.GameLose();
            }
            if (RandomMob == 7)
            {
                skeleton.GameLose();
                darkWizard.GameLose();
            }
            if (RandomMob == 8)
            {
                dog.GameLose();
                skeleton.GameLose();
            }
            if (RandomMob == 9)
            {
                dog.GameLose();
                darkWizard.GameLose();
            }
            objectRandomGeneration.objectGeneration();
            ChestGenerate();
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
    }
}
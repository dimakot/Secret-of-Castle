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
    /// Логика взаимодействия для levelmadscientist.xaml
    /// </summary>
    public partial class levelmadscientist : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer(); //Таймер
        Player Player_Controller; //Класс игрока
        Random manna = new Random(); //рандом для маны
        int zombieKilles = 0; //Количество убитых зомби
        MadScientist madScientist;
        public static Window Menu_to;
        Random rand = new Random(); //Рандом
        List<Image> ScientistList = new List<Image>(); //Список для Безумного ученого
        /*        Zombie zombieai; //Класс зомби*/
        /*        DarkWizard darkWizard; //Класс мага*/
        /*        List<Image> zombiesList = new List<Image>(); //Список для моба*/

        List<Image> objectlist = new List<Image>(); //Список для объектов        int gameTimerTick = 0;
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
        public levelmadscientist()
        {
            InitializeComponent();
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer);
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            /*            darkWizard = new DarkWizard(player, CanvasGame, wizardList, elc, wizardKilles);*/
            /*            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, zombieKilles);*/
            madScientist = new MadScientist(player, CanvasGame, ScientistList, elc, ScientistHPBar);
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player);
            GameLose();
            gametimer.Tick += (sender, e) => { GameTickTimer(sender, e); }; //Таймер игры
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
            player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
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
        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = Perks.star;
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller.Control(); //Движение игрока
            if (ScientistList.Count == 0 && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                int prt1 = rand.Next(1, 6); //Случайное число для выбора уровня
                if (RandomLevel.CurrentLevel == 7)
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
                if (RandomLevel.CurrentLevel == 14)
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

                else if (prt1 == 3)
                {
                    level2 ChangeLevel1 = new level2(); //При входе в портал, происходит переход на другой уровень 
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
                else if (prt1 == 4)
                {
                    level4 ChangeLevel1 = new level4(); //При входе в портал, происходит переход на другой уровень 
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
            }
            madScientist.ScientistMove();
            madScientist.elc = elc;
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
            objectRandomGeneration.objectGeneration();
            madScientist.MadScientistLose();
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

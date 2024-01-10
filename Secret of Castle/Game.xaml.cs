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
        public static Window Menu_to;
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
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList(); //Список для элементов
            Player_Controller.kbdown(sender, e); //Кнопка опущена
                if (e.Key == Key.E && Perks.star > 0 && Player.Lose == false && PauseCanvas.Visibility == Visibility.Hidden) //Выстрел магией
                {
                    Perks.star--; //Уменьшаем количество маны
                    ShootMagicBasic(Player.ControlWeapon);

                    if (Perks.star < 1) //Если маны нет, то генерируем ее
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
                        CanvasGame.Children.Remove(Chest); //Удаление сундука
                        Chest.Source = null; //Удаление картинки сундука
                        Perks perk = new Perks(); //Класс для перков
                        perk.Perks_choose(); //Выбор перка
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
            if (e.Key == Key.Escape) //Пауза
            {
                PauseCanvas.Visibility = Visibility.Visible; //Пауза
                gametimer.Stop(); //Остановка таймера
                Canvas.SetZIndex(PauseCanvas, 1000); //Пауза на передний план
            }
        }

        public void GenerateStars() // Появление маны
        {
            Image StarsManna = new Image(); //Создание маны
            StarsManna.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Star.png", UriKind.RelativeOrAbsolute)); //Спрайт маны
            StarsManna.Height = 80; StarsManna.Width = 80; //Размер маны
            int StarsCanvasTop, StarsCanvasLeft; //Координаты маны
            do //Проверка на столкновение с игроком
            {
                StarsCanvasLeft = manna.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Рандомные координаты
                StarsCanvasTop = manna.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - StarsCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - StarsCanvasTop) < 800);
            Canvas.SetLeft(StarsManna, StarsCanvasLeft); //Установка координат
            Canvas.SetTop(StarsManna, StarsCanvasTop); //Установка координат
            StarsManna.Tag = "Manna"; //Тег маны
            CanvasGame.Children.Add(StarsManna); //Добавление маны
            Canvas.SetZIndex(player, 1); //Игрок на передний план
            Canvas.SetZIndex(StarsManna, 1); //Мана на передний план
        }

        public void ChestGenerate() //Генерация сундука
        {
            Image Chest = new Image(); //Создание сундука
            Chest.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Objects/Chest.png", UriKind.RelativeOrAbsolute)); //Спрайт сундука
            Chest.Height = 100; Chest.Width = 100; //Размер сундука
            int ChestCanvasTop, ChestCanvasLeft; // Координаты сундука
            do  //Проверка на столкновение с игроком
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
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList(); //Список для элементов
            Player_Controller = new Player(player, CanvasGame, hp_bar, gametimer); //Класс игрока
            zombieai = new Zombie(player, CanvasGame, zombiesList, elc, zombieKilles); //Класс зомби
            skeleton = new Skeleton(player, CanvasGame, skeletonList, elc, skeletonKilles); //Класс скелета
            darkWizard = new DarkWizard(player, CanvasGame, wizardList, elc, wizardKilles); //Класс мага
            dog = new Dog(player, CanvasGame, DogList, elc, DogKilles); //Класс собаки
            objectRandomGeneration = new ObjectRandomGeneration(CanvasGame, objectlist, player); //Класс для генерации объектов
            GameLose(); //Поражение
            gametimer.Tick += new EventHandler(GameTickTimer); //Таймер игры
            gametimer.Interval = TimeSpan.FromMilliseconds(10); //Интервал таймера
            gametimer.Start(); //Запуск таймера
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute)); //Спрайт игрока
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
        }

        private void GameTickTimer(object sender, EventArgs e) //Таймер игры
        {
            MannaLB.Content = Perks.star; //Количество маны
            Player_Controller.Control(); //Движение игрока
            if ((zombiesList.Count == 0 && wizardList.Count == 0 && skeletonList.Count == 0 && DogList.Count == 0) && Canvas.GetLeft(player) < Canvas.GetLeft(Portal) + Portal.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal) && Canvas.GetTop(player) < Canvas.GetTop(Portal) + Portal.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal))
            {
                int prt1 = rand.Next(1, 6); //Случайное число для выбора уровня
                if (RandomLevel.CurrentLevel == 7) //Если текущий уровень 7, то переходим на окно босса
                {
                    levelmadscientist ChangeLevel = new levelmadscientist(); //При входе в портал, происходит переход на другой уровень 
                    this.Hide(); //Скрываем текущее окно
                    gametimer.Stop(); //Останавливаем таймер
                    ChangeLevel.Show(); //Открываем окно босса
                    Player.UpKeyDown = false; //Обнуляем кнопки
                    Player.DownKeyDown = false;
                    Player.LeftKeyDown = false;
                    Player.RightKeyDown = false;
                    Zombie.zombieKilles = 0; //Обнуляем количество убитых зомби
                    DarkWizard.wizardKilles = 0; //Обнуляем количество убитых магов
                    DarkWizard.wizardNeeded = 0; //Обнуляем количество магов
                    Skeleton.skeletonKilles = 0; //Обнуляем количество убитых скелетов
                    Zombie.zombiesNeeded = 0; //Обнуляем количество зомби
                    Zombie.zombiesNeeded = 0; //Обнуляем количество зомби
                    Player.Speed = 7; //Возвращаем скорость игрока
                    Perks.Speed_boosting = 0; //Обнуляем скорость игрока
                    RandomLevel.CurrentLevel++; //Увеличиваем текущий уровень
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
            if ((zombiesList.Count == 0 && wizardList.Count == 0 && skeletonList.Count == 0 && DogList.Count == 0) && Canvas.GetLeft(player) < Canvas.GetLeft(Portal1) + Portal1.ActualWidth && Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Portal1) && Canvas.GetTop(player) < Canvas.GetTop(Portal1) + Portal1.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Portal1))
            {
                int prt2 = rand.Next(1, 6); //Случайное число для выбора уровня
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
                else if (prt2 == 1)
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

                else if (prt2 == 2)
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

                else if (prt2 == 3)
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
                else if (prt2 == 4)
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
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            zombieai.ZombieMovement(); //Движение зомби
            zombieai.elc = elc; //Список для зомби
            darkWizard.WizardAI(); //ИИ мага
            darkWizard.elc = elc; //Список для мага
            skeleton.SkeletonMovement(); //Движение скелета
            skeleton.elc = elc; //Список для скелета
            dog.DogMovement(); //Движение собаки
            dog.elc = elc; //Список для собаки
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
            if (Player.HealthPlayer < 1) //Если здоровья нет, то поражение
            {
                LoseCanvas.Visibility = Visibility.Visible; //Поражение
                gametimer.Stop(); //Остановка таймера
                Canvas.SetZIndex(LoseCanvas, 1500); //Поражение на передний план
            }
        }
        public void ShootMagicBasic(string Controlmagic) //Выстрел магией
        {
            Magic ShootBasicWeapon = new Magic(); //Класс для магии
            ShootBasicWeapon.ControlWeapon = Controlmagic; //Управление магией
            ShootBasicWeapon.MagicHorisontal = (int)(Canvas.GetLeft(player) + (player.Width / 2)); //Координаты магии
            ShootBasicWeapon.MagicVertical = (int)(Canvas.GetTop(player) + (player.Height / 2)); //Координаты магии
            ShootBasicWeapon.SphereMagicNew(CanvasGame); //Создание магии
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
            int RandomMob = mob.Next(1, 9); //Случайное число для выбора моба
            if (RandomMob == 1) //Если цифра 1
            {
                zombieai.GameLose(); //Добавляем зомби
                darkWizard.GameLose(); //Добавляем мага
                skeleton.GameLose(); //Добавляем скелета
                dog.GameLose(); //Добавляем собаку
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
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Shutdown();
        }
    }
}
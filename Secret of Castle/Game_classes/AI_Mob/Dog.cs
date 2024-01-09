using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class Dog
    {
        List<Image> DogList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public List<UIElement> elc;
        public static int Speed_Dog = 2;
        public ProgressBar DogHPBar;
        public static int DogKilles;
        public static int DogNeeded = 0;
        DateTime lastDamageTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delay = 1000; //Задержка
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public Dog(Image player, Canvas CanvasGame, List<Image> DogList, List<UIElement> elc, int DogKilles = 0) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.DogList = DogList;
            this.elc = elc;
        }

        public void DogSpawn() //Создаем зомби
        {
            Image Dogs = new Image();
            Dogs.Tag = "MobAggry";
            Dogs.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Dog/dog_Right.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для зомби
            int ZombieCanvasTop, ZombieCanvasLeft; //Координаты зомби
            do
            {
                ZombieCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ZombieCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - ZombieCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - ZombieCanvasTop) < 800); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Dogs, ZombieCanvasLeft);
            Canvas.SetTop(Dogs, ZombieCanvasTop);
            Dogs.Height = 200; Dogs.Width = 150;
            DogHPBar = new ProgressBar(); // Создаем ProgressBar для зомби
            DogHPBar.Width = 150; DogHPBar.Height = 10;
            DogHPBar.Value = 50; DogHPBar.Maximum = 50;
            Canvas.SetLeft(DogHPBar, ZombieCanvasLeft);
            Canvas.SetTop(DogHPBar, ZombieCanvasTop - DogHPBar.Height);
            CanvasGame.Children.Add(DogHPBar);
            DogList.Add(Dogs);
            CanvasGame.Children.Add(Dogs);
            HPbars.Add(Dogs, DogHPBar);
            Canvas.SetZIndex(player, 1);
        }
        public void DogMovement()
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            foreach (UIElement w in elc)
            {
                if (w is Image ImgDog && (string)ImgDog.Tag == "MobAggry")
                {
                    Rect rect1 = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect2 = new Rect(Canvas.GetLeft(ImgDog), Canvas.GetTop(ImgDog), ImgDog.RenderSize.Width, ImgDog.RenderSize.Height);
                    if (rect1.IntersectsWith(rect2))
                    {
                        DateTime currentTime = DateTime.Now;
                        double difference = (currentTime - lastDamageTime).TotalMilliseconds;

                        if (difference >= delay) //В зависимости от выбранной сложности, игрок получает определенное кол-во урона
                        {
                            if (currentDifficulty == "Hard")
                            {
                                Player.HealthPlayer -= 10;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Medium")
                            {
                                Player.HealthPlayer -= 5;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Lite")
                            {
                                Player.HealthPlayer -= 2;
                                lastDamageTime = currentTime;
                            }
                        }

                    }
                    if (Canvas.GetLeft(ImgDog) > Canvas.GetLeft(player)) //Движение зомби
                    {
                        Canvas.SetLeft(ImgDog, Canvas.GetLeft(ImgDog) - Speed_Dog);
                        ImgDog.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Dog/dog_Left.png", UriKind.RelativeOrAbsolute)); //Текстура зомби, идущего влево
                        if (HPbars.ContainsKey(ImgDog))
                        {
                            ProgressBar DogHPBar = HPbars[ImgDog];
                            Canvas.SetLeft(DogHPBar, Canvas.GetLeft(ImgDog));
                        }
                    }

                    if (Canvas.GetLeft(ImgDog) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgDog, Canvas.GetLeft(ImgDog) + Speed_Dog);
                        if (HPbars.ContainsKey(ImgDog))
                        {
                            ProgressBar DogHPBar = HPbars[ImgDog];
                            Canvas.SetLeft(DogHPBar, Canvas.GetLeft(ImgDog));
                        }
                    }

                    if (Canvas.GetTop(ImgDog) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgDog, Canvas.GetTop(ImgDog) - Speed_Dog);
                        ImgDog.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Dog/dog_Right.png", UriKind.RelativeOrAbsolute)); // Текстура зомби, идущего вправо
                        if (HPbars.ContainsKey(ImgDog))
                        {
                            ProgressBar DogHPBar = HPbars[ImgDog];
                            Canvas.SetTop(DogHPBar, Canvas.GetTop(ImgDog) - DogHPBar.Height);
                        }
                    }

                    if (Canvas.GetTop(ImgDog) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgDog, Canvas.GetTop(ImgDog) + Speed_Dog);
                        if (HPbars.ContainsKey(ImgDog))
                        {
                            ProgressBar DogHPBar = HPbars[ImgDog];
                            Canvas.SetTop(DogHPBar, Canvas.GetTop(ImgDog) - DogHPBar.Height);
                        }
                    }
                    foreach (UIElement j in elc)
                    {
                        if (j is Image WeaponDamage && ((string)WeaponDamage.Tag == "SwordAttack" || (string)WeaponDamage.Tag == "BasicMagicAttack" || (string)WeaponDamage.Tag == "BowArrow") && w is Image DogMobAttack && (string)DogMobAttack.Tag == "MobAggry") //При попадании в зомби, урон отнимается
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                            Rect ZombieDamage = new Rect(Canvas.GetLeft(DogMobAttack), Canvas.GetTop(DogMobAttack), DogMobAttack.RenderSize.Width, DogMobAttack.RenderSize.Height);
                            if (MagicSphere.IntersectsWith(ZombieDamage))
                            {
                                if (HPbars.ContainsKey(DogMobAttack)) // Проверяем, существует ли зомби в словаре HPbars
                                {
                                    ProgressBar DogHPBar = HPbars[DogMobAttack]; // Получаем ProgressBar для текущего зомби
                                    WeaponDamage.Source = null;
                                    CanvasGame.Children.Remove(WeaponDamage);
                                    if ((string)WeaponDamage.Tag == "SwordAttack")
                                    {
                                        DogHPBar.Value -= 10; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BowArrow")
                                    {
                                        DogHPBar.Value -= 15; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BasicMagicAttack")
                                    {
                                        DogHPBar.Value -= 25; //При попадании в зомби, урон отнимается
                                    }
                                }
                            }
                            if (HPbars.ContainsKey(ImgDog))
                            {
                                ProgressBar DogHPBar = HPbars[ImgDog];
                                if (DogHPBar.Value < 1)
                                {
                                    CanvasGame.Children.Remove(ImgDog); //При смерти зомби, он пропадает
                                    CanvasGame.Children.Remove(DogHPBar);
                                    DogList.Remove(ImgDog); // Удаление зомби из списка
                                    HPbars.Remove(ImgDog); // Удаление зомби из словаря
                                    DogKilles++;
                                    DogNeeded++;
                                    if (currentDifficulty == "Hard") //В зависимости от сложности, спавнится определенное кол-во зомби
                                    {
                                        if (DogKilles < 15)
                                        {
                                            DogSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentDifficulty == "Medium")
                                    {
                                        if (DogKilles < 10)
                                        {
                                            DogSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentDifficulty == "Lite")
                                    {
                                        if (DogKilles < 5)
                                        {
                                            DogSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void GameLose()
        {
            foreach (Image i in DogList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);

            }
            DogList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            if (currentDifficulty == "Hard")
            {
                for (int i = 0; i < 5; i++)
                {
                    DogSpawn();
                }
            }
            else if (currentDifficulty == "Medium")
            {
                for (int i = 0; i < 3; i++)
                {
                    DogSpawn();
                }
            }
            else if (currentDifficulty == "Lite")
            {
                for (int i = 0; i < 2; i++)
                {
                    DogSpawn();
                }
            }
        }

    }
}

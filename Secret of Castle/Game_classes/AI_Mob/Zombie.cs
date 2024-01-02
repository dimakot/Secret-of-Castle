using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class Zombie
    {
        List<Image> zombiesList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public List<UIElement> elc;
        public int Speed_Zombie;
        public ProgressBar zombieHPBar;
        public static int zombieHP = 100;
        public static int zombieKilles;
        DateTime lastDamageTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delay = 1000; //Задержка
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public Zombie(Image player, Canvas CanvasGame, List<Image> zombiesList, List<UIElement> elc, int Speed_Zombie = 2, int zombieKilles = 0) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.zombiesList = zombiesList;
            this.elc = elc;
            this.Speed_Zombie = Speed_Zombie;
        }

        public void MobSpawn() //Создаем зомби
        {
            Image Zombies = new Image();
            Zombies.Tag = "Mob";
            Zombies.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для зомби
            int ZombieCanvasTop, ZombieCanvasLeft; //Координаты зомби
            do {
                ZombieCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width)); //Использует случайное значение от 0 до крайней точки разрешения экрана
                ZombieCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height)); } while (Math.Abs(Canvas.GetLeft(player) - ZombieCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - ZombieCanvasTop) < 800); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Zombies, ZombieCanvasLeft);
            Canvas.SetTop(Zombies, ZombieCanvasTop);
            Zombies.Height = 200; Zombies.Width = 200;
            zombieHPBar = new ProgressBar(); // Создаем ProgressBar для зомби
            zombieHPBar.Width = 150;
            zombieHPBar.Height = 10;
            zombieHPBar.Value = 100; // Устанавливаем здоровье зомби
            zombieHPBar.Maximum = 100; // Устанавливаем максимальное значение ProgressBar
            // Размещаем ProgressBar над зомби
            Canvas.SetLeft(zombieHPBar, ZombieCanvasLeft);
            Canvas.SetTop(zombieHPBar, ZombieCanvasTop - zombieHPBar.Height);
            CanvasGame.Children.Add(zombieHPBar); // Добавляем ProgressBar на Canvas
            zombiesList.Add(Zombies); // Добавляем зомби в список
            CanvasGame.Children.Add(Zombies); // Добавляем зомби на Canvas
            HPbars.Add(Zombies, zombieHPBar);
            Canvas.SetZIndex(player, 1);
        }
        public void ZombieMovement()
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            foreach (UIElement w in elc)
            {
                if (w is Image ImgZomb && (string)ImgZomb.Tag == "Mob")
                {
                    Rect rect1 = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect2 = new Rect(Canvas.GetLeft(ImgZomb), Canvas.GetTop(ImgZomb), ImgZomb.RenderSize.Width, ImgZomb.RenderSize.Height);
                    if (rect1.IntersectsWith(rect2))
                    {
                        DateTime currentTime = DateTime.Now;
                        double difference = (currentTime - lastDamageTime).TotalMilliseconds;

                        if (difference >= delay) //В зависимости от выбранной сложности, игрок получает определенное кол-во урона
                        {
                            if (currentDifficulty == "Hard")
                            {
                                Player.HealthPlayer -= 25;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Medium")
                            {
                                Player.HealthPlayer -= 15;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Lite")
                            {
                                Player.HealthPlayer -= 4;
                                lastDamageTime = currentTime;
                            }
                        }

                    }
                    if (Canvas.GetLeft(ImgZomb) > Canvas.GetLeft(player)) //Движение зомби
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) - Speed_Zombie);
                        ImgZomb.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Zombie/zombie_left.png", UriKind.RelativeOrAbsolute)); //Текстура зомби, идущего влево
                        ProgressBar zombieHPBar = HPbars[ImgZomb];
                        Canvas.SetLeft(zombieHPBar, Canvas.GetLeft(ImgZomb));
                    }

                    if (Canvas.GetLeft(ImgZomb) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) + Speed_Zombie);
                        ProgressBar zombieHPBar = HPbars[ImgZomb];
                        Canvas.SetLeft(zombieHPBar, Canvas.GetLeft(ImgZomb));
                    }

                    if (Canvas.GetTop(ImgZomb) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) - Speed_Zombie);
                        ImgZomb.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.RelativeOrAbsolute)); // Текстура зомби, идущего вправо
                        ProgressBar zombieHPBar = HPbars[ImgZomb];
                        Canvas.SetTop(zombieHPBar, Canvas.GetTop(ImgZomb) - zombieHPBar.Height);
                    }

                    if (Canvas.GetTop(ImgZomb) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) + Speed_Zombie);
                        ProgressBar zombieHPBar = HPbars[ImgZomb];
                        Canvas.SetTop(zombieHPBar, Canvas.GetTop(ImgZomb) - zombieHPBar.Height);
                    }
                    foreach (UIElement j in elc)
                    {
                        if (j is Image WeaponDamage && (string)WeaponDamage.Tag == "Damage" && w is Image ZombieMobAttack && (string)ZombieMobAttack.Tag == "Mob")
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                            if (MagicSphere.IntersectsWith(rect2))
                            {
                                zombieHPBar.Value -= 25; //При попадании в зомби, урон отнимается
                                WeaponDamage.Source = null;
                                CanvasGame.Children.Remove(WeaponDamage);
                            }
                            if (HPbars.ContainsKey(ImgZomb))
                            {
                                ProgressBar zombieHPBar = HPbars[ImgZomb];
                                if (zombieHPBar.Value == 0)
                                {
                                    CanvasGame.Children.Remove(ImgZomb); //При смерти зомби, он пропадает
                                    CanvasGame.Children.Remove(zombieHPBar);
                                    ZombieMobAttack.Source = null;
                                    HPbars.Remove(ImgZomb);
                                    zombiesList.Remove(ImgZomb);
                                    zombieKilles++;
                                    if (currentDifficulty == "Hard") //В зависимости от сложности, спавнится определенное кол-во зомби
                                    {
                                        if (zombieKilles < 50)
                                        {
                                            MobSpawn();
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
                                            MobSpawn();
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
                                            MobSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentDifficulty == "Hard") //В зависимости от сложности, появляется портал после определенного кол-во побежденных зомби
                                    {
                                        if (zombieKilles > 50)
                                        {
                                            Portal.Visibility = Visibility.Visible;
                                        }
                                    }
                                    if (currentDifficulty == "Medium")
                                    {
                                        if (zombieKilles > 25)
                                        {
                                            Portal.Visibility = Visibility.Visible;
                                        }
                                    }
                                    if (currentDifficulty == "Lite")
                                    {
                                        if (zombieKilles > 10)
                                        {
                                            Portal.Visibility = Visibility.Visible;
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
            player.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/zombie_right.png", UriKind.Relative));
            foreach (Image i in zombiesList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);
  
            }
            zombiesList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            if (currentDifficulty == "Hard")
            {
                for (int i = 0; i < 5; i++)
                {
                    MobSpawn();
                }
            }
            else if (currentDifficulty == "Medium")
            {
                for (int i = 0; i < 3; i++)
                {
                    MobSpawn();
                }
            }
            else if (currentDifficulty == "Lite")
            {
                for (int i = 0; i < 2; i++)
                {
                    MobSpawn();
                }
            }
        }

    }
}

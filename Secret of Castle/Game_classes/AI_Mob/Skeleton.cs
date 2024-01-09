using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class Skeleton
    {
        List<Image> skeletonList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public List<UIElement> elc;
        public static int Speed_skeleton = 2;
        public ProgressBar skeletonHPBar;
        public static int skeletonKilles;
        public static int skeletonNeeded = 0;
        DateTime lastDamageTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delay = 1000; //Задержка
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public Skeleton(Image player, Canvas CanvasGame, List<Image> skeletonList, List<UIElement> elc, int skeletonKilles = 0) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.skeletonList = skeletonList;
            this.elc = elc;
        }
        public void SkeletonSpawn() //Создаем зомби
        {
            Image Skeletons = new Image();
            Skeletons.Tag = "Skeleton";
            Skeletons.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Skeleton/Skeleton_Right.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для зомби
            int SkeletonCanvasTop, SkeletonCanvasLeft; //Координаты зомби
            do
            {
                SkeletonCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                SkeletonCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - SkeletonCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - SkeletonCanvasTop) < 800); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Skeletons, SkeletonCanvasLeft);
            Canvas.SetTop(Skeletons, SkeletonCanvasTop);
            Skeletons.Height = 200; Skeletons.Width = 200;
            skeletonHPBar = new ProgressBar(); // Создаем ProgressBar для зомби
            skeletonHPBar.Width = 150; skeletonHPBar.Height = 10;
            skeletonHPBar.Value = 75; skeletonHPBar.Maximum = 75;
            Canvas.SetLeft(skeletonHPBar, SkeletonCanvasLeft);
            Canvas.SetTop(skeletonHPBar, SkeletonCanvasTop - skeletonHPBar.Height);
            CanvasGame.Children.Add(skeletonHPBar);
            skeletonList.Add(Skeletons);
            CanvasGame.Children.Add(Skeletons);
            HPbars.Add(Skeletons, skeletonHPBar);
            Canvas.SetZIndex(player, 1);
        }

        public void SkeletonMovement()
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            foreach (UIElement w in elc)
            {
                if (w is Image ImgSkeleton && (string)ImgSkeleton.Tag == "Skeleton")
                {
                    Rect rect1 = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect2 = new Rect(Canvas.GetLeft(ImgSkeleton), Canvas.GetTop(ImgSkeleton), ImgSkeleton.RenderSize.Width, ImgSkeleton.RenderSize.Height);
                    if (rect1.IntersectsWith(rect2))
                    {
                        DateTime currentTime = DateTime.Now;
                        double difference = (currentTime - lastDamageTime).TotalMilliseconds;

                        if (difference >= delay) //В зависимости от выбранной сложности, игрок получает определенное кол-во урона
                        {
                            if (currentDifficulty == "Hard")
                            {
                                Player.HealthPlayer -= 15;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Medium")
                            {
                                Player.HealthPlayer -= 10;
                                lastDamageTime = currentTime;
                            }
                            else if (currentDifficulty == "Lite")
                            {
                                Player.HealthPlayer -= 5;
                                lastDamageTime = currentTime;
                            }
                        }

                    }
                    if (Canvas.GetLeft(ImgSkeleton) > Canvas.GetLeft(player)) //Движение зомби
                    {
                        Canvas.SetLeft(ImgSkeleton, Canvas.GetLeft(ImgSkeleton) - Speed_skeleton);
                        ImgSkeleton.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Skeleton/Skeleton_Left.png", UriKind.RelativeOrAbsolute)); //Текстура зомби, идущего влево
                        if (HPbars.ContainsKey(ImgSkeleton))
                        {
                            ProgressBar skeletonHPBar = HPbars[ImgSkeleton];
                            Canvas.SetLeft(skeletonHPBar, Canvas.GetLeft(ImgSkeleton));
                        }
                    }

                    if (Canvas.GetLeft(ImgSkeleton) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgSkeleton, Canvas.GetLeft(ImgSkeleton) + Speed_skeleton);
                        if (HPbars.ContainsKey(ImgSkeleton))
                        {
                            ProgressBar skeletonHPBar = HPbars[ImgSkeleton];
                            Canvas.SetLeft(skeletonHPBar, Canvas.GetLeft(ImgSkeleton));
                        }
                    }

                    if (Canvas.GetTop(ImgSkeleton) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgSkeleton, Canvas.GetTop(ImgSkeleton) - Speed_skeleton);
                        ImgSkeleton.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Skeleton/Skeleton_Right.png", UriKind.RelativeOrAbsolute)); // Текстура зомби, идущего вправо
                        if (HPbars.ContainsKey(ImgSkeleton))
                        {
                            ProgressBar skeletonHPBar = HPbars[ImgSkeleton];
                            Canvas.SetTop(skeletonHPBar, Canvas.GetTop(ImgSkeleton) - skeletonHPBar.Height);
                        }
                    }

                    if (Canvas.GetTop(ImgSkeleton) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgSkeleton, Canvas.GetTop(ImgSkeleton) + Speed_skeleton);
                        if (HPbars.ContainsKey(ImgSkeleton))
                        {
                            ProgressBar skeletonHPBar = HPbars[ImgSkeleton];
                            Canvas.SetTop(skeletonHPBar, Canvas.GetTop(ImgSkeleton) - skeletonHPBar.Height);
                        }
                    }
                    foreach (UIElement j in elc)
                    {
                        if (j is Image WeaponDamage && ((string)WeaponDamage.Tag == "SwordAttack" || (string)WeaponDamage.Tag == "BasicMagicAttack" || (string)WeaponDamage.Tag == "BowArrow") && w is Image SkeletonMobAttack && (string)SkeletonMobAttack.Tag == "Skeleton") //При попадании в зомби, урон отнимается
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                            Rect ZombieDamage = new Rect(Canvas.GetLeft(SkeletonMobAttack), Canvas.GetTop(SkeletonMobAttack), SkeletonMobAttack.RenderSize.Width, SkeletonMobAttack.RenderSize.Height);
                            if (MagicSphere.IntersectsWith(ZombieDamage))
                            {
                                if (HPbars.ContainsKey(SkeletonMobAttack)) // Проверяем, существует ли зомби в словаре HPbars
                                {
                                    ProgressBar skeletonHPBar = HPbars[SkeletonMobAttack]; // Получаем ProgressBar для текущего зомби
                                    WeaponDamage.Source = null;
                                    CanvasGame.Children.Remove(WeaponDamage);
                                    if ((string)WeaponDamage.Tag == "SwordAttack")
                                    {
                                        skeletonHPBar.Value -= 10; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BowArrow")
                                    {
                                        skeletonHPBar.Value -= 15; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BasicMagicAttack")
                                    {
                                        skeletonHPBar.Value -= 25; //При попадании в зомби, урон отнимается
                                    }
                                }
                            }
                            if (HPbars.ContainsKey(ImgSkeleton))
                            {
                                ProgressBar skeletonHPBar = HPbars[ImgSkeleton];
                                if (skeletonHPBar.Value < 1)
                                {
                                    CanvasGame.Children.Remove(ImgSkeleton); //При смерти зомби, он пропадает
                                    CanvasGame.Children.Remove(skeletonHPBar);
                                    skeletonList.Remove(ImgSkeleton); // Удаление зомби из списка
                                    HPbars.Remove(ImgSkeleton); // Удаление зомби из словаря
                                    skeletonKilles++;
                                    skeletonNeeded++;
                                    if (currentDifficulty == "Hard") //В зависимости от сложности, спавнится определенное кол-во зомби
                                    {
                                        if (skeletonKilles < 30)
                                        {
                                            SkeletonSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentDifficulty == "Medium")
                                    {
                                        if (skeletonKilles < 15)
                                        {
                                            SkeletonSpawn();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentDifficulty == "Lite")
                                    {
                                        if (skeletonKilles < 7)
                                        {
                                            SkeletonSpawn();
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
            foreach (Image i in skeletonList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);

            }
            skeletonList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            if (currentDifficulty == "Hard")
            {
                for (int i = 0; i < 4; i++)
                {
                    SkeletonSpawn();
                }
            }
            else if (currentDifficulty == "Medium")
            {
                for (int i = 0; i < 3; i++)
                {
                    SkeletonSpawn();
                }
            }
            else if (currentDifficulty == "Lite")
            {
                for (int i = 0; i < 2; i++)
                {
                    SkeletonSpawn();
                }
            }
        }

    }

}

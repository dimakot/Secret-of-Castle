using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Secret_of_Castle
{
    internal class MadScientist
    {
        List<Image> ScientistList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public List<UIElement> elc;
        public static int Speed_Scientist = 1;
        public ProgressBar ScientistHPBar;
        public static int HealthScientist = 1000;
        DateTime lastDamageBulbTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        DateTime lastCreateBulbTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delayDamage = 6000; //Задержка
        int delayCreate = 10000; //Задержка
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public MadScientist(Image player, Canvas CanvasGame, List<Image> zombiesList, List<UIElement> elc, ProgressBar ScientistHPBar) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.ScientistList = zombiesList;
            this.elc = elc;
            this.ScientistHPBar = ScientistHPBar;
        }
        public void MadScientistSpawn() //Создаем Ученого
        {
            Image Scientist = new Image();
            Scientist.Tag = "MadScientistBoss";
            Scientist.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/MadScientist/MadScientistRight.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для Ученого
            int MadScientistCanvasTop, MadScientistCanvasLeft; //Координаты Ученого
            do
            {
                MadScientistCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 100); //Использует случайное значение от 0 до крайней точки разрешения экрана
                MadScientistCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 100);
            } while (Math.Abs(Canvas.GetLeft(player) - MadScientistCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - MadScientistCanvasTop) < 800); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Scientist, MadScientistCanvasLeft);
            Canvas.SetTop(Scientist, MadScientistCanvasTop);
            Scientist.Height = 298; Scientist.Width = 144;
            ScientistList.Add(Scientist);
            CanvasGame.Children.Add(Scientist);
            HPbars.Add(Scientist, ScientistHPBar);
            Canvas.SetZIndex(player, 1);
        }
        public void MadScientistDamage()
        {
            BulbScientist magicAttack = new BulbScientist();
            magicAttack.MadScientistHorisontal = (int)(Canvas.GetLeft(player)); // Задаем координаты для снаряда
            magicAttack.MadScientistVertical = (int)(Canvas.GetTop(player));
            magicAttack.MadScientistMagicAttack(CanvasGame); // Создаем снаряд
        }


        public void ScientistMove() //Движение Ученого
        {
            DateTime currentTime1 = DateTime.Now;
            double differenceDae = (currentTime1 - lastCreateBulbTime).TotalMilliseconds;
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            foreach (UIElement w in elc)
            {
                if (w is Image ScientistMad && ((string)ScientistMad.Tag == "MadScientistBoss" || (string)ScientistMad.Tag == "MadScientistBoss"))
                {
                    if (Canvas.GetLeft(ScientistMad) > Canvas.GetLeft(player)) //Если ученый правее игрока
                    {
                        Canvas.SetLeft(ScientistMad, Canvas.GetLeft(ScientistMad) - Speed_Scientist); //Двигаем ученого влево
                        ScientistMad.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/MadScientist/MadScientistLeft.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetLeft(ScientistMad) < Canvas.GetLeft(player)) //Если ученый левее игрока
                    {
                        Canvas.SetLeft(ScientistMad, Canvas.GetLeft(ScientistMad) + Speed_Scientist); //Двигаем ученого вправо
                        ScientistMad.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/MadScientist/MadScientistRight.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetTop(ScientistMad) > Canvas.GetTop(player)) //Если ученый ниже игрока
                    {
                        Canvas.SetTop(ScientistMad, Canvas.GetTop(ScientistMad) - Speed_Scientist); //Двигаем ученого вверх
                    }
                    if (Canvas.GetTop(ScientistMad) < Canvas.GetTop(player)) //Если ученый выше игрока
                    {
                        Canvas.SetTop(ScientistMad, Canvas.GetTop(ScientistMad) + Speed_Scientist); //Двигаем ученого вниз
                    }
                    DateTime currentTime = DateTime.Now;
                    double differenceDamage = (currentTime - lastDamageBulbTime).TotalMilliseconds;
                    if (differenceDamage >= delayDamage)
                    {
                        lastDamageBulbTime = DateTime.Now;
                        MadScientistDamage();
                    }
                    if (ScientistHPBar.Value < 2500)
                    {
                        Speed_Scientist = 2;
                        delayDamage = 5000;
                    }
                    foreach (UIElement q in elc)
                    {
                        if (q is Image wizardWeapon && (string)wizardWeapon.Tag == "BulbMagicAttack")
                        {
                            Rect WizardWeaponHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                            Rect PlayerHitBox = new Rect(Canvas.GetLeft(wizardWeapon), Canvas.GetTop(wizardWeapon), wizardWeapon.RenderSize.Width, wizardWeapon.RenderSize.Height);
                            if (WizardWeaponHitBox.IntersectsWith(PlayerHitBox))
                            {
                                if (currentDifficulty == "Hard")
                                {
                                    Player.HealthPlayer -= 1;
                                }
                                else if (currentDifficulty == "Medium")
                                {
                                    Player.HealthPlayer -= 1;
                                }
                                else if (currentDifficulty == "Lite")
                                {
                                    Player.HealthPlayer -= 1;
                                }
                                if (differenceDae >= delayCreate)
                                {
                                    lastCreateBulbTime = DateTime.Now;
                                    wizardWeapon.Source = null;
                                    CanvasGame.Children.Remove(wizardWeapon);
                                }
                            }
                        }
                    }
                        foreach (UIElement j in elc)
                        {
                            if (j is Image WeaponDamage && ((string)WeaponDamage.Tag == "SwordAttack" || (string)WeaponDamage.Tag == "BasicMagicAttack" || (string)WeaponDamage.Tag == "BowArrow") && w is Image MadScientistBossAttack && (string)MadScientistBossAttack.Tag == "MadScientistBoss") //При попадании в зомби, урон отнимается
                            {
                                Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                                Rect ZombieDamage = new Rect(Canvas.GetLeft(MadScientistBossAttack), Canvas.GetTop(MadScientistBossAttack), MadScientistBossAttack.RenderSize.Width, MadScientistBossAttack.RenderSize.Height);
                                if (MagicSphere.IntersectsWith(ZombieDamage))
                                {
                                    WeaponDamage.Source = null;
                                    CanvasGame.Children.Remove(WeaponDamage);
                                    double damage = 0;
                                    if ((string)WeaponDamage.Tag == "SwordAttack")
                                    {
                                        damage = 10; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BowArrow")
                                    {
                                        damage = 15; //При попадании в зомби, урон отнимается
                                    }
                                    if ((string)WeaponDamage.Tag == "BasicMagicAttack")
                                    {
                                        damage = 25; //При попадании в зомби, урон отнимается
                                    }
                                    ScientistHPBar.Value -= damage; //При попадании в зомби, урон отнимается
                                }
                            }
                            if (ScientistHPBar.Value < 1)
                            {
                                CanvasGame.Children.Remove(ScientistMad); //При смерти зомби, он пропадает
                                CanvasGame.Children.Remove(ScientistHPBar);
                                ScientistList.Remove(ScientistMad); // Удаление зомби из списка
                                HPbars.Remove(ScientistMad); // Удаление зомби из словаря
                            }
                        }
                    }
            }
        }
        public void MadScientistLose()
        {
            foreach (Image i in ScientistList)
            {
                CanvasGame.Children.Remove(i);

            }
            ScientistList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            MadScientistSpawn();
        }
    }
}

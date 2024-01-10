using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class DarkWizard
    {
        List<Image> wizardList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public ProgressBar wizardHPBar;
        public int Speed_Wizard;
        public static int wizardKilles;
        public static int wizardNeeded = 0;
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        DateTime LastTeleportTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        DateTime LastDamageTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delay = 5000; //Задержка
        int delayDamage = 1500; //Задержка урона
        public List<UIElement> elc;



        public DarkWizard(Image player, Canvas CanvasGame, List<Image> wizardList, List<UIElement> elc, int wizardKilles = 0) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.wizardList = wizardList;
            this.elc = elc;

        }
        public void WizardSpawn() //Создаем мага
        {
            Image Wizards = new Image();
            Wizards.Tag = "Wizards";
            Wizards.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteRight.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для зомби
            int WizardCanvasTop, WizardCanvasLeft; //Координаты зомби
            do
            {
                WizardCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                WizardCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - WizardCanvasLeft) < 400 && Math.Abs(Canvas.GetTop(player) - WizardCanvasTop) < 400); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Wizards, WizardCanvasLeft);
            Canvas.SetTop(Wizards, WizardCanvasTop);
            wizardHPBar = new ProgressBar(); // Создаем ProgressBar для маг
            wizardHPBar.Width = 100; wizardHPBar.Height = 10;
            wizardHPBar.Value = 100; wizardHPBar.Maximum = 100;
            Canvas.SetLeft(wizardHPBar, WizardCanvasLeft);
            Canvas.SetTop(wizardHPBar, WizardCanvasTop - wizardHPBar.Height);
            Wizards.Height = 150; Wizards.Width = 100;
            wizardList.Add(Wizards);
            CanvasGame.Children.Add(Wizards);
            CanvasGame.Children.Add(wizardHPBar);
            HPbars.Add(Wizards, wizardHPBar);
            Panel.SetZIndex(player, 1);
        }
        public void TeleportRandomly()
        {
            foreach (UIElement wz in elc)
            {
                if (wz is Image wizards && (string)wizards.Tag == "Wizards") // Если элемент - маг
                {
                    int WizardCanvasTop, WizardCanvasLeft; //Координаты маг
                    do
                    {
                        WizardCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                        WizardCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
                    } while (Math.Abs(Canvas.GetLeft(player) - WizardCanvasLeft) < 400 && Math.Abs(Canvas.GetTop(player) - WizardCanvasTop) < 400); //Пока расстояние между игроком и магом меньше 800, маг не спавнится
                    Canvas.SetLeft(wizards, WizardCanvasLeft); // Устанавливаем координаты для мага
                    Canvas.SetTop(wizards, WizardCanvasTop); // Устанавливаем координаты для мага

                    Canvas.SetLeft(wizards, WizardCanvasLeft);
                    Canvas.SetTop(wizards, WizardCanvasTop);
                    if (HPbars.ContainsKey(wizards))
                    {
                        ProgressBar wizardHPBar = HPbars[wizards];
                        Canvas.SetLeft(wizardHPBar, WizardCanvasLeft);
                        Canvas.SetTop(wizardHPBar, WizardCanvasTop - wizardHPBar.Height);
                    }
                }
            }
        }
        public string CalculateDirection(double WizardCanvasLeft, double WizardCanvasTop, double playerLeft, double playerTop) // Вычисляем направление
        {
            // Разница между координатами игрока и зомби
            double deltaX = playerLeft - WizardCanvasLeft;
            double deltaY = playerTop - WizardCanvasTop;

            //Угол между Магом и игроком
            double angle = Math.Atan2(deltaY, deltaX); // Возвращает угол в радианах

            Dictionary<string, (double, double)> way = new Dictionary<string, (double, double)> //Словарь для направлений
            {
                {"Angle0", (-Math.PI / 8, Math.PI / 8)}, // Задаем диапазон углов
                {"Angle45", (-3 * Math.PI / 8, -Math.PI / 8)},
                {"Angle90", (-5 * Math.PI / 8, -3 * Math.PI / 8)},
                {"Angle135", (-7 * Math.PI / 8, -5 * Math.PI / 8)},
                {"Angle180", (7 * Math.PI / 8, -7 * Math.PI / 8)},
                {"Angle225", (5 * Math.PI / 8, 7 * Math.PI / 8)},
                {"Angle270", (3 * Math.PI / 8, 5 * Math.PI / 8)},
                {"Angle315", (Math.PI / 8, 3 * Math.PI / 8)},
            };

            // Направление по умолчанию
            foreach (var Way in way) // Перебираем словарь
            {
                if (angle >= Way.Value.Item1 && angle < Way.Value.Item2) // Если угол между заданным диапазоном, то возвращаем направление
                {
                    return Way.Key; // Возвращаем направление
                }
            }

            return "Angle180"; // Стреляем налево если угол не соответствует ни одному из заданных углов
        }
        public void WizardDamage() //Урон мага
        {
            foreach (UIElement e in elc) // Перебираем все элементы на канвасе
            {
                if (e is Image wizard && (string)wizard.Tag == "Wizards") // Если элемент - маг
                {
                    LastDamageTime = DateTime.Now; // Записываем время последнего урона
                    string direction = CalculateDirection(Canvas.GetLeft(wizard), Canvas.GetTop(wizard), Canvas.GetLeft(player), Canvas.GetTop(player)); // Вычисляем направление
                    ShootWizardWeapon ShootWizardWeapon = new ShootWizardWeapon(); // Создаем экземпляр класса
                    ShootWizardWeapon.way = direction; // Задаем направление
                    ShootWizardWeapon.MagicHorisontal = (int)(Canvas.GetLeft(wizard) + (wizard.Width / 2)); // Задаем координаты для снаряда
                    ShootWizardWeapon.MagicVertical = (int)(Canvas.GetTop(wizard) + (wizard.Height / 2));
                    ShootWizardWeapon.WizardMagicSphere(CanvasGame); // Создаем снаряд

                }
            }

        }
        public void WizardAI()
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            // ИИ мага
            foreach (UIElement e in elc)
            {
                if (e is Image wizard && (string)wizard.Tag == "Wizards")
                {
                    if (Canvas.GetLeft(wizard) > Canvas.GetLeft(player))
                    {
                        wizard.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteLeft.png", UriKind.RelativeOrAbsolute));
                    }
                    if (Canvas.GetLeft(wizard) < Canvas.GetLeft(player))
                    {
                        wizard.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteRight.png", UriKind.RelativeOrAbsolute));
                    }
                    DateTime currentTime = DateTime.Now;
                    double difference = (currentTime - LastTeleportTime).TotalMilliseconds;
                    if (difference >= delay)
                    {
                        LastTeleportTime = DateTime.Now;
                        TeleportRandomly();
                    }
                    double differenceDamage = (currentTime - LastDamageTime).TotalMilliseconds;
                    if (differenceDamage >= delayDamage)
                    {
                        LastDamageTime = DateTime.Now;
                        WizardDamage();
                    }
                    foreach (UIElement w in elc)
                    {
                        if (w is Image wizardWeapon && (string)wizardWeapon.Tag == "WizardMagicAttack")
                        {
                            Rect WizardWeaponHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                            Rect PlayerHitBox = new Rect(Canvas.GetLeft(wizardWeapon), Canvas.GetTop(wizardWeapon), wizardWeapon.RenderSize.Width, wizardWeapon.RenderSize.Height);
                            if (WizardWeaponHitBox.IntersectsWith(PlayerHitBox))
                            {
                                if (currentDifficulty == "Hard")
                                {
                                    Player.HealthPlayer -= 25;
                                }
                                else if (currentDifficulty == "Medium")
                                {
                                    Player.HealthPlayer -= 15;
                                }
                                else if (currentDifficulty == "Lite")
                                {
                                    Player.HealthPlayer -= 4;
                                }
                                wizardWeapon.Source = null;
                                CanvasGame.Children.Remove(wizardWeapon);
                            }
                        }
                        foreach (UIElement j in elc)
                        {
                            if (j is Image WeaponDamage && ((string)WeaponDamage.Tag == "SwordAttack" || (string)WeaponDamage.Tag == "BasicMagicAttack" || (string)WeaponDamage.Tag == "BowArrow") && e is Image wizardAttack && (string)wizardAttack.Tag == "Wizards") //При попадании в зомби, урон отнимается
                            {
                                Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                                Rect ZombieDamage = new Rect(Canvas.GetLeft(wizardAttack), Canvas.GetTop(wizardAttack), wizardAttack.RenderSize.Width, wizardAttack.RenderSize.Height);
                                if (MagicSphere.IntersectsWith(ZombieDamage))
                                {
                                    if (HPbars.ContainsKey(wizardAttack)) // Проверяем, существует ли зомби в словаре HPbars
                                    {
                                        ProgressBar wizardHPBar = HPbars[wizardAttack]; // Получаем ProgressBar для текущего зомби
                                        WeaponDamage.Source = null;
                                        CanvasGame.Children.Remove(WeaponDamage);
                                        if ((string)WeaponDamage.Tag == "SwordAttack")
                                        {
                                            wizardHPBar.Value -= 1; //При попадании в зомби, урон отнимается
                                        }
                                        if ((string)WeaponDamage.Tag == "BowArrow")
                                        {
                                            wizardHPBar.Value -= 1; //При попадании в зомби, урон отнимается
                                        }
                                        if ((string)WeaponDamage.Tag == "BasicMagicAttack")
                                        {
                                            wizardHPBar.Value -= 1; //При попадании в зомби, урон отнимается
                                        }
                                    }
                                }
                                if (HPbars.ContainsKey(wizard))
                                {
                                    ProgressBar wizardHPBar = HPbars[wizard];
                                    if (wizardHPBar.Value < 1)
                                    {
                                        CanvasGame.Children.Remove(wizard); //При смерти зомби, он пропадает
                                        CanvasGame.Children.Remove(wizardHPBar);
                                        wizardList.Remove(wizard); // Удаление зомби из списка
                                        HPbars.Remove(wizard); // Удаление зомби из словаря
                                        wizardKilles++;
                                        wizardNeeded++;
                                        if (currentDifficulty == "Hard") //В зависимости от сложности, спавнится определенное кол-во зомби
                                        {
                                            if (wizardKilles < 10)
                                            {
                                                WizardSpawn();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (currentDifficulty == "Medium")
                                        {
                                            if (wizardKilles < 5)
                                            {
                                                WizardSpawn();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (currentDifficulty == "Lite")
                                        {
                                            if (wizardKilles < 3)
                                            {
                                                WizardSpawn();
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
        }
        public void GameLose()
        {
            foreach (Image i in wizardList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);

            }
            wizardList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            if (currentDifficulty == "Hard")
            {
                for (int i = 0; i < 3; i++)
                {
                    WizardSpawn();
                }
            }
            else if (currentDifficulty == "Medium")
            {
                for (int i = 0; i < 2; i++)
                {
                    WizardSpawn();
                }
            }
            else if (currentDifficulty == "Lite")
            {
                for (int i = 0; i < 1; i++)
                {
                    WizardSpawn();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class Dragon
    {
        List<Image> DragonList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public List<UIElement> elc;
        public static int Speed_Dragon = 1;
        public ProgressBar DragonHPBar;
        public static int HealthDragon = 5000;
        DateTime lastDamageMagicTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delayDamage = 3000; //Задержка
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public Dragon(Image player, Canvas CanvasGame, List<Image> zombiesList, List<UIElement> elc, ProgressBar DragonHPBar) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.DragonList = zombiesList;
            this.elc = elc;
            this.DragonHPBar = DragonHPBar;
        }
        public void DragonSpawn() //Создаем зДракона
        {
            Image Dragon = new Image();
            Dragon.Tag = "Dragon";
            Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_RightLite.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для Дракона
            int DragonCanvasTop, DragonCanvasLeft; //Координаты Дракона
            do
            {
                DragonCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 100); //Использует случайное значение от 0 до крайней точки разрешения экрана
                DragonCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 100);
            } while (Math.Abs(Canvas.GetLeft(player) - DragonCanvasLeft) < 800 && Math.Abs(Canvas.GetTop(player) - DragonCanvasTop) < 800); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Dragon, DragonCanvasLeft);
            Canvas.SetTop(Dragon, DragonCanvasTop);
            Dragon.Height = 235; Dragon.Width = 375;
            DragonList.Add(Dragon);
            CanvasGame.Children.Add(Dragon);
            HPbars.Add(Dragon, DragonHPBar);
            Canvas.SetZIndex(player, 1);
        }
        public string CalculateDirection(double WizardCanvasLeft, double WizardCanvasTop, double playerLeft, double playerTop)
        {
            // Разница между координатами игрока и зомби
            double deltaX = playerLeft - WizardCanvasLeft;
            double deltaY = playerTop - WizardCanvasTop;

            //Угол между Магом и игроком
            double angle = Math.Atan2(deltaY, deltaX);

            // Словарь направлений
            Dictionary<string, (double, double)> way = new Dictionary<string, (double, double)>
            {
                {"Angle0", (-Math.PI / 8, Math.PI / 8)},
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
                    return Way.Key;
                }
            }

            return "Angle180"; // Стреляем налево если угол не соответствует ни одному из заданных углов
        }
        public void DragonDamDamage() //Урон мага
        {
            foreach (UIElement e in elc) // Перебираем все элементы на канвасе
            {
                if (e is Image Dragon && (string)Dragon.Tag == "Dragon") // Если элемент - маг
                {
                    lastDamageMagicTime = DateTime.Now; // Записываем время последнего урона
                    string direction = CalculateDirection(Canvas.GetLeft(Dragon), Canvas.GetTop(Dragon), Canvas.GetLeft(player), Canvas.GetTop(player)); // Вычисляем направление
                    DragonAttackClass ShootDragonWeapon = new DragonAttackClass(); // Создаем экземпляр класса
                    ShootDragonWeapon.way = direction; // Задаем направление
                    ShootDragonWeapon.DragonHorisontal = (int)(Canvas.GetLeft(Dragon) + (Dragon.Width / 2)); // Задаем координаты для снаряда
                    ShootDragonWeapon.DragonVertical = (int)(Canvas.GetTop(Dragon) + (Dragon.Height / 2));
                    ShootDragonWeapon.DragonMagicAttack(CanvasGame); // Создаем снаряд

                }
            }

        }
        public void DragonMove() //Движение Дракона
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            foreach (UIElement w in elc)
            {
                if (w is Image Dragon && (string)Dragon.Tag == "Dragon")
                {
                    if (Canvas.GetLeft(Dragon) > Canvas.GetLeft(player)) //Если дракон правее игрока
                    {
                        Canvas.SetLeft(Dragon, Canvas.GetLeft(Dragon) - Speed_Dragon); //Двигаем дракон влево
                        if (DragonHPBar.Value < 2500)
                        {
                            Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_LeftHard.png", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_LeftLite.png", UriKind.RelativeOrAbsolute));
                        }
                    }
                    if (Canvas.GetLeft(Dragon) < Canvas.GetLeft(player)) //Если дракон левее игрока
                    {
                        Canvas.SetLeft(Dragon, Canvas.GetLeft(Dragon) + Speed_Dragon); //Двигаем дракон вправо
                        Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_RightLite.png", UriKind.RelativeOrAbsolute));
                        if (DragonHPBar.Value < 2500)
                        {
                            Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_RightHard.png", UriKind.RelativeOrAbsolute));
                        }
                        else { Dragon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/Boss/Dragon/Dragon_RightLite.png", UriKind.RelativeOrAbsolute)); }
                    }
                    if (Canvas.GetTop(Dragon) > Canvas.GetTop(player)) //Если дракон ниже игрока
                    {
                        Canvas.SetTop(Dragon, Canvas.GetTop(Dragon) - Speed_Dragon); //Двигаем дракон вверх
                    }
                    if (Canvas.GetTop(Dragon) < Canvas.GetTop(player)) //Если дракон выше игрока
                    {
                        Canvas.SetTop(Dragon, Canvas.GetTop(Dragon) + Speed_Dragon); //Двигаем дракон вниз
                    }
                    DateTime currentTime = DateTime.Now;
                    double differenceDamage = (currentTime - lastDamageMagicTime).TotalMilliseconds;
                    if (differenceDamage >= delayDamage)
                    {
                        lastDamageMagicTime = DateTime.Now;
                        DragonDamDamage();
                    }
                    if (DragonHPBar.Value < 2500)
                    {
                        Speed_Dragon = 3;
                        delayDamage = 2000;
                    }
                    foreach (UIElement j in elc)
                    {
                        if (j is Image WeaponDamage && ((string)WeaponDamage.Tag == "SwordAttack" || (string)WeaponDamage.Tag == "BasicMagicAttack" || (string)WeaponDamage.Tag == "BowArrow") && w is Image DragonMobAttack && (string)DragonMobAttack.Tag == "Dragon") //При попадании в зомби, урон отнимается
                        {
                            Rect MagicSphere = new Rect(Canvas.GetLeft(WeaponDamage), Canvas.GetTop(WeaponDamage), WeaponDamage.RenderSize.Width, WeaponDamage.RenderSize.Height);
                            Rect ZombieDamage = new Rect(Canvas.GetLeft(DragonMobAttack), Canvas.GetTop(DragonMobAttack), DragonMobAttack.RenderSize.Width, DragonMobAttack.RenderSize.Height);
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
                                DragonHPBar.Value -= damage; //При попадании в зомби, урон отнимается
                            }
                        }
                        foreach (UIElement i in elc)
                        {
                            if (i is Image dragonAttackMagic && (string)dragonAttackMagic.Tag == "DragonMagicAttack")
                            {
                                Rect PlayerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                                Rect DragonWeaponHitBox = new Rect(Canvas.GetLeft(dragonAttackMagic), Canvas.GetTop(dragonAttackMagic), dragonAttackMagic.RenderSize.Width, dragonAttackMagic.RenderSize.Height);
                                if (DragonWeaponHitBox.IntersectsWith(PlayerHitBox))
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
                                    dragonAttackMagic.Source = null;
                                    CanvasGame.Children.Remove(dragonAttackMagic);
                                }
                            }
                        }

                        if (DragonHPBar.Value < 1)
                        {
                            CanvasGame.Children.Remove(Dragon); //При смерти зомби, он пропадает
                            CanvasGame.Children.Remove(DragonHPBar);
                            DragonList.Remove(Dragon); // Удаление зомби из списка
                            HPbars.Remove(Dragon); // Удаление зомби из словаря
                        }
                    }
                }
            }
        }
        public void DragonLose()
        {
            foreach (Image i in DragonList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);

            }
            DragonList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            DragonSpawn();
        }
    }
}

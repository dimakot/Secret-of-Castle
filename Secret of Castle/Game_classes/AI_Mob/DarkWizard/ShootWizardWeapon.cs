using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Secret_of_Castle
{
    internal class ShootWizardWeapon
    {
        public string way;
        public int MagicHorisontal;
        public int MagicVertical;
        List<Image> wizardList; //Список для моба
        private int SpeedMagicWizard = 50; //Скорость магиского снаряда
        private Image WizardWeapon = new Image();
        private DispatcherTimer TimerMagicWizard = new DispatcherTimer();
        public void WizardMagicSphere(Canvas CanvasGame)
        {
            List<string> MagicSphereImages = new List<string>() { //Создаем лист из картинок для анимации при помощи таймера
                "pack://application:,,,/Texture/Weapon/Magic/DarkWizardWeapon/Weapon_DarkWizardLite_1.png",
                "pack://application:,,,/Texture/Weapon/Magic/DarkWizardWeapon/Weapon_DarkWizardLite_2.png",
                "pack://application:,,,/Texture/Weapon/Magic/DarkWizardWeapon/Weapon_DarkWizardLite_3.png",
                "pack://application:,,,/Texture/Weapon/Magic/DarkWizardWeapon/Weapon_DarkWizardLite_4.png"
            }; int animationCurrentImage = 0;
            WizardWeapon.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute));
            WizardWeapon.Height = 30; WizardWeapon.Width = 30; //задаем стандартные параметры для генерации Магической сферы, тег
            WizardWeapon.Tag = "WizardMagicAttack";
            Canvas.SetLeft(WizardWeapon, MagicHorisontal); Canvas.SetTop(WizardWeapon, MagicVertical);
            CanvasGame.Children.Add(WizardWeapon);
            TimerMagicWizard.Interval = TimeSpan.FromMilliseconds(SpeedMagicWizard); //запускаем таймер со скоростью в указанной выши
            TimerMagicWizard.Tick += (sender, e) =>
            {
                animationCurrentImage = (animationCurrentImage + 1) % MagicSphereImages.Count;
                WizardWeapon.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute)); WizardDamage(sender, e); //анимация воспроизводится со скоростью таймера
            };
            TimerMagicWizard.Start();
        }
        public void WizardDamage(object sender, EventArgs w) //Событие для магической сферы
        {

            if (way == "Angle0") //Если угол 0, то
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) + SpeedMagicWizard); //Снаряд летит вправо
            }
            if (way == "Angle45")
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) + SpeedMagicWizard);
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) - SpeedMagicWizard);
            }

            if (way == "Angle90")
            {
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) - SpeedMagicWizard);
            }
            if (way == "Angle135")
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) - SpeedMagicWizard);
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) - SpeedMagicWizard);
            }
            if (way == "Angle180")
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) - SpeedMagicWizard);
            }
            if (way == "Angle225")
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) - SpeedMagicWizard);
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) + SpeedMagicWizard);
            }

            if (way == "Angle270")
            {
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) + SpeedMagicWizard);
            }

            if (way == "Angle315")
            {
                Canvas.SetLeft(WizardWeapon, Canvas.GetLeft(WizardWeapon) + SpeedMagicWizard);
                Canvas.SetTop(WizardWeapon, Canvas.GetTop(WizardWeapon) + SpeedMagicWizard);
            }
            if (Canvas.GetLeft(WizardWeapon) < 0 || Canvas.GetLeft(WizardWeapon) > 2000 || Canvas.GetTop(WizardWeapon) < 0 || Canvas.GetTop(WizardWeapon) > 2000)
            {
                TimerMagicWizard.Stop();
                TimerMagicWizard = null;
                WizardWeapon.Source = null;
            }
        }

    }
}

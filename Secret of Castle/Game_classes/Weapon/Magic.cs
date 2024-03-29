﻿using Secret_of_Castle;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Weapon //пространство имен для оружия
{
    internal class Magic
    {
        public string ControlWeapon;
        public int MagicHorisontal;
        public int MagicVertical;
        private int SpeedMagic = 50; //Скорость магиского снаряда
        private Image MagicSphere = new Image();
        private DispatcherTimer TimerBasicMagicWeapon = new DispatcherTimer();
        public void SphereMagicNew(Canvas CanvasGame) //Создание магической сферы
        {
            List<string> MagicSphereImages = new List<string>() { //Создаем лист из картинок для анимации при помощи таймера
                "pack://application:,,,/Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_1.png",
                "pack://application:,,,/Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_2.png",
                "pack://application:,,,/Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_3.png",
                "pack://application:,,,/Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_4.png"
            }; int animationCurrentImage = 0;
            MagicSphere.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute));
            MagicSphere.Height = 106; MagicSphere.Width = 70; //задаем стандартные параметры для генерации Магической сферы, тег
            MagicSphere.Tag = "BasicMagicAttack";
            Canvas.SetLeft(MagicSphere, MagicHorisontal); Canvas.SetTop(MagicSphere, MagicVertical);
            Canvas.SetZIndex(MagicSphere, 1);
            CanvasGame.Children.Add(MagicSphere);
            TimerBasicMagicWeapon.Interval = TimeSpan.FromMilliseconds(SpeedMagic); //запускаем таймер со скоростью в указанной выши
            TimerBasicMagicWeapon.Tick += (sender, e) =>
            {
                animationCurrentImage = (animationCurrentImage + 1) % MagicSphereImages.Count;
                MagicSphere.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute)); BasicMagicEvent(sender, e); //анимация воспроизводится со скоростью таймера
            };
            TimerBasicMagicWeapon.Start();
        }
        private void BasicMagicEvent(object sender, EventArgs w) //Событие для магической сферы
        {
            if (ControlWeapon == "Left") //Если прожата кнопка (тег забит в персонажа (не работает )) снаряд стреляет в определенную сторону
            {
                Canvas.SetLeft(MagicSphere, Canvas.GetLeft(MagicSphere) - SpeedMagic);
            }
            if (ControlWeapon == "Right")
            {
                Canvas.SetLeft(MagicSphere, Canvas.GetLeft(MagicSphere) + SpeedMagic);
            }
            if (ControlWeapon == "Down")
            {
                Canvas.SetTop(MagicSphere, Canvas.GetTop(MagicSphere) + SpeedMagic);
            }
            if (ControlWeapon == "Up")
            {
                Canvas.SetTop(MagicSphere, Canvas.GetTop(MagicSphere) - SpeedMagic);
            }

            if (Canvas.GetLeft(MagicSphere) < 10 || Canvas.GetLeft(MagicSphere) > 2000 || Canvas.GetTop(MagicSphere) < 10 || Canvas.GetTop(MagicSphere) > 1000)
            {
                TimerBasicMagicWeapon.Stop(); // при достижении определенной точки на канвасе таймер останавливается и сфера пропадает
                MagicSphere.Source = null;
                TimerBasicMagicWeapon = null;
            }
        }

    }
}
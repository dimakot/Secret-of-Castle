﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;
using Weapon;

namespace Secret_of_Castle
{
    class DragonAttackClass
    {
        public string way;
        public int DragonHorisontal;
        public int DragonVertical;
        List<Image> DragonList; //Список для моба
        private int SpeedMagicDragonAttack = 50; //Скорость магического снаряда
        private Image DragonWeapon = new Image();
        private DispatcherTimer TimerMagicDragon = new DispatcherTimer();

        public void DragonMagicAttack(Canvas CanvasGame)
        {
            DragonWeapon.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Weapon/Magic/DragonWeapon/Fire_Dragon.png", UriKind.RelativeOrAbsolute));
            DragonWeapon.Height = 38; DragonWeapon.Width = 82; //задаем стандартные параметры для генерации Магической сферы, тег
            DragonWeapon.Tag = "DragonMagicAttack";
            Canvas.SetLeft(DragonWeapon, DragonHorisontal); Canvas.SetTop(DragonWeapon, DragonVertical);
            Panel.SetZIndex(DragonWeapon, 1);
            CanvasGame.Children.Add(DragonWeapon);
            TimerMagicDragon.Interval = TimeSpan.FromMilliseconds(SpeedMagicDragonAttack); //запускаем таймер со скоростью в указанной выши
            TimerMagicDragon.Tick += (sender, e) =>
            {
                DragonDamage(sender, e);
            };
            TimerMagicDragon.Start();
        }
        public void DragonDamage(object sender, EventArgs w) //Событие для магической сферы
        {

            if (way == "Angle0")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) + SpeedMagicDragonAttack);
            }
            if (way == "Angle45")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) + SpeedMagicDragonAttack);
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) - SpeedMagicDragonAttack);
            }

            if (way == "Angle90")
            {
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) - SpeedMagicDragonAttack);
            }
            if (way == "Angle135")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) - SpeedMagicDragonAttack);
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) - SpeedMagicDragonAttack);
            }
            if (way == "Angle180")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) - SpeedMagicDragonAttack);
            }
            if (way == "Angle225")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) - SpeedMagicDragonAttack);
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) + SpeedMagicDragonAttack);
            }

            if (way == "Angle270")
            {
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) + SpeedMagicDragonAttack);
            }

            if (way == "Angle315")
            {
                Canvas.SetLeft(DragonWeapon, Canvas.GetLeft(DragonWeapon) + SpeedMagicDragonAttack);
                Canvas.SetTop(DragonWeapon, Canvas.GetTop(DragonWeapon) + SpeedMagicDragonAttack);
            }
            if (Canvas.GetLeft(DragonWeapon) < 0 || Canvas.GetLeft(DragonWeapon) > 2000 || Canvas.GetTop(DragonWeapon) < 0 || Canvas.GetTop(DragonWeapon) > 2000)
            {
                TimerMagicDragon.Stop();
                TimerMagicDragon = null;
                DragonWeapon.Source = null;
            }
        }

    }
}

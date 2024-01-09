﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Secret_of_Castle
{
    internal class Player
    {
        public static bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown, Lose;
        public static int Speed = 7;
        public static string ControlWeapon = "Down";
        Image player;
        Canvas CanvasGame;
        ProgressBar hp_bar;
        public static int HealthPlayer = 100;
        public List<UIElement> elc;
        DispatcherTimer gametimer;

        public Player(Image player, Canvas CanvasGame, ProgressBar hp_bar, DispatcherTimer gametimer)
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.hp_bar = hp_bar;
            this.gametimer = gametimer;
        }
        public void kbup(object sender, KeyEventArgs e) //Кнопка поднята
        {
            if (Lose == true)
            {
                return;
            }
            if (e.Key == Key.W)
            {
                UpKeyDown = false;
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyDown = false;

            }
            if (e.Key == Key.D)
            {
                RightKeyDown = false;
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 7 + Perks.Speed_boosting;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            }
        }
        public void kbdown(object sender, KeyEventArgs e) //Кнопка опущена
        {
            if (Lose == true) //Если активно условие проигрыша, то управление не работает
            {
                return;
            }
            if (e.Key == Key.W)
            {
                UpKeyDown = true;
                ControlWeapon = "Up";
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = true;
                ControlWeapon = "Down";
            }
            if (e.Key == Key.A)
            {
                LeftKeyDown = true;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_left.png", UriKind.RelativeOrAbsolute));
                ControlWeapon = "Left";
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = true;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
                ControlWeapon = "Right";
            }
            if (e.Key == Key.LeftShift)
            {
                Speed = 12 + Perks.Speed_boosting;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture\\Mob\\Player\\player_berserk.png", UriKind.RelativeOrAbsolute));
            }
        }
        public void Control()
        {
            if (LeftKeyDown == true && Canvas.GetLeft(player) > 0) //Движения игрока
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
            }

            if (RightKeyDown == true && Canvas.GetLeft(player) + player.Width < this.CanvasGame.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
            }

            if (UpKeyDown == true && Canvas.GetTop(player) > 85)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
            }

            if (DownKeyDown == true && Canvas.GetTop(player) + player.Height < this.CanvasGame.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
            }
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            foreach (UIElement u in elc)
            {
                if (u is Image collisionobj && (string)collisionobj.Tag == "objects" && u is Image ZombieMob && (string)ZombieMob.Tag == "Zombie")
                {
                    Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect6 = new Rect(Canvas.GetLeft(collisionobj), Canvas.GetTop(collisionobj), collisionobj.RenderSize.Width, collisionobj.RenderSize.Height);
                    Rect rect7 = new Rect(Canvas.GetLeft(ZombieMob), Canvas.GetTop(ZombieMob), ZombieMob.RenderSize.Width, ZombieMob.RenderSize.Height);
                    //Проверка на столкновение с объектами
                    if (playerRect.IntersectsWith(rect6))
                    {
                        if (UpKeyDown)
                        {
                            UpKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
                        }
                        if (DownKeyDown)
                        {
                            DownKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                        }
                        if (LeftKeyDown)
                        {
                            LeftKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                        }
                        if (RightKeyDown)
                        {
                            RightKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                        }
                    }
                    if (playerRect.IntersectsWith(rect7))
                    {
                        if (UpKeyDown)
                        {
                            UpKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) + Speed);
                        }
                        if (DownKeyDown)
                        {
                            DownKeyDown = false;
                            Canvas.SetTop(player, Canvas.GetTop(player) - Speed);
                        }
                        if (LeftKeyDown)
                        {
                            LeftKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed);
                        }
                        if (RightKeyDown)
                        {
                            RightKeyDown = false;
                            Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed);
                        }
                    }
                }
            }
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            if (currentDifficulty == "Hard") //В зависимости от сложности, максимальное значение HP бара разное
            {
                if (Zombie.zombiesNeeded > 20) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Zombie.zombiesNeeded = 0;
                }
                if (DarkWizard.wizardNeeded > 4) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    DarkWizard.wizardNeeded = 0;
                }
                if (Skeleton.skeletonNeeded > 15) //Если убито 10 скелетов, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    Skeleton.skeletonNeeded = 0;
                }
                hp_bar.Maximum = 100 + Perks.hp_boosting;
            }
            if (currentDifficulty == "Medium")
            {
                if (Zombie.zombiesNeeded > 10) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Zombie.zombiesNeeded = 0;
                }
                if (DarkWizard.wizardNeeded > 3) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 5;
                    DarkWizard.wizardNeeded = 0;
                }
                if (Skeleton.skeletonNeeded > 10) //Если убито 10 скелетов, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    Skeleton.skeletonNeeded = 0;
                }
                hp_bar.Maximum = 150 + Perks.hp_boosting;
            }
            if (currentDifficulty == "Lite")
            {
                if (Zombie.zombiesNeeded > 5) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Zombie.zombiesNeeded = 0;
                }
                if (DarkWizard.wizardNeeded > 2) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 5;
                    DarkWizard.wizardNeeded = 0;
                }
                if (Skeleton.skeletonNeeded > 5) //Если убито 10 скелетов, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Skeleton.skeletonNeeded = 0;
                }
                hp_bar.Maximum = 200 + Perks.hp_boosting;
            }
            if (DarkWizard.wizardKilles > 20) //Если убито 10 зомби, то игрок получает 10 здоровья
            {
                HealthPlayer += 20;
            }
            if (HealthPlayer > 1) //Если HP больше 1, то мы заносим значение здоровья в прогресс бар 
            {
                hp_bar.Value = HealthPlayer;
            }
            else //иначе игрок проигрывает, таймер отключается
            {
                Lose = true;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_berserk.png", UriKind.RelativeOrAbsolute));
                gametimer.Stop();
            }
            if (hp_bar.Value > 75)
            {
                hp_bar.Foreground = Brushes.Green; //если здоровья больше 50, то ProgressBar окрашен в зеленый 
            }
            else if (hp_bar.Value > 25)
            {
                hp_bar.Foreground = Brushes.Yellow; //если здоровья больше 25, то ProgressBar окрашен в желтый
            }
            else
            {
                hp_bar.Foreground = Brushes.Red; //в иных случаях ProgressBar красный 
            }
        }
    }
}
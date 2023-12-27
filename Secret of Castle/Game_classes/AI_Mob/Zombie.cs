﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Secret_of_Castle
{
    internal class Zombie
    {
        Image player;
        Random rand = new Random();
        Canvas CanvasGame;
        List<object> zombiesList; //Список для моба
        public List<UIElement> elc;
        public int Speed_Zombie;
        ProgressBar zombieHPBar;
        public static int zombieHP = 100;
        difficult Difficult;
        DateTime lastDamageTime; //Используем модуль времени, для отображения последнего времени нанесения урона
        int delay = 1000; //Задержка
        public Zombie(Image player, Canvas CanvasGame, List<object> zombiesList, List<UIElement> elc, int Speed_Zombie = 2) {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.zombiesList = zombiesList;
            this.elc = elc;
            this.Speed_Zombie = Speed_Zombie;
        }

        public void MobSpawn() //Создаем зомби
        {
            Image Zombies = new Image();
            Zombies.Tag = "Zombie";
            Zombies.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(Zombies, rand.Next(0, Convert.ToInt32(CanvasGame.Width))); //Использует случайное значение от 0 до крайней точки разрешения экрана
            Canvas.SetTop(Zombies, rand.Next(85, Convert.ToInt32(CanvasGame.Height)));
            zombieHPBar = new ProgressBar(); zombieHPBar.Background = Brushes.Gray; zombieHPBar.Foreground = Brushes.Green;
            zombieHPBar.Width = 170; zombieHPBar.Height = 12;
            Zombies.Height = 200; Zombies.Width = 200;
            zombieHPBar.Minimum = 0; zombieHPBar.Maximum = zombieHP; zombieHPBar.Value = zombieHP;
            zombieHPBar.Tag = "ZombieHP";
            zombiesList.Add(Zombies); // Добавляем зомби в список
            zombiesList.Add(zombieHPBar); // Добавляем ProgressBar в список
            CanvasGame.Children.Add(Zombies); // Добавляем зомби на Canvas
            CanvasGame.Children.Add(zombieHPBar); // Добавляем ProgressBar на Canvas
            Canvas.SetZIndex(player, 1);
        }
        public void ZombieMovement() {
            foreach (UIElement w in elc) {
                if (w is Image ImgZomb && (string)ImgZomb.Tag == "Zombie")
                {
                    Rect rect1 = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                    Rect rect2 = new Rect(Canvas.GetLeft(ImgZomb), Canvas.GetTop(ImgZomb), ImgZomb.RenderSize.Width, ImgZomb.RenderSize.Height);
                    if (rect1.IntersectsWith(rect2))
                    {
                        DateTime currentTime = DateTime.Now;
                        double difference = (currentTime - lastDamageTime).TotalMilliseconds;

                        if (difference >= delay) //В зависимости от выбранной сложности, игрок получает определенное кол-во урона
                        {
                            string currentDifficulty = difficult.Instance.CurrentDifficulty;
                            if (currentDifficulty == "Hard")
                            {
                                Player.HealthPlayer -= 50;
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
                            else
                            {
                                Player.HealthPlayer -= 4;
                                lastDamageTime = currentTime;
                            }
                        }

                    }
                    if (Canvas.GetLeft(ImgZomb) > Canvas.GetLeft(player)) //Движение зомби
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) - Speed_Zombie);
                        ImgZomb.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_left.png", UriKind.Relative)); //Текстура зомби, идущего влево
                        Canvas.SetLeft(zombieHPBar, Canvas.GetLeft(ImgZomb) + 15);
                    }

                    if (Canvas.GetLeft(ImgZomb) < Canvas.GetLeft(player))
                    {
                        Canvas.SetLeft(ImgZomb, Canvas.GetLeft(ImgZomb) + Speed_Zombie);
                    }

                    if (Canvas.GetTop(ImgZomb) > Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) - Speed_Zombie);
                        ImgZomb.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/Zombie/zombie_right.png", UriKind.Relative)); // Текстура зомби, идущего вправо
                        Canvas.SetTop(zombieHPBar, Canvas.GetTop(ImgZomb) - 15);
                    }

                    if (Canvas.GetTop(ImgZomb) < Canvas.GetTop(player))
                    {
                        Canvas.SetTop(ImgZomb, Canvas.GetTop(ImgZomb) + Speed_Zombie);
                    }
                }
            }
        }
        public void GameLose() {
            player.Source = new BitmapImage(new Uri("Texture/Mob/Enemy/zombie_right.png", UriKind.Relative));
            foreach (Image i in zombiesList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                            CanvasGame.Children.Remove(i);
            }

            zombiesList.Clear();

            for (int i = 0; i < 3; i++) //если мобов меньше чем 3, спавнится еще один
            {
                MobSpawn();
            }
        }

    }
}

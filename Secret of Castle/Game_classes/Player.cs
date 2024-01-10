        using System;
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
        public static bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown, Lose; //Переменные для управления игроком
        public static int Speed = 7; //Скорость игрока
        public static string ControlWeapon = "Down"; //Направление оружия
        Image player; //Изображение игрока
        Canvas CanvasGame; //Канвас игры
        ProgressBar hp_bar; //Прогресс бар здоровья
        public static int HealthPlayer = 100; //Здоровье игрока
        public List<UIElement> elc; //Список элементов канваса
        DispatcherTimer gametimer; //Таймер игры

        public Player(Image player, Canvas CanvasGame, ProgressBar hp_bar, DispatcherTimer gametimer) //Конструктор класса
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.hp_bar = hp_bar;
            this.gametimer = gametimer;
        }
        public void kbup(object sender, KeyEventArgs e) //Кнопка поднята
        {
            if (Lose == true) //Если активно условие проигрыша, то управление не работает
            {
                return;
            }
            if (e.Key == Key.W) //Если кнопка W поднята, то игрок не двигается вверх
            {
                UpKeyDown = false; //Устанавливаем значение переменной в false
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
            if (e.Key == Key.W) //Если кнопка W опущена, то игрок двигается вверх
            {
                UpKeyDown = true; //Устанавливаем значение переменной в true
                ControlWeapon = "Up"; //Устанавливаем направление оружия вверх
            }
            if (e.Key == Key.S)
            {
                DownKeyDown = true;
                ControlWeapon = "Down";
            }
            if (e.Key == Key.A) //Если кнопка A опущена, то игрок двигается влево
            {
                LeftKeyDown = true; //Устанавливаем значение переменной в true
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_left.png", UriKind.RelativeOrAbsolute)); //Устанавливаем изображение игрока влево
                ControlWeapon = "Left"; //Устанавливаем направление оружия влево
            }
            if (e.Key == Key.D)
            {
                RightKeyDown = true;
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_right.png", UriKind.RelativeOrAbsolute));
                ControlWeapon = "Right";
            }
            if (e.Key == Key.LeftShift) //Если кнопка Shift опущена, то игрок ускоряется
            {
                Speed = 12 + Perks.Speed_boosting; //Устанавливаем скорость игрока в 12
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture\\Mob\\Player\\player_berserk.png", UriKind.RelativeOrAbsolute)); //Устанавливаем изображение игрока с ускорением
            }
        }
        public void Control() //Управление игроком
        {
            if (LeftKeyDown == true && Canvas.GetLeft(player) > 0) //Движения игрока
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - Speed); //Устанавливаем позицию игрока влево
            }

            if (RightKeyDown == true && Canvas.GetLeft(player) + player.Width < this.CanvasGame.Width) //Движения игрока
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + Speed); //Устанавливаем позицию игрока вправо
            }

            if (UpKeyDown == true && Canvas.GetTop(player) > 85)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - Speed); //Устанавливаем позицию игрока вверх
            }

            if (DownKeyDown == true && Canvas.GetTop(player) + player.Height < this.CanvasGame.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + Speed); //Устанавливаем позицию игрока вниз
            }
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            if (currentDifficulty == "Hard") //В зависимости от сложности, получаемое количество урона разное
            {
                if (Zombie.zombiesNeeded > 20) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Zombie.zombiesNeeded = 0; //Обнуляем счетчик убитых зомби
                }
                if (DarkWizard.wizardNeeded > 4) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    DarkWizard.wizardNeeded = 0;  //Обнуляем счетчик убитых магов
                }
                if (Skeleton.skeletonNeeded > 15) //Если убито 10 скелетов, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    Skeleton.skeletonNeeded = 0;  //Обнуляем счетчик убитых скелетов
                }
                hp_bar.Maximum = 100 + Perks.hp_boosting;
            }
            if (currentDifficulty == "Medium")
            {
                if (Zombie.zombiesNeeded > 10) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 15;
                    Zombie.zombiesNeeded = 0;  //Обнуляем счетчик убитых зомби
                }
                if (DarkWizard.wizardNeeded > 3) //Если убито 10 зомби, то игрок получает 10 здоровья
                {
                    HealthPlayer += 5;
                    DarkWizard.wizardNeeded = 0;  //Обнуляем счетчик убитых магов
                }
                if (Skeleton.skeletonNeeded > 10) //Если убито 10 скелетов, то игрок получает 10 здоровья
                {
                    HealthPlayer += 10;
                    Skeleton.skeletonNeeded = 0;  //Обнуляем счетчик убитых скелетов
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
                Lose = true; //Устанавливаем значение переменной в true
                player.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Player/player_berserk.png", UriKind.RelativeOrAbsolute)); 
                gametimer.Stop(); //Останавливаем таймер
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
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Weapon
{
    public class Sword
    {
        Image player;
        public int swordHorizontal;
        public int swordVertical;
        private int SpeedSword = 50; //Скорость волны меча
        private Image SwordWaves = new Image();
        private DispatcherTimer TimerSwordWeapon = new DispatcherTimer();
        double animationtick = 0;
        public void WaveSwordCreator(Canvas CanvasGame, Image player)
        {
            List<string> SwordWavesImages = new List<string>() { //Создаем лист из картинок для анимации при помощи таймера
                "pack://application:,,,/Texture/Weapon/Sword/Wave_0.png",
                "pack://application:,,,/Texture/Weapon/Sword/Wave_1.png",
                "pack://application:,,,/Texture/Weapon/Sword/Wave_2.png",
                "pack://application:,,,/Texture/Weapon/Sword/Wave_3.png",
                "pack://application:,,,/Texture/Weapon/Sword/Wave_4.png",
            }; int animationCurrentImage = 0;
            SwordWaves.Source = new BitmapImage(new Uri(SwordWavesImages[animationCurrentImage], UriKind.RelativeOrAbsolute));
            SwordWaves.Height = 256; SwordWaves.Width = 156; //задаем стандартные параметры для генерации волны меча, тег
            SwordWaves.Tag = "SwordAttack";
            this.player = player;
            Canvas.SetLeft(SwordWaves, Canvas.GetLeft(player) + player.Width / 2 - SwordWaves.Width / 2.5);
            Canvas.SetTop(SwordWaves, Canvas.GetTop(player) + player.Height / 2 - SwordWaves.Height / 2.5);
            Canvas.SetZIndex(SwordWaves, 1);
            CanvasGame.Children.Add(SwordWaves);
            TimerSwordWeapon.Interval = TimeSpan.FromMilliseconds(SpeedSword); //запускаем таймер со скоростью в указанной выше скорости
            TimerSwordWeapon.Tick += (sender, e) =>
            {
                animationCurrentImage = (animationCurrentImage + 1) % SwordWavesImages.Count;
                SwordWaves.Source = new BitmapImage(new Uri(SwordWavesImages[animationCurrentImage], UriKind.RelativeOrAbsolute)); //анимация воспроизводится со скоростью таймера
                SwordEvent(sender, e);
                animationtick++;
            };
            TimerSwordWeapon.Start();
        }
        public void SwordEvent(object sender, EventArgs w)
        {
                //сделай пропадание меча после анимации
                if (animationtick == 4)
                {
                    TimerSwordWeapon.Stop(); // при достижении определенной точки на канвасе таймер останавливается и сфера пропадает
                    SwordWaves.Source = null;
                    TimerSwordWeapon = null;
                }
            }
        }
    }

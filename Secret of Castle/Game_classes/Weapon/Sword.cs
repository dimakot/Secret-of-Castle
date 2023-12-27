using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Weapon
{
    public class Sword
    {
        public string ControlWeapon; //Мое изменение, благодаря этому стрингу мы и задаем направление удара 
        DispatcherTimer SwordTimer = new DispatcherTimer();
        List<Image> imagesTrash = new List<Image>(); //Список желательно вынести в powerWaveCreator ( Если ты планируешь делать анимации, если нет, то он не нужен )
        Image powerWave = new Image();
        public double playerVertical, playerHorizontal;
        private int SpeedSword = 100; // Мое изменение, здесь задаем скорость для таймера и соотвественно скорость оружия
        public void powerWaveCreator(Canvas CanvasGame)
        {   
            powerWave.Source = new BitmapImage(new Uri("Texture/Weapon/Sword/basicSword2", UriKind.RelativeOrAbsolute));
            powerWave.Width = 100;
            powerWave.Height = 100;
            powerWave.Tag = "Wave";
            Canvas.SetLeft(powerWave, playerHorizontal + 30); //Зачем +30?
            Canvas.SetTop (powerWave, playerVertical + 30);
            Canvas.SetZIndex(powerWave, 1);
            CanvasGame.Children.Add(powerWave);
            imagesTrash.Add(powerWave);
            //мои изменения, включаем таймер, ставим его скорость и задаем ему действие
            SwordTimer.Interval = TimeSpan.FromMilliseconds(SpeedSword); //запускаем таймер со скоростью в указанной выши
            SwordTimer.Tick += new EventHandler(SwordEvent);
        }
        // Где таймер??
        public void SwordEvent(object sender, EventArgs w)
        {
            //Дальше удачи :)
        }
    }
}

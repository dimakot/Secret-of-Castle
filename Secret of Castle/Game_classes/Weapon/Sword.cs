using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Secret_of_Castle.Game_classes.Weapon
{
    public class Sword
    {
        DispatcherTimer delayTimer = new DispatcherTimer();
        List<Image> imagesTrash = new List<Image>();
        Image powerWave = new Image();
        public double playerVertical, playerHorizontal;
        public void powerWaveCreator(Canvas CanvasGame)
        {   
            powerWave.Source = new BitmapImage(new Uri("Texture/Weapon/Sword/basicSword2", UriKind.RelativeOrAbsolute));
            powerWave.Width = 100;
            powerWave.Height = 100;
            powerWave.Tag = "Wave";
            Canvas.SetLeft(powerWave, playerHorizontal + 30);
            Canvas.SetTop (powerWave, playerVertical + 30);
            Canvas.SetZIndex(powerWave, 1);
            CanvasGame.Children.Add(powerWave);
            imagesTrash.Add(powerWave);
         
        }
    }
}

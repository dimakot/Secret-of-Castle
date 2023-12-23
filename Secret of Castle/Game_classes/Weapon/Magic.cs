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

namespace Secret_of_Castle
{
    internal class Magic
    {
        public string Controlmagic;
        public int MagicHorisontal;
        public int MagicVertical;
        private int SpeedMagic = 50;
        private Image MagicSphere = new Image();
        private DispatcherTimer TimerBasicMagicWeapon = new DispatcherTimer();
        public void SphereMagicNew(Canvas CanvasGame)
        {
            MagicSphere.Source = new BitmapImage(new Uri("Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_1.png", UriKind.RelativeOrAbsolute));
            MagicSphere.Height = 106; MagicSphere.Width = 70;
            MagicSphere.Tag = "BasicMagicSphere";
            Canvas.SetLeft(MagicSphere, MagicHorisontal); Canvas.SetTop(MagicSphere, MagicVertical);
            Canvas.SetZIndex(MagicSphere, 1);
            CanvasGame.Children.Add(MagicSphere);
            TimerBasicMagicWeapon.Interval = TimeSpan.FromMilliseconds(SpeedMagic); 
            TimerBasicMagicWeapon.Tick += new EventHandler(BasicMagicEvent);
            TimerBasicMagicWeapon.Start();
        }
        private void BasicMagicEvent(object sender, EventArgs w)
        {
            if (Controlmagic == "Left")
            {
                Canvas.SetLeft(MagicSphere, Canvas.GetLeft(MagicSphere) - SpeedMagic);
            }
            if (Controlmagic == "Right")
            {
                Canvas.SetLeft(MagicSphere, Canvas.GetLeft(MagicSphere) + SpeedMagic);
            }
            if (Controlmagic == "Down")
            {
                Canvas.SetTop(MagicSphere, Canvas.GetTop(MagicSphere) + SpeedMagic);
            }
            if (Controlmagic == "Up")
            {
                Canvas.SetTop(MagicSphere, Canvas.GetTop(MagicSphere) - SpeedMagic);
            }

            if (Canvas.GetLeft(MagicSphere) < 10 || Canvas.GetLeft(MagicSphere) > 2000 || Canvas.GetTop(MagicSphere) < 10 || Canvas.GetTop(MagicSphere) > 1000)
            {
                TimerBasicMagicWeapon.Stop();
                MagicSphere.Source = null;
                TimerBasicMagicWeapon = null;
            }
        }
    }
}
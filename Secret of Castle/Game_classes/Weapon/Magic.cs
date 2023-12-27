using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Secret_of_Castle
{
    internal class Magic
    {
        public string ControlWeapon;
        public int MagicHorisontal;
        public int MagicVertical;
        private int SpeedMagic = 50;
        private Image MagicSphere = new Image();
        private DispatcherTimer TimerBasicMagicWeapon = new DispatcherTimer();
        public void SphereMagicNew(Canvas CanvasGame)
        {
            List<string> MagicSphereImages = new List<string>() {
                "Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_1.png",
                "Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_2.png",
                "Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_3.png",
                "Texture/Weapon/Magic/BasicSphere/MagicSphereBasic_4.png"
            }; int animationCurrentImage = 0;
            MagicSphere.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute));
            MagicSphere.Height = 106; MagicSphere.Width = 70;
            MagicSphere.Tag = "BasicMagicSphere";
            Canvas.SetLeft(MagicSphere, MagicHorisontal); Canvas.SetTop(MagicSphere, MagicVertical);
            Canvas.SetZIndex(MagicSphere, 1);
            CanvasGame.Children.Add(MagicSphere);
            TimerBasicMagicWeapon.Interval = TimeSpan.FromMilliseconds(SpeedMagic);
            TimerBasicMagicWeapon.Tick += (sender, e) => {
                animationCurrentImage = (animationCurrentImage + 1) % MagicSphereImages.Count;
                MagicSphere.Source = new BitmapImage(new Uri(MagicSphereImages[animationCurrentImage], UriKind.RelativeOrAbsolute)); BasicMagicEvent(sender, e);
            };
            TimerBasicMagicWeapon.Start();
        }
        private void BasicMagicEvent(object sender, EventArgs w)
        {
            if (ControlWeapon == "Left")
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
                TimerBasicMagicWeapon.Stop();
                MagicSphere.Source = null;
                TimerBasicMagicWeapon = null;
            }
        }
    }
}
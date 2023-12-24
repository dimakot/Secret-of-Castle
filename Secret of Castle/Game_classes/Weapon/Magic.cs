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
/*        public void KillMob(Canvas CanvasGame, List<UIElement> elc)
        {
            foreach (UIElement j in elc)
            {
                foreach (UIElement u in elc)
                {
                    if (j is Image BasicMagSphere && (string)BasicMagSphere.Tag == "BasicMagicSphere" && u is Image ZombieMob && (string)ZombieMob.Tag == "Zombie") //Убийство мобов
                    {
                        if (Canvas.GetLeft(ZombieMob) < Canvas.GetLeft(BasicMagSphere) + BasicMagSphere.ActualWidth &&
                        Canvas.GetLeft(ZombieMob) + ZombieMob.ActualWidth > Canvas.GetLeft(BasicMagSphere) &&
                        Canvas.GetTop(ZombieMob) < Canvas.GetTop(BasicMagSphere) + BasicMagSphere.ActualHeight &&
                        Canvas.GetTop(ZombieMob) + ZombieMob.ActualHeight > Canvas.GetTop(BasicMagSphere))
                        {
                            CanvasGame.Children.Remove(BasicMagSphere);
                            BasicMagSphere.Source = null;
                            CanvasGame.Children.Remove(ZombieMob);
                            ZombieMob.Source = null;
                            zombieList.Remove(ZombieMob
                        }
                    }
                }
            }
        }*/
    }
}
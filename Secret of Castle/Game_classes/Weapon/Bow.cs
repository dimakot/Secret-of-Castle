using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Bow
{
    class Bow
    {
        public string ControlWeapon;
        public int BowHorisontal;
        public int BowVertical;
        private int SpeedBow = 100;
        private Image BowArrow = new Image();
        private DispatcherTimer TimerBowArrow = new DispatcherTimer();
        public void BowNew(Canvas CanvasGame)
        {
            BowArrow.Source = new BitmapImage(new Uri("Castle_1.jpeg", UriKind.RelativeOrAbsolute)); //Спрайт стрелы //ПОМЕНЯТЬ
            BowArrow.Height = 106; BowArrow.Width = 70;
            BowArrow.Tag = "BasicMagicSphere";
            Canvas.SetLeft(BowArrow, BowHorisontal); Canvas.SetTop(BowArrow, BowVertical);
            Canvas.SetZIndex(BowArrow, 1);
            CanvasGame.Children.Add(BowArrow);
            TimerBowArrow.Interval = TimeSpan.FromMilliseconds(SpeedBow);
            TimerBowArrow.Tick += new EventHandler(BowEvent);
            TimerBowArrow.Start();
        }
        private void BowEvent(object sender, EventArgs w)
        {
            if (ControlWeapon == "Left")
            {
                Canvas.SetLeft(BowArrow, Canvas.GetLeft(BowArrow) - SpeedBow);
            }
            if (ControlWeapon == "Right")
            {
                Canvas.SetLeft(BowArrow, Canvas.GetLeft(BowArrow) + SpeedBow);
            }
            if (ControlWeapon == "Down")
            {
                Canvas.SetTop(BowArrow, Canvas.GetTop(BowArrow) + SpeedBow);
            }
            if (ControlWeapon == "Up")
            {
                Canvas.SetTop(BowArrow, Canvas.GetTop(BowArrow) - SpeedBow);
            }

            if (Canvas.GetLeft(BowArrow) < 10 || Canvas.GetLeft(BowArrow) > 2000 || Canvas.GetTop(BowArrow) < 10 || Canvas.GetTop(BowArrow) > 1000)
            {
                TimerBowArrow.Stop();
                BowArrow.Source = null;
                TimerBowArrow = null;
            }
        }
    }
}

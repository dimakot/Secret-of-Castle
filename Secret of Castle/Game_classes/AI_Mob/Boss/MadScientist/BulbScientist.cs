using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Secret_of_Castle
{
    class BulbScientist
    {
        public string way;
        public int MadScientistHorisontal;
        public int MadScientistVertical;
        List<Image> ScientistList; //Список для моба
        private int SpeedMagicMadScientistAttack = 50; //Скорость магиского снаряда
        private Image MadScientistWeapon = new Image();
        private DispatcherTimer TimerMagicMadScientist = new DispatcherTimer();

        public void MadScientistMagicAttack(Canvas CanvasGame)
        {
            List<string> MadScientistAttackImages = new List<string>() { //Создаем лист из картинок для анимации при помощи таймера
                "pack://application:,,,/Texture/Weapon/Magic/MadScientistBulb/MadScientist_1.png",
                "pack://application:,,,/Texture/Weapon/Magic/MadScientistBulb/MadScientist_2.png",
                "pack://application:,,,/Texture/Weapon/Magic/MadScientistBulb/MadScientist_3.png",
            }; int animationCurrentImage = 0;
            MadScientistWeapon.Source = new BitmapImage(new Uri(MadScientistAttackImages[animationCurrentImage], UriKind.RelativeOrAbsolute));
            MadScientistWeapon.Height = 300; MadScientistWeapon.Width = 300; //задаем стандартные параметры для генерации Магической сферы, тег
            MadScientistWeapon.Tag = "BulbMagicAttack";
            Canvas.SetLeft(MadScientistWeapon, MadScientistHorisontal); Canvas.SetTop(MadScientistWeapon, MadScientistVertical);
            Panel.SetZIndex(MadScientistWeapon, 1);
            CanvasGame.Children.Add(MadScientistWeapon);
            TimerMagicMadScientist.Interval = TimeSpan.FromMilliseconds(SpeedMagicMadScientistAttack); //запускаем таймер со скоростью в указанной выши
            TimerMagicMadScientist.Tick += (sender, e) =>
            {
                animationCurrentImage = (animationCurrentImage + 1) % MadScientistAttackImages.Count;
                MadScientistWeapon.Source = new BitmapImage(new Uri(MadScientistAttackImages[animationCurrentImage], UriKind.RelativeOrAbsolute)); MadScientistDamage(sender, e); //анимация воспроизводится со скоростью таймера
            };
            TimerMagicMadScientist.Start();
        }
        public void MadScientistDamage(object sender, EventArgs w)
        {

        }
    }
}

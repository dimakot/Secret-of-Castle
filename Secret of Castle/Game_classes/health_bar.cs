using Control_player;
using Secret_of_Castle.Game_classes.IO_Mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
namespace Secret_of_Castle.Game_classes
{
    internal class health_bar
    {
        Image player;
        Image Zombies;
        ProgressBar hp_bar;
        public health_bar(Image player, Image Zombies, ProgressBar hp_bar)
        {
            this.player = player;
            this.Zombies = Zombies;
            this.hp_bar = hp_bar;
        }
        public void damage()
        {
            if (Canvas.GetLeft(player) + player.ActualWidth > Canvas.GetLeft(Zombies) && Canvas.GetLeft(player) < Canvas.GetLeft(Zombies) + Zombies.ActualWidth && Canvas.GetTop(player) < Canvas.GetTop(Zombies) + Zombies.ActualHeight && Canvas.GetTop(player) + player.ActualHeight > Canvas.GetTop(Zombies))
            {
                hp_bar.Value -= 0.5; //Если зомби прикосается к коллизии игрока, то из хп бара вычется 1 хп
                if (hp_bar.Value > 50)
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
}
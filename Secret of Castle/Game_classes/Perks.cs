using System.Numerics;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows;

namespace Secret_of_Castle
{
    internal class Perks
    {
        ProgressBar hp_bar;
        Random manna;
        Image player;
        Canvas CanvasGame;
        public List<UIElement> elc;
        public static int star = 10;
        public static int Speed_boosting = 0;
        Random Perksch = new Random();
        public static int hp_boosting = 0;
        public void Perks_choose()
        {
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            int Perkschance = Perksch.Next(1, 8);
            if (Perkschance == 1)
            {
                hp_boosting += 20;
            }
            if (Perkschance == 2)
            {
                Speed_boosting += 5;
            }
            if (Perkschance == 3)
            {
                Player.HealthPlayer -= 50;
            }
            if (Perkschance == 4)
            {
                Player.HealthPlayer -= 50;
            }
            if (Perkschance == 5)
            {
                Player.HealthPlayer -= 100;
            }
            if (Perkschance == 6)
            {
                Player.HealthPlayer += 50;
            }
            if (Perkschance == 7)
            {
                Player.HealthPlayer += 100;
            }
        }
    }
}

using System.Windows.Controls;

namespace Secret_of_Castle
{
    internal class Perks
    {
        ProgressBar hp_bar;
        public void HP_boost()
        {
            //увеличивает хп на 100
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //Получаем текущую сложность
            if (currentDifficulty == "Hard") //В зависимости от сложности, максимальное значение HP бара разное
            {
                hp_bar.Maximum = 100;
            }
            if (currentDifficulty == "Medium")
            {
                hp_bar.Maximum = 150;
            }
            if (currentDifficulty == "Lite")
            {
                hp_bar.Maximum = 200;
            }
        }
        public void Damage_boost()
        {
            //увеличивает урон на 10

        }
        public void Speed_boost()
        {
            //увеличивает скорость на 10

        }
    }
}

using System.Windows;
using Sound;

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Difficult_Select.xaml
    /// </summary>
    public partial class Difficult_Select : Window
    {
        public static Window Game_to;
        public Difficult_Select()
        {
            InitializeComponent();
        }

        private void Lite_Button_Click(object sender, RoutedEventArgs e) //Установка сложности
        {
            difficult.Instance.SetDifficulty("Lite"); //Установка сложности
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            To_Level.Visibility = Visibility.Visible;
            DifficultCurrentLB.Content = "Легкая";
            HP_PersonLB.Content = "Количество здоровья: 200";
            MobDifficultLB.Content = "Количество мобов: Низкое";
            Player.HealthPlayer = 200;
        }

        private void Medium_Button_Click(object sender, RoutedEventArgs e) //Установка сложности
        {
            difficult.Instance.SetDifficulty("Medium"); //Установка сложности
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            To_Level.Visibility = Visibility.Visible;
            DifficultCurrentLB.Content = "Нормальная";
            HP_PersonLB.Content = "Количество здоровья: 150";
            MobDifficultLB.Content = "Количество мобов: Среднее";
            Player.HealthPlayer = 150;
        }

        private void Hard_Button_Click(object sender, RoutedEventArgs e) //Установка сложности
        {
            difficult.Instance.SetDifficulty("Hard"); //Установка сложности
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            To_Level.Visibility = Visibility.Visible;
            DifficultCurrentLB.Content = "Сложная";
            HP_PersonLB.Content = "Количество здоровья: 100";
            MobDifficultLB.Content = "Количество мобов: Большое";
            Player.HealthPlayer = 100;
        }

        private void To_Level_Click(object sender, RoutedEventArgs e) //Переход на игровой уровень
        {
            SoundPlayerM.SoundMenuMain = 0;
            if (Game_to == null)
            {
                Game_to = new Game();
                Game_to.Show();
            }
            else
            {
                Game_to.Activate();
            }
            this.Hide();
        }
    }
}

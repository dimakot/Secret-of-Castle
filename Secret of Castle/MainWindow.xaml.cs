/*using Sound;*/
using System;
using System.Windows;
using System.Windows.Input;

namespace Secret_of_Castle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window Game_to;
        public MainWindow()
        {
/*            SoundPlayerM.SoundMenuMain = 1;*/
            InitializeComponent();
/*            SoundPlayerM.MusicMenu();*/

        }
        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Button_Play_MouseEnter(object sender, MouseEventArgs e) //При наведении на кнопку играть меняется картинка
        {
        }

        private void Button_Play_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void Settings_MouseEnter(object sender, MouseEventArgs e) //При наведении на кнопку настройки меняется цвет и размер шрифта
        {

        }
        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void Button_Exit_MouseEnter(object sender, MouseEventArgs e) //При наведении на кнопку выход меняется цвет и размер шрифта
        {

        }
        private void Button_Exit_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            DifficultCanvas.Visibility = Visibility.Visible;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
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

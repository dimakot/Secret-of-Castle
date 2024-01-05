using Sound;
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
            SoundPlayerM.SoundMenuMain = 1;
            InitializeComponent();
            SoundPlayerM.MusicMenu();

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
            /* Game To_Game = new Game();
               To_Game.Show(); //Показывает окно поверх второго*/
            if (Game_to == null)
            {
                Game_to = new Difficult_Select();
                Game_to.Show();
            }
            else
            {
                Game_to.Activate();
            }
            this.Hide();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}

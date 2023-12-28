using System.Windows;

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Pause.xaml
    /// </summary>
    public partial class Pause : Window
    {
        public Pause()
        {
            InitializeComponent();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            Settings To_Settings = new Settings();
            To_Settings.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

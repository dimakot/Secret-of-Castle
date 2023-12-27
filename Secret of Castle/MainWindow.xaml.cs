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
            InitializeComponent();

        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPos = new Point(e.GetPosition(relativeTo: this).X, e.GetPosition(relativeTo: this).Y);
        }

        private void Button_Play_MouseEnter(object sender, MouseEventArgs e) {

        }

        private void Button_Play_MouseLeave(object sender, MouseEventArgs e) {

        }

        private void Button_Play_Click(object sender, RoutedEventArgs e) {
            /* Game To_Game = new Game();
               To_Game.Show(); //Показывает окно поверх второго*/
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

        private void Button_Exit_Click(object sender, RoutedEventArgs e) {

            System.Windows.Application.Current.Shutdown();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings To_Settings = new Settings();
            To_Settings.Show();
        }
    }
}

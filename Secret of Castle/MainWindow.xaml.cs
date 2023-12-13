using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var SoundPlayerMenu = Application.GetResourceStream(new Uri("pack://application:,,,/Sound/Music/MainMenuTheme.wav"));
                using (var s = SoundPlayerMenu.Stream)
                {
                    System.Media.SoundPlayer MainMenuPlayer = new System.Media.SoundPlayer(s);
                    MainMenuPlayer.Load();
                    MainMenuPlayer.Play();
                }
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

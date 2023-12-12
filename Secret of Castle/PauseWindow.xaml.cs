using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Secret_of_Castle {
    /// <summary>
    /// Логика взаимодействия для Pause.xaml
    /// </summary>
    public partial class Pause : Window {
        public Pause() {
            InitializeComponent();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e) {
            Settings To_Settings = new Settings();
            To_Settings.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

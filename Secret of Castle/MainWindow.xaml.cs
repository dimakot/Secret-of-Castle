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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Play_MouseEnter(object sender, MouseEventArgs e) {

        }

        private void Button_Play_MouseLeave(object sender, MouseEventArgs e) {

        }

        private void Button_Play_Click(object sender, RoutedEventArgs e) {
         /*   Game To_Game = new Game();
            To_Game.Show(); //Показывает окно поверх второго*/

            var Game = new Game();
            Game.Owner = this; // Ставит окно Главным
            Game.ShowDialog(); // Показывает выбранное окно вместо первого 

        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e) {

            System.Windows.Application.Current.Shutdown();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

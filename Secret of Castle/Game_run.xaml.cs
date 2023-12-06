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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Game_run.xaml
    /// </summary>
    public partial class Game_run : Page
    {
        public Game_run()
        {
            InitializeComponent();
        }

        private void Player_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W && Player.Margin.Top > 0)
            {
                Player.Margin = new Thickness(Player.Margin.Left, Player.Margin.Top - 5, 0, 0);
            }
            if (e.Key == Key.A && Player.Margin.Left > 0)
            {
                Player.Margin = new Thickness(Player.Margin.Left - 5, Player.Margin.Top, 0, 0);
            }
            if (e.Key == Key.S && Player.Margin.Top < this.ActualHeight)
            {
                Player.Margin = new Thickness(Player.Margin.Left, Player.Margin.Top + 5, 0, 0);
            }
            if (e.Key == Key.D && Player.Margin.Left < this.ActualHeight)
            {
                Player.Margin = new Thickness(Player.Margin.Left + 5, Player.Margin.Top, 0, 0);
            }
        }
    }
}

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

namespace Secret_of_Castle.Level
{
    /// <summary>
    /// Логика взаимодействия для level1.xaml
    /// </summary>
    public partial class level1 : Window
    {
        public level1()
        {
            InitializeComponent();
        }
        private void kbup(object sender, KeyEventArgs e)
        {
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        { //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
            if (e.Key == Key.Escape)
            {
                Pause To_pause = new Pause();
                To_pause.Show();
            }
        }
    }
}

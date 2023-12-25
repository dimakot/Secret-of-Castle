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

namespace Secret_of_Castle
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Exit_Settings_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DifficultLite_Click(object sender, RoutedEventArgs e)
        {
            difficult.Instance.SetDifficulty("Lite");
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            Difficultlb.Content = currentDifficulty;
        }

        private void DifficultMedium_Click(object sender, RoutedEventArgs e)
        {
            difficult.Instance.SetDifficulty("Medium");
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            Difficultlb.Content = currentDifficulty;
        }

        private void DifficultHard_Click(object sender, RoutedEventArgs e)
        {
            difficult.Instance.SetDifficulty("Hard");
            string currentDifficulty = difficult.Instance.CurrentDifficulty;
            Difficultlb.Content = currentDifficulty;
        }
    }
}

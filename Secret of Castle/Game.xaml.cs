﻿using System;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
        }

        private void Game1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W && rect1.Margin.Top > 4) { }
            rect1.Margin = new Thickness (rect1.Margin.Left,rect1.Margin.Top (-5), 0,0);

        }
    }
}

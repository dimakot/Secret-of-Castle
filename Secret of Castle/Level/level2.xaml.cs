using Collision_class;
using Control_player;
using Secret_of_Castle.Game_classes.IO_Mob;
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
using System.Windows.Threading;

namespace Secret_of_Castle.Level
{
    /// <summary>
    /// Логика взаимодействия для level2.xaml
    /// </summary>
    public partial class level2 : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Player Player_Controller;
        int Speed = 7;
        Random rand = new Random();

        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
        }
        public level2()
        {
            InitializeComponent(); //Таймер
            Player_Controller = new Player(player, CanvasGame);
            CanvasGame.Focus();
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Tick += GameTickTimer;
            gametimer.Start();
        }
        private void Game1_KeyDown(object sender, KeyEventArgs e)
        { //По нажатию на кнопку Esc на клавиатуре открывается меню паузы
            if (e.Key == Key.Escape)
            {
                Pause To_pause = new Pause();
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e)
        {
            Player_Controller.Control(); //Движение игрока
        }
    }
}

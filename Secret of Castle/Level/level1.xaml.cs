using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Secret_of_Castle.Level;
using Weapon;

namespace Secret_of_Castle.Level
{
    /// <summary>
    /// Логика взаимодействия для level1.xaml
    /// </summary>
    public partial class level1 : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();
        Player Player_Controller;
        Random rand = new Random();
        public bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        public static int Speed = 7;
        public level1()
        {
            InitializeComponent(); //Таймер
            List<UIElement> elc = CanvasGame.Children.Cast<UIElement>().ToList();
            Player_Controller = new Player(player, CanvasGame, hp_bar);
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
            gametimer.Start();
        }
        private void kbup(object sender, KeyEventArgs e)
        {
            Player_Controller.kbup(sender, e); //Кнопка поднята
        }
        private void kbdown(object sender, KeyEventArgs e)
        {
            Player_Controller.kbdown(sender, e); //Кнопка опущена
            if (e.Key == Key.Escape)
            {
                Pause To_pause = new Pause();
                To_pause.Show();
            }
        }
        private void GameTickTimer(object sender, EventArgs e)
        {
            Player_Controller.Control(); //Движение игрока
/*                Rect playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.RenderSize.Width, player.RenderSize.Height);
                Rect portal = new Rect(Canvas.GetLeft(Portal), Canvas.GetTop(Portal), Portal.RenderSize.Width, Portal.RenderSize.Height); //Переход на другой уровень
                if (playerRect.IntersectsWith(portal))
                {
                    level1 ChangeLevel = new level1();
                    this.Hide(); // скрываем текущее окно
                    gametimer.Stop();
                    ChangeLevel.ShowDialog(); // показываем новое окно как диалоговое
                    this.Close(); // закрываем текущее окно после закрытия нового
                }*/
            }
        }
    }

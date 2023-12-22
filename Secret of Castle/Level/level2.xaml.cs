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
{   ///второй уровень, сюда можно засунуть рандомную генерацию
    /// <summary>
    /// Логика взаимодействия для level2.xaml
    /// </summary>
    public partial class level2 :  Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();
        private bool UpKeyDown, DownKeyDown, LeftKeyDown, RightKeyDown;
        Player Player_Controller;
        int Speed = 7;
        Random rand = new Random();
        //public List<Rectangle> wallList;
        //----------------------------------------------------
        int x, y, n = 0;
        public bool[,] coordinateWeb = new bool[19, 10];
        public void startWeb()
        {
            for (int i = 0; i < coordinateWeb.GetLength(0); i++)
            {
                for (int j = 0; j < coordinateWeb.GetLength(1); j++)
                {
                    coordinateWeb[i, j] = true;
                }
            }
        }
        //-------------------------------------------------------
        public void newHorisontalWall(int xx, int yy)
        {
            Rectangle rect = new Rectangle();
            rect.Tag = "objects";
            rect.Width = 50;
            rect.Height = 300;
            rect.Stroke = Brushes.Aqua;
            rect.Fill = Brushes.Coral;
            //wallList.Add(rect);
            CanvasGame.Children.Add(rect);
            Canvas.SetTop(rect, yy * 100);
            Canvas.SetLeft(rect, xx * 100);

        }
        public void wallBuilder()
        {

            if (coordinateWeb[x, y] == true && n < 2 && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newHorisontalWall(x, y);
                n++;
            }
            else if (coordinateWeb[x, y] == true && n == 2 && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newHorisontalWall(x++, y);
                n = 0;
            }

            else if (coordinateWeb[x, y + 1] == true && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newHorisontalWall(x, y += 1);
            }
            else if (coordinateWeb[x, y - 1] == true && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newHorisontalWall(x, y -= 1);
            }
            else if (coordinateWeb[x + 1, y] == true && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newVerticallWall(x += 1, y);
            }
            else if (coordinateWeb[x - 1, y] == true && x < coordinateWeb.GetLength(0) && y < coordinateWeb.GetLength(1))
            {
                newVerticallWall(x -= 1, y);
            }
        }
        public void newVerticallWall(int xx, int yy)
        {
            Rectangle rect = new Rectangle();
            rect.Tag = "objects";
            rect.Width = 100;
            rect.Height = 50;
            rect.Stroke = Brushes.Aqua;
            rect.Fill = Brushes.Coral;
            CanvasGame.Children.Add(rect);
            rect.Margin = new Thickness(xx * 100, yy * 100, 0, 0);
        }
        //--------------------------------------------------------
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
            Player_Controller = new Player(player, CanvasGame, hp_bar);
            CanvasGame.Focus();
            gametimer.Tick += new EventHandler(GameTickTimer);
            gametimer.Interval = TimeSpan.FromMilliseconds(10);
           
            gametimer.Start();
            //----------------
            startWeb();
            for (int i = 0; i < 10; i++)
            {
                wallBuilder();
           }
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
            player.Source = new BitmapImage(new Uri("Texture/Mob/Player/player_stand.png", UriKind.RelativeOrAbsolute));
            Player_Controller.Control(); //Движение игрока
        }
    }
}

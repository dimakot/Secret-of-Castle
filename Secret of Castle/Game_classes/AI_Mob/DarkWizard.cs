using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Secret_of_Castle
{
    internal class DarkWizard
    {
        List<Image> wizardList; //Список для моба
        Image player;
        Image Portal;
        Random rand = new Random();
        Canvas CanvasGame;
        public ProgressBar wizardHPBar;
        public int Speed_Wizard;
        public static int wizardKilles;
        public static Dictionary<Image, ProgressBar> HPbars = new Dictionary<Image, ProgressBar>();
        public Dictionary<Image, DateTime> LastTeleportTimes { get; set; }
        int delay = 2000; //Задержка
        int delayDamage = 1000; //Задержка урона
        public List<UIElement> elc;
        public DarkWizard(Image player, Canvas CanvasGame, List<Image> wizardList, List<UIElement> elc, int wizardKilles = 0) //Конструктор
        {
            this.player = player;
            this.CanvasGame = CanvasGame;
            this.wizardList = wizardList;
            this.elc = elc;
   
        }
        public void WizardSpawn() //Создаем мага
        {
            Image Wizards = new Image();
            Wizards.Tag = "Wizards";
            Wizards.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteRight.png", UriKind.RelativeOrAbsolute));
            // Генерируем случайные координаты для зомби
            int WizardCanvasTop, WizardCanvasLeft; //Координаты зомби
            do
            {
                WizardCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                WizardCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
            } while (Math.Abs(Canvas.GetLeft(player) - WizardCanvasLeft) < 400 && Math.Abs(Canvas.GetTop(player) - WizardCanvasTop) < 400); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
            Canvas.SetLeft(Wizards, WizardCanvasLeft);
            Canvas.SetTop(Wizards, WizardCanvasTop);
            wizardHPBar = new ProgressBar(); // Создаем ProgressBar для зомби
            wizardHPBar.Width = 150; wizardHPBar.Height = 10;
            wizardHPBar.Value = 100; wizardHPBar.Maximum = 100;
            Wizards.Height = 300; Wizards.Width = 220;
            wizardList.Add(Wizards);
            CanvasGame.Children.Add(Wizards);
            CanvasGame.Children.Add(wizardHPBar);
            HPbars.Add(Wizards, wizardHPBar);
            Canvas.SetZIndex(player, 1);
        }
        public void TeleportRandomly()
        {
            foreach (var wizard in wizardList)
            {
                int WizardCanvasTop, WizardCanvasLeft; //Координаты зомби
                do
                {
                    WizardCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200);
                    WizardCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
                } while (Math.Abs(Canvas.GetLeft(player) - WizardCanvasLeft) < 400 && Math.Abs(Canvas.GetTop(player) - WizardCanvasTop) < 400);
                Canvas.SetLeft(wizard, WizardCanvasLeft);
                Canvas.SetTop(wizard, WizardCanvasTop);

                Canvas.SetLeft(wizard, WizardCanvasLeft);
                Canvas.SetTop(wizard, WizardCanvasTop);
            }
        }

        public void WizardAI()
        {
            // ИИ мага
            foreach (UIElement w in elc)
            {
                DateTime currentTime = DateTime.Now;
                if (w is Image wizard && (string)wizard.Tag == "Wizards")
                {
                    if (!LastTeleportTimes.ContainsKey(wizard))
                    {
                        LastTeleportTimes[wizard] = currentTime;
                    }
                    if ((currentTime - LastTeleportTimes[wizard]).TotalMilliseconds > delay)
                    {
                        LastTeleportTimes[wizard] = currentTime;
                    }
                    LastTeleportTimes[wizard] = currentTime;
                    /*                        if (Canvas.GetLeft(i) > Canvas.GetLeft(player))
                                    {
                                        i.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteRight.png", UriKind.RelativeOrAbsolute));
                                    }
                                    if (Canvas.GetLeft(i) < Canvas.GetLeft(player))
                                    {
                                        i.Source = new BitmapImage(new Uri("pack://application:,,,/Texture/Mob/Enemy/DarkWizard/darkWizardLiteRight.png", UriKind.RelativeOrAbsolute));
                                    }*/
                    /*                if (DateTime.Now.Subtract(LastTeleportTime).TotalMilliseconds > delay)
                                    {
                                        LastTeleportTime = DateTime.Now;
                                        int WizardCanvasTop, WizardCanvasLeft; //Координаты зомби
                                        do
                                        {
                                            WizardCanvasLeft = rand.Next(0, Convert.ToInt32(CanvasGame.Width) - 200); //Использует случайное значение от 0 до крайней точки разрешения экрана
                                            WizardCanvasTop = rand.Next(85, Convert.ToInt32(CanvasGame.Height) - 200);
                                        } while (Math.Abs(Canvas.GetLeft(player) - WizardCanvasLeft) < 400 && Math.Abs(Canvas.GetTop(player) - WizardCanvasTop) < 400); //Пока расстояние между игроком и зомби меньше 800, зомби не спавнится
                                        Canvas.SetLeft(i, WizardCanvasLeft);
                                        Canvas.SetTop(i, WizardCanvasTop);
                                    }*/
                }
                }
        }
        public void GameLose()
        {
            foreach (Image i in wizardList) // при проигрыше игрока, лист с зомби чистится, мобы пропадают
            {
                CanvasGame.Children.Remove(i);

            }
            wizardList.Clear();
            string currentDifficulty = difficult.Instance.CurrentDifficulty; //В зависимости от выбранной сложности спавнится за раз определенное кол-во мобов
            if (currentDifficulty == "Hard")
            {
                for (int i = 0; i < 5; i++)
                {
                    WizardSpawn();
                }
            }
            else if (currentDifficulty == "Medium")
            {
                for (int i = 0; i < 3; i++)
                {
                    WizardSpawn();
                }
            }
            else if (currentDifficulty == "Lite")
            {
                for (int i = 0; i < 2; i++)
                {
                    WizardSpawn();
                }
            }
        }
    }
}

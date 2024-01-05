using System;
using System.Windows;

namespace Sound
{
    public static class SoundPlayerM
    {
        public static int SoundMenuMain = 0;
        public static void MusicMenu()
        {
            if (SoundMenuMain == 1)
            {
                var sri = Application.GetResourceStream(new Uri("pack://application:,,,/Sound/Music/MainMenuTheme.wav"));

                if (sri != null)
                {
                    using (var s = sri.Stream)
                    {
                        System.Media.SoundPlayer pl = new System.Media.SoundPlayer(s);
                        pl.Load();
                        pl.Play();
                    }
                }
            }
            //сделай остановку музыки при переходе на другое окно
            else if (SoundMenuMain == 0)
            {
                var sri = Application.GetResourceStream(new Uri("pack://application:,,,/Sound/Music/MainMenuTheme.wav"));

                if (sri != null)
                {
                    using (var s = sri.Stream)
                    {
                        System.Media.SoundPlayer pl = new System.Media.SoundPlayer(s);
                        pl.Load();
                        pl.Stop();
                    }
                }
            }
        }
    }

}

using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Settings : GameScreen
    {
        private string _title;

        private static Settings _instance;

        private Settings()
        {
            _title = "Settings";
        }

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        public override void Draw()
        {
            string volumeLabel = $"Music Volume: {AudioHandler.Instance.MusicVolume}";

            double titlteX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(_title, Font, FontSize) / 2;
            double titleY = GameConstants.WindowHeight / 4;

            double labelX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(volumeLabel, Font, FontSize) / 2;
            double labelY = GameConstants.WindowHeight / 2;

            SplashKit.DrawText(_title, Color.White, Font, FontSize, titlteX, titleY);
            SplashKit.DrawText(volumeLabel, Color.White, Font, FontSize, labelX, labelY);
        }
    }
}

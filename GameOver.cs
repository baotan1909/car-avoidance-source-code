using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class GameOver : IngameScreen
    {
        private static GameOver _instance;
        private GameOver() : base()
        {
            ScreenLabel = "Game Over";
        }
        public static GameOver Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameOver();
                }
                return _instance;
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}

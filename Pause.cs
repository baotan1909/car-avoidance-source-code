using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Pause : IngameScreen
    {
        private static Pause _instance;
        private Pause() : base()
        {
            ScreenLabel = "Pause";
        }
        public static Pause Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Pause();
                }
                return _instance;
            }
        }
        public override void Draw()
        {
            string instructionLabel = "Press P to resume the game";
            double instructionX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(instructionLabel, Font, FontSize) / 2;
            double instructionY = GameConstants.WindowHeight - 30;

            base.Draw();

            SplashKit.DrawText(instructionLabel, Color.White, Font, FontSize, instructionX, instructionY);
        }
    }
}
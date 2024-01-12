using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAvoidance;
using SplashKitSDK;

namespace CarAvoidance
{
    public class Credits : GameScreen
    {
        private static Credits _instance;
        private string _title;

        private Credits()
        {
            _title = "Credits";
        }

        public static Credits Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Credits();
                }
                return _instance;
            }
        }

        public override void Draw()
        {
            double titleX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(_title, Font, FontSize) / 2;
            double titleY = GameConstants.WindowHeight / 4;

            SplashKit.DrawText(_title, Color.White, Font, FontSize, titleX, titleY);

            double y = titleY + 100;
            const double lineSpacing = 50;
            int newFontSize = FontSize - 8;

            DrawWrappedText("Art: Cars by TMD Studios", 50, ref y, newFontSize, lineSpacing);
            DrawWrappedText("Music: The Nutcracker Suite 'March'\nREMIX by TPRMX", 50, ref y, newFontSize, lineSpacing);
        }

        private void DrawWrappedText(string text, double x, ref double y, int fontSize, double lineSpacing)
        {
            double maxWidth = GameConstants.WindowWidth - 50;
            double textWidth = SplashKit.TextWidth(text, Font, fontSize);

            if (textWidth > maxWidth)
            {
                string[] lines = text.Split('\n');
                foreach (var line in lines)
                {
                    string[] words = line.Split(' ');
                    string currentLine = "";

                    foreach (var word in words)
                    {
                        double currentLineWidth = SplashKit.TextWidth(currentLine + word, Font, fontSize);
                        if (currentLineWidth <= maxWidth)
                        {
                            currentLine += word + " ";
                        }
                        else
                        {
                            SplashKit.DrawText(currentLine, Color.White, Font, fontSize, x, y);
                            y += lineSpacing;
                            currentLine = word + " ";
                        }
                    }

                    SplashKit.DrawText(currentLine, Color.White, Font, fontSize, x, y);
                    y += lineSpacing;
                }
            }
            else
            {
                SplashKit.DrawText(text, Color.White, Font, fontSize, x, y);
                y += lineSpacing;
            }
        }
    }
}

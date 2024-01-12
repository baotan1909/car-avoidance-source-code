using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CarAvoidance;
using SplashKitSDK;

namespace CarAvoidance
{
    public class IngameScreen : GameScreen
    {

        private double _buttonWidth, _buttonHeight, _centerX, _centerY;
        private string _screenLabel;

        public string ScreenLabel
        {
            get { return _screenLabel; }
            set { _screenLabel = value; }
        }
        public IngameScreen() 
        {

            _buttonWidth = 200;
            _buttonHeight = 50;
            _centerX = GameConstants.WindowWidth / 2;
            _centerY = GameConstants.WindowHeight / 2;
        }

        private void DrawLabels(string label, double y)
        {
            double x = _centerX - SplashKit.TextWidth(label, Font, FontSize) / 2;
            SplashKit.DrawText(label, Color.White, Font, FontSize, x, y);
        }

        private void DrawButton(string label, double buttonY, double labelY) 
        {
            double x = _centerX - SplashKit.TextWidth(label, Font, FontSize) / 2;
            SplashKit.FillRectangle(Color.Gray, _centerX - _buttonWidth / 2, buttonY, _buttonWidth, _buttonHeight);
            SplashKit.DrawText(label, Color.Black, Font, FontSize, x, labelY);
        }

        public override void Draw()
        {
            string scoreLabel = $"Score: {ScoreHandler.Instance.Score}";
            string hiScoreLabel = $"Hi-Score: {ScoreHandler.Instance.HiScore}";

            double y1 = _centerY - 100;
            double y2 = _centerY - 50;
            double y3 = _centerY;

            double btnY1 = _centerY + 30;
            double btnY2 = _centerY + 90;
            double lblY1 = _centerY + 40;
            double lblY2 = _centerY + 100;

            DrawLabels(scoreLabel, y1);
            DrawLabels(hiScoreLabel, y2);
            DrawLabels(_screenLabel, y3);
        
            DrawButton("Restart", btnY1, lblY1);
            DrawButton("Menu", btnY2, lblY2);
        }

        public bool IsMouseOverRestart(Point2D pt)
        {
            double btnY = _centerY + 30;
            double btnX = _centerX - _buttonWidth / 2;

            if (pt.X >= btnX 
                && pt.X <= btnX + _buttonWidth 
                && pt.Y >= btnY 
                && pt.Y <= btnY + _buttonHeight)
            {
                return true;
            }
            return false;
        }

        public bool IsMouseOverMenu(Point2D pt)
        {
            double btnY = _centerY + 90;
            double btnX = _centerX - _buttonWidth / 2;

            if (pt.X >= btnX 
                && pt.X <= btnX + _buttonWidth 
                && pt.Y >= btnY 
                && pt.Y <= btnY + _buttonHeight)
            {
                return true;
            }
            return false;
        }
    }
}

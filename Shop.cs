using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Shop : GameScreen
    {
        private string _title;
        private static Shop _instance;

        private Shop()
        {
            _title = "Shop";
        }

        public static Shop Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Shop();
                }
                return _instance;
            }
        }
        public void Buy()
        {
            if (!SplashKit.MouseClicked(MouseButton.LeftButton))
                return;

            for (int i = 0; i < 3; i++)
            {
                double startX = 135 + i * 115;
                double centerY = GameConstants.WindowHeight / 2;

                if (HoverSkill(SplashKit.MousePosition(), startX, centerY))
                {
                    string powerupText = GetPowerupText(i).Replace("\n", "");
                    int cost = GetPowerupCost(i);

                    if (CoinManager.Instance.CoinAmount < cost)
                    {
                        break;
                    }

                    ISkill skill = SkillManager.Instance.Skills.Values.FirstOrDefault(s => s.Name == powerupText);

                    if (skill != null && skill.Amount >= 99)
                    {
                        return;
                    }

                    CoinManager.Instance.DeductCoins(cost);

                    if (skill != null)
                    {
                        skill.Amount += 1;
                    }

                    break;
                }
            }
        }

        public override void Draw()
        {
            string coinLabel = $"Coin: {CoinManager.Instance.CoinAmount.ToString("D4")}";
            StringBuilder updatedCoinText = new StringBuilder("Coin: ");

            double centerX = GameConstants.WindowWidth / 2;
            double centerY = GameConstants.WindowHeight / 2;

            double titlteX = centerX - SplashKit.TextWidth(_title, Font, FontSize) / 2;
            double titleY = centerY / 2;

            double labelX = centerX - SplashKit.TextWidth(coinLabel, Font, FontSize) / 2;
            double labelY = centerY / 4;

            double squareWidth = 100;
            double textY = centerY + squareWidth / 2 - (SplashKit.TextHeight("Sample Text", Font, FontSize - 8) / 2);

            SplashKit.DrawText(_title, Color.White, Font, FontSize, titlteX, titleY);
            SplashKit.DrawText(coinLabel, Color.White, Font, FontSize - 8, labelX, labelY);

            for (int i = 0; i < 3; i++)
            {
                double startX = 135 + i * 115;
                double squareCenterX = startX + squareWidth / 2;

                SplashKit.FillRectangle(HoverColor(SplashKit.MousePosition(), startX, centerY), startX, centerY, squareWidth, squareWidth);

                DrawMultilineText(GetPowerupText(i), squareCenterX, textY);

                string costLabel = $"Cost\n{GetPowerupCost(i)}";

                double costY = centerY + squareWidth + 30;

                DrawMultilineText(costLabel, squareCenterX, costY);

                string powerupText = GetPowerupText(i).Replace("\n", "");
                ISkill skill = SkillManager.Instance.Skills.Values.FirstOrDefault(s => s.Name == powerupText);

                string amountLabel = skill != null
                    ? $"Amount\n{skill.Amount}"
                    : "Amount\n0";

                double amountY = costY + 50;
                DrawMultilineText(amountLabel, squareCenterX, amountY);
            }
        }
        private void DrawMultilineText(string text, double centerX, double textY)
        {
            string[] lines = text.Split(new[] { '\n' });

            double currentTextY = textY - (lines.Length - 1) * (SplashKit.TextHeight("Sample Text", Font, FontSize - 8) / 2);

            foreach (string line in lines)
            {
                double textX = centerX - SplashKit.TextWidth(line, Font, FontSize - 8) / 2;

                SplashKit.DrawText(line, Color.White, Font, FontSize - 8, textX, currentTextY);

                currentTextY += SplashKit.TextHeight("Sample Text", Font, FontSize - 8);
            }
        }
        private string GetPowerupText(int index)
        {
            switch (index)
            {
                case 0:
                    return "Slow\nDown";
                case 1:
                    return "Time\nStop";
                case 2:
                    return "Invin\ncible";
                default:
                    return string.Empty;
            }
        }
        private int GetPowerupCost(int index)
        {
            switch (index)
            {
                case 0:
                    return 500;
                case 1:
                    return 2000;
                case 2:
                    return 4999;
                default:
                    return 0;
            }
        }
        private bool HoverSkill(Point2D pt, double btnX, double btnY)
        {

            if (pt.X >= btnX
                && pt.X <= btnX + 100
                && pt.Y >= btnY
                && pt.Y <= btnY + 100)
            {
                return true;
            }
            return false;
        }
        private Color HoverColor(Point2D pt, double btnX, double btnY)
        {
            if(HoverSkill(pt, btnX, btnY))
            {
                return Color.DarkGray;
            }    
            return Color.Gray;
        }
    }
}

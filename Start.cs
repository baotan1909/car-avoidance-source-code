using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAvoidance;
using SplashKitSDK;

namespace CarAvoidance
{
    public class Start : GameScreen
    {
        private static Start _instance;

        private Bitmap _instructionBitmap;
        private int _titleFontSize, _selectedBtn;
        private string _title;
        private double _instructionX, _instructionY;
        private List<string> _buttons;

        public int SelectedBtn
        {
            get { return _selectedBtn; }
            set { _selectedBtn = value; }
        }
        public List<string> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }
        private Start()
        {
            _titleFontSize = 48;
            _title = "Car Avoidance";
            _buttons = new List<string>() { "Play", "Shop", "Settings", "Credits", "Quit" };
            _selectedBtn = 0;
            _instructionBitmap = SplashKit.BitmapNamed("ClickableInstruction");
            _instructionX = 80;
            _instructionY = (GameConstants.WindowHeight / 2) - (_instructionBitmap.Height / 2);
        }
        public static Start Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Start();
                }
                return _instance;
            }
        }
        public override void Draw()
        {
            double titleX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(_title, Font, _titleFontSize) / 2;
            double titleY = GameConstants.WindowHeight / 4;
            SplashKit.DrawText(_title, Color.White, Font, _titleFontSize, titleX, titleY);
            
            for (int i = 0; i < _buttons.Count; i++)
            {
                Color btnColor = (i == _selectedBtn) ? Color.Red : Color.White;
                double btnX = GameConstants.WindowWidth / 2 - SplashKit.TextWidth(_buttons[i], Font, FontSize) / 2;
                double btnY = GameConstants.WindowHeight / 2 + i * 50;

                SplashKit.DrawText(_buttons[i], btnColor, Font, FontSize, btnX, btnY);
            }

            _instructionBitmap.Draw(_instructionX, _instructionY);
        }

        public bool IsMouseOverInstruction(Point2D pt)
        {
            double mouseX = pt.X;
            double mouseY = pt.Y;
            if (mouseX > _instructionX &&
                   mouseX < _instructionX + _instructionBitmap.Width &&
                   mouseY > _instructionY &&
                   mouseY < _instructionY + _instructionBitmap.Height)
            {
                return true;
            }
            return false;
        }
    }
}

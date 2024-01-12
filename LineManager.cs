using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAvoidance;
using SplashKitSDK;

namespace CarAvoidance
{
    public class LineManager
    {
        private static LineManager _instance;
        private const int NumLinesPerRow = 3;
        private List<Line> _lines;

        private LineManager(int windowWidth)
        {
            _lines = new List<Line>();
            InitializeLines(windowWidth);
        }

        public static LineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LineManager(GameConstants.WindowWidth);
                }
                return _instance;
            }
        }

        private void InitializeLines(int windowWidth)
        {
            for (int i = 1; i <= NumLinesPerRow; i++)
            {
                double x = windowWidth / (NumLinesPerRow + 1) * i;
                _lines.Add(new Line(x, 30));
                _lines.Add(new Line(x, 150));
                _lines.Add(new Line(x, 270));
                _lines.Add(new Line(x, 390));
            }
        }

        public void Update(double y)
        {
            foreach (var line in _lines)
            {
                line.Move(y);
            }
        }

        public void Draw()
        {
            foreach (var line in _lines)
            {
                line.Draw();
            }
        }
    }
}

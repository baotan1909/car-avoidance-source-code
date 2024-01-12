using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Line : GameObject
    {
        public Line(double x, double y) : base(x, y) { }

        public void Move(double y)
        {
            Y += y;

            if (Y > GameConstants.WindowHeight)
            {
                Y = 30;
            }
        }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color.White, X, Y, 10, 50);
        }
    }
}

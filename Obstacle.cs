using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Obstacle : GameObject
    {
        private Bitmap _obBitmap;

        public Bitmap ObBitmap
        {
            get { return _obBitmap; }
        }

        public Obstacle(double x, double y) : base(x, y)
        {
            int random = 0;
            int previousRandom = 0;
            do
            {
                random = SplashKit.Rnd(1, 6);
            } while (random == previousRandom);

            previousRandom = random;
            _obBitmap = SplashKit.BitmapNamed("Ob" + random);
        }
        public void Move(double y)
        {
            Y += y;
        }
        public override void Draw()
        {
            _obBitmap.Draw(X, Y);
        }
    }
}

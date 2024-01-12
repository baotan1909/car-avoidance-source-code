using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Coin : GameObject
    {
        private Bitmap _coinBitmap;

        public Bitmap CoinBitmap 
        { 
            get { return _coinBitmap; } 
        }

        public Coin(double x, double y) : base(x, y)
        {
            _coinBitmap = SplashKit.BitmapNamed("Coin");
        }

        public void Move(double y)
        {
            Y += y;
        }

        public override void Draw()
        {
            _coinBitmap.Draw(X, Y);
        }
    }
}

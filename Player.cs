using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAvoidance;
using SplashKitSDK;
using static System.Formats.Asn1.AsnWriter;

namespace CarAvoidance
{
    public class Player : GameObject
    {
        private Bitmap _playerBitmap;

        public Bitmap PlayerBitmap
        {
            get { return _playerBitmap; }
        }

        public Player() : base()
        {
            X = GameConstants.WindowWidth / 2;
            Y = GameConstants.WindowHeight - 50;
            _playerBitmap = SplashKit.BitmapNamed("Player");
        }

        public void Move(double x, double y)
        {
            X += x;
            Y += y;
        }

        public override void Draw()
        {
            _playerBitmap.Draw(X, Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CarAvoidance
{
    public class Instruction : GameScreen
    {
        private static Instruction _instance;
        private Bitmap _instructionBitmap;

        private Instruction()
        {
            _instructionBitmap = SplashKit.BitmapNamed("Instruction");
        }

        public static Instruction Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Instruction();
                }
                return _instance;
            }
        }

        public override void Draw()
        {
            _instructionBitmap.Draw(0, 0);
        }
    }
}

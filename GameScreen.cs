using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public abstract class GameScreen
    {
        private int _fontSize;
        private string _font;

        public GameScreen()
        {
            _font = "fonts\\Minecraft.ttf";
            _fontSize = 32;
        }

        public string Font
        { 
            get { return _font; } 
        }
        public int FontSize
        { 
           get {  return _fontSize; } 
        }

        public abstract void Draw();
    }
}

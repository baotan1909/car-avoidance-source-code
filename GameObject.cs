using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public abstract class GameObject
    {
        private double _x;
        private double _y;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public GameObject() 
        { }

        public GameObject(double x, double y)
        {
            _x = x;
            _y = y;
        }
        public abstract void Draw();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance.configuration
{
    public class GameConfiguration
    {
        private double _speed;

        private bool _isObstacleCollisionChecked;

        public double Speed { get => _speed; set => _speed = value; }
        public bool IsObstacleCollisionChecked { get => _isObstacleCollisionChecked; set => _isObstacleCollisionChecked = value; }
    }
}

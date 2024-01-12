using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class ObstacleManager
    {
        private static ObstacleManager _instance;
        private List<Obstacle> _obstacles;

        private ObstacleManager() 
        {
            _obstacles = new List<Obstacle>();
        }

        public static ObstacleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObstacleManager();
                }
                return _instance;
            }
        }

        public void Update(double y)
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                obstacle.Move(y);
            }

            _obstacles.RemoveAll(obstacle => obstacle.Y >= GameConstants.WindowHeight);


            if (_obstacles.Count == 0 || _obstacles[_obstacles.Count - 1].Y >= GameConstants.WindowHeight / 2)
            {
                AddObstacle();
            }
        }
        public void AddObstacle()
        {
            double x;
            double previousX = 0;

            do
            {
                x = SplashKit.Rnd(130, 455);
            } while (x == previousX);

            previousX = x;
            double y = 30;

            Obstacle newObstacle = new Obstacle(x, y);
            _obstacles.Add(newObstacle);
        }
        public void ClearObstacles()
        {
            _obstacles.Clear();
        }
        public bool CollideWith(Player player)
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.ObBitmap.BitmapCollision(obstacle.X, obstacle.Y, player.PlayerBitmap, player.X, player.Y))
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.Draw();
            }
        }
    }
}

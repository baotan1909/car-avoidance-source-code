using System;
using CarAvoidance.configuration;
using SplashKitSDK;

namespace CarAvoidance
{
    public class Program
    {
        public static void Main()
        {
            GameConfiguration config = new GameConfiguration();
            config.Speed = 0;
            config.IsObstacleCollisionChecked = true;
            Game game = new Game(config);
            game.Update();
        }
    }
}

using CarAvoidance.configuration;
using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public static class GameConstants
    {
        public const int WindowWidth = 600;
        public const int WindowHeight = 510;
        public const int MaxScore = 999999999;
        public const int MaxCoin = 9999;
    }

    public class Game
    {
        private Window _gameWindow;
        private GameScreen _currentScreen;
        private AudioHandler _audioHandler;
        private ScoreHandler _scoreHandler;
        private Player _player;
        private LineManager _lineManager;
        private ObstacleManager _obstacleManager;
        private CoinManager _coinManager;
        private SkillManager _skillManager;
        private GameConfiguration _configuration;

        public Game(GameConfiguration configuration)
        {
            _gameWindow = new Window("Car Avoidance", GameConstants.WindowWidth, GameConstants.WindowHeight);
            Load();
            _audioHandler = AudioHandler.Instance;
            _scoreHandler = ScoreHandler.Instance;
            _currentScreen = Start.Instance;
            _player = new Player();
            _lineManager = LineManager.Instance;
            _obstacleManager = ObstacleManager.Instance;
            _coinManager = CoinManager.Instance;
            _skillManager = SkillManager.Instance;
            _configuration = configuration;
        }
        private void Load()
        {
            try
            {
                Directory.CreateDirectory("game");
                SplashKit.LoadMusic("Nutcracker", @"audio\Nutcracker REMIX.mp3");
                SplashKit.LoadBitmap("Player", @"images\player.png");
                SplashKit.LoadBitmap("Coin", @"images\coin.png");
                for (int i = 1; i <= 5; i++)
                {
                    string obBitmapName = $"Ob{i}";
                    string obBitmapPath = $@"images\ob{i}.png";
                    SplashKit.LoadBitmap(obBitmapName, obBitmapPath);
                }
                SplashKit.LoadBitmap("ClickableInstruction", @"images\instruction_rect.png");
                SplashKit.LoadBitmap("Instruction", @"images\instruction.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during loading: {ex.Message}");
            }
        }
        public void Update()
        {
            while (!_gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();

                _audioHandler.Musicloop();

                if (SplashKit.KeyTyped(KeyCode.DownKey) && _currentScreen is Start startScreen)
                {
                    if (startScreen.SelectedBtn < startScreen.Buttons.Count - 1)
                    {
                        startScreen.SelectedBtn++;
                    }
                }

                else if (SplashKit.KeyTyped(KeyCode.UpKey) && _currentScreen is Start startScreenUp)
                {
                    if (startScreenUp.SelectedBtn > 0)
                    {
                        startScreenUp.SelectedBtn--;
                    }
                }

                if (_currentScreen is Start startScreenEnter && (SplashKit.KeyTyped(KeyCode.KeypadEnter) || SplashKit.KeyTyped(KeyCode.ReturnKey)))
                {
                    switch (startScreenEnter.Buttons[startScreenEnter.SelectedBtn])
                    {
                        case "Play":
                            _currentScreen = null;
                            break;
                        case "Shop":
                            _currentScreen = Shop.Instance;
                            break;
                        case "Settings":
                            _currentScreen = Settings.Instance;
                            break;
                        case "Credits":
                            _currentScreen = Credits.Instance;
                            break;
                        case "Quit":
                            CloseGameWindow();
                            return;
                    }
                }

                if (_currentScreen is Start startScreenMouseOver && SplashKit.MouseClicked(MouseButton.LeftButton) && startScreenMouseOver.IsMouseOverInstruction(SplashKit.MousePosition()))
                {
                    _currentScreen = Instruction.Instance;
                }

                if (_currentScreen is Instruction instructionScreen && SplashKit.KeyTyped(KeyCode.EscapeKey))
                {
                    _currentScreen = Start.Instance;
                }

                if (_currentScreen is Shop shop)
                {
                    shop.Buy();
                    if (SplashKit.KeyTyped(KeyCode.EscapeKey))
                    {
                        _currentScreen = Start.Instance;
                    }
                }

                if (_currentScreen is Settings settingsScreen)
                {
                    if (SplashKit.KeyTyped(KeyCode.UpKey) && _audioHandler.MusicVolume < 100)
                    {
                        _audioHandler.AdjustMusicVolume(10);
                    }
                    else if (SplashKit.KeyTyped(KeyCode.DownKey) && _audioHandler.MusicVolume > 0)
                    {
                        _audioHandler.AdjustMusicVolume(-10);
                    }
                    else if (SplashKit.KeyTyped(KeyCode.EscapeKey))
                    {
                        _currentScreen = Start.Instance;
                    }
                }

                if (_currentScreen is Credits creditsScreen && SplashKit.KeyTyped(KeyCode.EscapeKey))
                {
                    _currentScreen = Start.Instance;
                }

                if (_currentScreen is GameOver gameOverScreen)
                {
                    if (SplashKit.MouseClicked(MouseButton.LeftButton) && gameOverScreen.IsMouseOverMenu(SplashKit.MousePosition()))
                    {
                        BackToMenu();
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && gameOverScreen.IsMouseOverRestart(SplashKit.MousePosition()))
                    {
                        RestartGame();
                    }
                }

                if (_currentScreen is Pause pauseScreen)
                {
                    if (SplashKit.KeyTyped(KeyCode.PKey))
                    {
                        _currentScreen = null;
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && pauseScreen.IsMouseOverMenu(SplashKit.MousePosition()))
                    {
                        BackToMenu();
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && pauseScreen.IsMouseOverRestart(SplashKit.MousePosition()))
                    {
                        RestartGame();
                    }
                }

                else if (_currentScreen == null && SplashKit.KeyTyped(KeyCode.PKey))
                {
                    _currentScreen = Pause.Instance;
                }

                else if (_currentScreen == null)
                {
                    _configuration.Speed = 5 + _scoreHandler.Score / 1000;

                    _skillManager.Update(_configuration);

                    _lineManager.Update(_configuration.Speed);

                    _obstacleManager.Update(_configuration.Speed);

                    _coinManager.Update(_player, _configuration.Speed);

                    _scoreHandler.ScoreHandle();

                    if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < 455)
                    {
                        _player.Move(5, 0);

                    }
                    if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 125)
                    {
                        _player.Move(-5, 0);
                    }
                    if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < GameConstants.WindowHeight - _player.PlayerBitmap.Height)
                    {
                        _player.Move(0, 5);

                    }
                    if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y - _player.PlayerBitmap.Height > 0)
                    {
                        _player.Move(0, -5);
                    }

                    CollisionCheck(_player);
                }
                Draw();
            }
            _skillManager.SaveSkillAmounts();
        }

        private void CloseGameWindow()
        {
            SplashKit.CloseWindow(_gameWindow);

        }
        private void CollisionCheck(Player player)
        {
            try
            {
                if (!_configuration.IsObstacleCollisionChecked) return;
                if (_obstacleManager.CollideWith(player))
                {
                    _currentScreen = GameOver.Instance;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during collision check: {ex.Message}");
            }
        }
        private void RestartGame()
        {
            ResetGameComponents();
            _currentScreen = null;
        }
        private void BackToMenu()
        {
            ResetGameComponents();
            _currentScreen = Start.Instance;
        }
        private void ResetGameComponents()
        {
            _player = new Player();
            _obstacleManager.ClearObstacles();
            _coinManager.ClearCoin();
            _scoreHandler.Score = 0;
            _scoreHandler.IsPlayedBefore = true;
            _scoreHandler.HasDisplayedHiScoreMessage = false;
            _skillManager.StopSkillEffects();
        }
        private void Draw()
        {
            SplashKit.ClearScreen(Color.Black);
            if (_currentScreen != null)
            {
                _currentScreen.Draw();
            }
            else
            {
                BackGround();

                _lineManager.Draw();

                _coinManager.Draw();

                _obstacleManager.Draw();

                _player.Draw();

                _skillManager.Draw();

                _scoreHandler.Draw();
            }
            SplashKit.RefreshScreen(60);
        }
        private void BackGround()
        {
            SplashKit.FillRectangle(Color.DarkOliveGreen, 0, 0, 130, GameConstants.WindowHeight);
            SplashKit.FillRectangle(Color.DarkGray, 120, 0, 10, GameConstants.WindowHeight);
            SplashKit.FillRectangle(Color.DarkOliveGreen, GameConstants.WindowWidth - 115, 0, GameConstants.WindowWidth, GameConstants.WindowHeight);
            SplashKit.FillRectangle(Color.DarkGray, GameConstants.WindowWidth - 115, 0, 10, GameConstants.WindowHeight);
            SplashKit.FillRectangle(Color.Black, 0, 0, GameConstants.WindowWidth, 30);
        }
    }
}
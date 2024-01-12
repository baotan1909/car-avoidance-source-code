using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CarAvoidance
{
    public class ScoreHandler
    {
        private static ScoreHandler _instance;

        private SplashKitSDK.Timer _hiScoreTimer;
        private int _score, _hiScore, _fontSize;
        private bool _isNewHiScore, _hasDisplayedHiScoreMessage, _isPlayedBefore;
        private string _font, _scoreTxt;
        private string _highScoreFilePath = @"game\high_score.txt";

        private ScoreHandler()
        {
            _font = "fonts\\Minecraft.ttf";
            _fontSize = 20;
            _hiScoreTimer = SplashKit.CreateTimer("fiveSec");
            _hasDisplayedHiScoreMessage = false;
            LoadHighScore();
            UpdateScoreText();
        }

        public static ScoreHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreHandler();
                }
                return _instance;
            }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value < 0 ? 0 : Math.Min(value, GameConstants.MaxScore); }
        }

        public int HiScore
        {
            get { return _hiScore; }
        }
        public bool HasDisplayedHiScoreMessage
        {
            set {  _hasDisplayedHiScoreMessage = value;}
            get { return _hasDisplayedHiScoreMessage;}
        }

        public bool IsPlayedBefore
        {
            set { _isPlayedBefore = value; }
            get { return _isPlayedBefore; }
        }
        public void ScoreHandle()
        {
            _score = Math.Min(_score + 1, GameConstants.MaxScore);

            if (_score >= _hiScore)
            {
                _hiScore = _score;
                UpdateHiScore();
            }

            UpdateScoreText();
        }

        private void LoadHighScore()
        {
            if (File.Exists(_highScoreFilePath))
            {
                string highScoreText = File.ReadAllText(_highScoreFilePath);
                if (int.TryParse(highScoreText, out int loadedHiScore))
                {
                    _hiScore = Math.Min(Math.Max(loadedHiScore, 0), GameConstants.MaxScore);
                    _isPlayedBefore = true;
                }
                else
                {
                    _hiScore = 0;
                    _isPlayedBefore = false;
                }
            }
            else
            {
                _isPlayedBefore = false;
                _hiScore = 0;
            }
        }

        private void UpdateScoreText()
        {
            if (_score >= _hiScore && _isPlayedBefore && !_hasDisplayedHiScoreMessage)
            {
                _isNewHiScore = true;
                _isPlayedBefore = false;
                _hasDisplayedHiScoreMessage = true;
                SplashKit.StartTimer("fiveSec");
            }

            if (_isNewHiScore)
            {
                _scoreTxt = "You've reached a new hi-score!";
                if (_hiScoreTimer.Ticks / 1000 == 1)
                {
                    SplashKit.StopTimer(_hiScoreTimer);
                    _isNewHiScore = false;
                }
            }
            else
            {
                string scoreString = _score.ToString("D9");
                StringBuilder updatedScoreText = new StringBuilder("Score: ");

                for (int i = 0; i < 9; i++)
                {
                    updatedScoreText.Append(scoreString[i]);
                    if (i < 8)
                    {
                        updatedScoreText.Append(" ");
                    }
                }

                _scoreTxt = updatedScoreText.ToString();
            }
        }

        private void UpdateHiScore()
        {
            File.WriteAllText(_highScoreFilePath, _hiScore.ToString());
        }

        public void Draw()
        {
            double scoreTxtX = 290;
            double scoreTxtY = 10;
            
            SplashKit.DrawText(_scoreTxt, Color.White, _font, _fontSize, scoreTxtX, scoreTxtY);
        }
    }
}

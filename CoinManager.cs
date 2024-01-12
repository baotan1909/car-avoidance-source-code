using CarAvoidance;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class CoinManager
    {
        private static CoinManager? _instance;
        private int _coinAmount, _fontSize;
        private List<Coin> _coins;
        private string? _font, _coinTxt;
        private string _coinAmountFilePath = @"game\coins.txt";

        private CoinManager() 
        {
            _font = "fonts\\Minecraft.ttf";
            _fontSize = 20;
            _coins = new List<Coin>();
            LoadCoin();
        }

        public int CoinAmount
        {
            get { return _coinAmount; }
        }

        public static CoinManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CoinManager();
                }
                return _instance;
            }
        }
        public void Update(Player player, double y)
        {
            foreach (var coin in _coins)
            {
                coin.Move(y);
            }

            CollideWith(player);

            _coins.RemoveAll(coin => coin.Y >= GameConstants.WindowHeight);

            if (_coins.Count == 0)
            {
                AddCoin();
            }
        }
        private void AddCoin()
        {
            int randomCheck = SplashKit.Rnd(0, 11);

            if (randomCheck % 3 == 0)
            {
                double x;
                double previousX = 0;

                do
                {
                    x = SplashKit.Rnd(130, 455);
                } while (x == previousX);

                previousX = x;
                double y = 15;

                Coin newCoin = new Coin(x, y);
                _coins.Add(newCoin);
            }
        }

        public void ClearCoin()
        {
            _coins.Clear();
        }

        private void CollideWith(Player player)
        {
            for (int i = _coins.Count - 1; i >= 0; i--)
            {
                Coin coin = _coins[i];

                if (coin.CoinBitmap.BitmapCollision(coin.X, coin.Y, player.PlayerBitmap, player.X, player.Y))
                {
                    _coins.RemoveAt(i);
                    if (_coinAmount < GameConstants.MaxCoin)
                    {
                        _coinAmount++;
                        UpdateCoinText();
                    }
                }
            }
        }

        private void LoadCoin()
        {
            if (File.Exists(_coinAmountFilePath))
            {
                string coinText = File.ReadAllText(_coinAmountFilePath);
                if (int.TryParse(coinText, out int loadedCoin))
                {
                    _coinAmount = Math.Min(Math.Max(loadedCoin, 0), GameConstants.MaxCoin);
                }
                else
                {
                    _coinAmount = 0;
                }
            }
            else
            {
                _coinAmount = 0;
            }
            UpdateCoinText();
        }

        private void UpdateCoinText()
        {
            string coinString = _coinAmount.ToString("D4");
            StringBuilder updatedCoinText = new StringBuilder("Coin: ");

            for (int i = 0; i < 4; i++)
            {
                updatedCoinText.Append(coinString[i]);
                if (i < 3)
                {
                    updatedCoinText.Append(" ");
                }
            }

            _coinTxt = updatedCoinText.ToString();

            File.WriteAllText(_coinAmountFilePath, _coinAmount.ToString());
        }
        public void DeductCoins(int cost)
        {
            _coinAmount -= cost;
            UpdateCoinText();
        }
        public void Draw()
        {
            foreach (var coin in _coins)
            {
                coin.Draw();
            }

            double coinTxtX = 120;
            double coinTxtY = 10;

            SplashKit.DrawText(_coinTxt, Color.White, _font, _fontSize, coinTxtX, coinTxtY);
        }
    }
}

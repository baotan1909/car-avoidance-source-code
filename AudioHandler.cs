using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CarAvoidance
{
    public class AudioHandler
    {
        private double _musicVolume, _volumePercentage;
        private string _volumeFilePath = @"audio\music_volume.txt";
        private Music _gameMusic;

        private static AudioHandler _instance;

        private AudioHandler()
        {
            _gameMusic = SplashKit.MusicNamed("Nutcracker");

            if (File.Exists(_volumeFilePath))
            {
                string musicVolumeText = File.ReadAllText(_volumeFilePath);
                _musicVolume = double.Parse(musicVolumeText);
            }
            else
            {
                _musicVolume = 100;
            }
        }

        public static AudioHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AudioHandler();
                }
                return _instance;
            }
        }

        public double volumePercentage
        {
            get { return _volumePercentage; }
        }
        public double MusicVolume
        {
            get { return _musicVolume; }
            set { _musicVolume = value; }
        }

        private void SetMusicVolume(double new_volume)
        {
            _musicVolume = Math.Max(new_volume, 0);
            _musicVolume = Math.Min(100, _musicVolume);
            File.WriteAllText(_volumeFilePath, _musicVolume.ToString());
        }

        public double UpdateVolume()
        {
            return _volumePercentage = _musicVolume / 100;
        }

        public void AdjustMusicVolume(double delta)
        {
            double _newVolume = _musicVolume + delta;
            SetMusicVolume(_newVolume);
            SplashKit.SetMusicVolume((float)UpdateVolume());
        }

        public void Musicloop()
        {
            if (!SplashKit.MusicPlaying())
            {
                SplashKit.PlayMusic("Nutcracker");
                LoadMusicVolume();
            }
        }

        private void LoadMusicVolume()
        {
            SplashKit.SetMusicVolume((float)UpdateVolume());
        }
    }
}

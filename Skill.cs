using CarAvoidance.skill;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class Skill : ISkill
    {
        private int _id, _amount, _duration, _cooldown, _fontSize;
        private string _name, _font;
        private double _textX, _textY;
        private bool _isActive;
        private Color _color;
        private SplashKitSDK.Timer _durationTimer, _coolDownTimer;

        public Skill(int id, string name, int duration, int cooldown, SplashKitSDK.Timer durationTimer, SplashKitSDK.Timer coolDownTimer, double textY)
        {
            _id = id;
            _font = "fonts\\Minecraft.ttf";
            _fontSize = 20;
            _name = name;
            _amount = 0;
            _isActive = false;
            _duration = duration;
            _cooldown = cooldown;
            _durationTimer = durationTimer;
            _coolDownTimer = coolDownTimer;
            _color = Color.White;
            _textX = 5;
            _textY = textY;
        }

        public string Name
        {
            get { return _name; }
        }
        public double TextY
        {
            get { return _textY; }
            set { _textY = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (value <= 0)
                {
                    _amount = 0;
                }
                else if (value >= 99)
                {
                    _amount = 99;
                }
                else
                {
                    _amount = value;
                }
            }
        }
        public bool IsActive
        {
            get { return _isActive; }
        }

        public void Activate()
        {
            if (_amount <= 0)
            {
                return;
            }

            if (_isActive)
            {
                return;
            }

            if (_coolDownTimer.Ticks > 0)
            {
                return;
            }

            _amount--;
            _isActive = true;
            _color = Color.Yellow;
            StartDuration();
        }

        private void StartDuration()
        {
            SplashKit.StartTimer(_durationTimer);
        }

        private bool IsDurationOver()
        {
            return _durationTimer.Ticks / 1000 == _duration;
        }

        public void StopDuration()
        {
            SplashKit.StopTimer(_durationTimer);
            _isActive = false;
            this.ClearEffect();
        }

        public void StartCoolDown()
        {
            if (IsDurationOver())
            {
                StopDuration();

                if (!IsCoolDownComplete())
                {
                    _color = Color.Red;
                    SplashKit.StartTimer(_coolDownTimer);;
                }
            }
        }

        public bool IsCoolDownComplete()
        {
            return _coolDownTimer.Ticks / 1000 >= _cooldown;
        }


        public void StopCoolDown()
        {
            SplashKit.StopTimer(_coolDownTimer);
            _color = Color.White;
        }

        public virtual void SpecialEffect(SkillArgument skillArgument) { }

        public virtual void ClearEffect()
        {}

        public void Draw()
        {
            double Y = 50;

            SplashKit.DrawText($"{_id}. {_name}", _color, _font, _fontSize, _textX, _textY);
            SplashKit.DrawText($"Amount: {_amount}", _color, _font, _fontSize, _textX, _textY + 25);
            SplashKit.DrawText($"Duration: {_duration - _durationTimer.Ticks / 1000}", _color, _font, _fontSize, _textX, _textY + 50);
            SplashKit.DrawText($"CD: {_cooldown - _coolDownTimer.Ticks / 1000}", _color, _font, _fontSize, _textX, _textY + 75);
        }
    }
}



using CarAvoidance.skill;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class S_Invincible : Skill
    {
        public S_Invincible(string name, int duration, int cooldown)
            : base(3, name, duration, cooldown, SplashKit.CreateTimer($"{name}Duration"), SplashKit.CreateTimer($"{name}Cooldown"), 300)
        { }

        private SkillArgument? _skillArgument;

        public override void SpecialEffect(SkillArgument skillArgument)
        {
            if (this._skillArgument == null)
            {
                skillArgument.Configuration.IsObstacleCollisionChecked = false;
                this._skillArgument = skillArgument;
            }
        }

        public override void ClearEffect()
        {
            if (this._skillArgument != null)
            {
                this._skillArgument.Configuration.IsObstacleCollisionChecked = true;
                this._skillArgument = null;
            }
        }
    }
}


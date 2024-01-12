using CarAvoidance.skill;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class S_Timestop : Skill
    {
        public S_Timestop(string name, int duration, int cooldown)
            : base(2, name, duration, cooldown, SplashKit.CreateTimer($"{name}Duration"), SplashKit.CreateTimer($"{name}Cooldown"), 175)
        { }

        public override void SpecialEffect(SkillArgument skillArgument)
        {
            skillArgument.Configuration.Speed = 0;
        }
    }
}
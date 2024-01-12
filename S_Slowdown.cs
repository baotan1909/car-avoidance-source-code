using CarAvoidance.skill;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class S_Slowdown : Skill
    {
        public S_Slowdown(string name, int duration, int cooldown)
            : base(1,name, duration, cooldown, SplashKit.CreateTimer($"{name}Duration"), SplashKit.CreateTimer($"{name}Cooldown"), 50)
        { }

        public override void SpecialEffect(SkillArgument skillArgument)
        {
            skillArgument.Configuration.Speed = 1;
        }
    }
}
using CarAvoidance.skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public interface ISkill
    {
        string Name { get; }
        int Amount { get; set; }
        double TextY { get; set; }
        void Activate();
        bool IsActive { get; }
        void StopDuration();
        void StartCoolDown();
        bool IsCoolDownComplete();
        void StopCoolDown();
        void SpecialEffect(SkillArgument skillArgument);
        void ClearEffect();
        void Draw();
    }
}

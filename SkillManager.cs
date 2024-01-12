using CarAvoidance.skill;
using CarAvoidance.configuration;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance
{
    public class SkillManager
    {
        private static SkillManager _instance;

        private Dictionary<int, ISkill> _skills;
        private ISkill? _activeSkill;

        private SkillManager()
        {
            _skills = new Dictionary<int, ISkill>
            {
                { 1, new S_Slowdown("SlowDown", 5, 30) },
                { 2, new S_Timestop("TimeStop", 3, 60) },
                { 3, new S_Invincible("Invincible", 7, 90) }
            };

            LoadSkillAmounts();
        }
        public Dictionary<int, ISkill> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }
        public static SkillManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SkillManager();
                }
                return _instance;
            }
        }
        public void Update(GameConfiguration configuration)
        {
            for (int i = 1; i <= _skills.Count; i++)
            {
                if (SplashKit.KeyTyped((KeyCode)Enum.Parse(typeof(KeyCode), $"Num{i}Key")))
                {
                    if (_activeSkill == null || !_activeSkill.IsActive)
                    {
                        _activeSkill = _skills[i];
                        _activeSkill.Activate();
                    }
                }
            }

            if (_activeSkill != null && _activeSkill.IsActive)
            {
                _activeSkill.SpecialEffect(new SkillArgument(configuration));
                _activeSkill.StartCoolDown();
            }

            if (_activeSkill != null && _activeSkill.IsCoolDownComplete())
            {
                _activeSkill.StopCoolDown();
                _activeSkill = null;
            }
        }

        public void StopSkillEffects()
        {
            foreach (var skillEntry in _skills.Values)
            {
                ISkill skill = skillEntry;

                if (skill.IsActive)
                {
                    skill.StopDuration();
                }

                if (!skill.IsCoolDownComplete())
                {
                    skill.StopCoolDown();
                }
            }
        }
        public void SaveSkillAmounts()
        {
            using (StreamWriter writer = new StreamWriter(@"game\skill_amounts.txt"))
            {
                foreach (var skillEntry in _skills)
                {
                    ISkill skill = skillEntry.Value;
                    writer.WriteLine($"{skill.Name} {skill.Amount}");
                }
            }
        }

        private void LoadSkillAmounts()
        {
            if (File.Exists(@"game\skill_amounts.txt"))
            {
                string[] lines = File.ReadAllLines(@"game\skill_amounts.txt");

                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        string skillName = parts[0];
                        int amount = int.Parse(parts[1]);

                        var skillEntry = _skills.FirstOrDefault(entry => entry.Value.Name == skillName);

                        if (skillEntry.Value != null)
                        {
                            skillEntry.Value.Amount = amount;
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (ISkill skill in _skills.Values)
            {
                skill.Draw();
            }
        }
    }
}

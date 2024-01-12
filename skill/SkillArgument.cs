using CarAvoidance.configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAvoidance.skill
{
    public class SkillArgument
    {
        private readonly GameConfiguration _configuration;

        public SkillArgument(GameConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GameConfiguration Configuration => _configuration;
    }
}

using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class SkillDetail
    {
        public int Id { get; set; }
        public int? Skill { get; set; }
        public int? Weakfoot { get; set; }
        public int? Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Workrates { get; set; }
        public string Footed { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

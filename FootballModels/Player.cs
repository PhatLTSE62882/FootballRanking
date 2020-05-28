using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class Player
    {
        public Player()
        {
            DefDetail = new HashSet<DefDetail>();
            DriDetail = new HashSet<DriDetail>();
            PacDetail = new HashSet<PacDetail>();
            PasDetail = new HashSet<PasDetail>();
            Position = new HashSet<Position>();
            ShoDetail = new HashSet<ShoDetail>();
            SkillDetail = new HashSet<SkillDetail>();
            TradingMarket = new HashSet<TradingMarket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Ovr { get; set; }
        public string Pos { get; set; }
        public int? Pac { get; set; }
        public int? Sho { get; set; }
        public int? Dri { get; set; }
        public int? Def { get; set; }
        public int? Phy { get; set; }
        public int? Sm { get; set; }
        public int? Wf { get; set; }
        public string Wrs { get; set; }
        public string Foot { get; set; }
        public int? Stats { get; set; }
        public string Season { get; set; }
        public int? Team { get; set; }
        public int? Pas { get; set; }
        public string Face { get; set; }
        public string Flag { get; set; }
        public string Nation { get; set; }

        public virtual Team TeamNavigation { get; set; }
        public virtual ICollection<DefDetail> DefDetail { get; set; }
        public virtual ICollection<DriDetail> DriDetail { get; set; }
        public virtual ICollection<PacDetail> PacDetail { get; set; }
        public virtual ICollection<PasDetail> PasDetail { get; set; }
        public virtual ICollection<Position> Position { get; set; }
        public virtual ICollection<ShoDetail> ShoDetail { get; set; }
        public virtual ICollection<SkillDetail> SkillDetail { get; set; }
        public virtual ICollection<TradingMarket> TradingMarket { get; set; }
    }
}

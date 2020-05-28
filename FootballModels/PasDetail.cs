using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class PasDetail
    {
        public int Id { get; set; }
        public int? Pas { get; set; }
        public int? Vision { get; set; }
        public int? Crossing { get; set; }
        public int? Fk { get; set; }
        public int? Sp { get; set; }
        public int? Lp { get; set; }
        public int? Curve { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

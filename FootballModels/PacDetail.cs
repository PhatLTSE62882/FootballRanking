using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class PacDetail
    {
        public int Id { get; set; }
        public int Pac { get; set; }
        public int? Acc { get; set; }
        public int? Speed { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class DefDetail
    {
        public int Id { get; set; }
        public int? Interception { get; set; }
        public int? Ha { get; set; }
        public int? Da { get; set; }
        public int? Stand { get; set; }
        public int? Slide { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

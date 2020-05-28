using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class PhyDetail
    {
        public int Id { get; set; }
        public int? Jump { get; set; }
        public int? Stamina { get; set; }
        public int? Strength { get; set; }
        public int? Aggression { get; set; }
        public int Player { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class Position
    {
        public int Id { get; set; }
        public int? Cb { get; set; }
        public int? Cdm { get; set; }
        public int? Cm { get; set; }
        public int? Cam { get; set; }
        public int? Cf { get; set; }
        public int? St { get; set; }
        public int? Lw { get; set; }
        public int? Lf { get; set; }
        public int? Rf { get; set; }
        public int? Rw { get; set; }
        public int? Lm { get; set; }
        public int? Rm { get; set; }
        public int? Rb { get; set; }
        public int? Rwb { get; set; }
        public int? Lb { get; set; }
        public int? Lwb { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

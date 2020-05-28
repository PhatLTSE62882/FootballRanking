using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class ShoDetail
    {
        public int Id { get; set; }
        public int? Pos { get; set; }
        public int? Fin { get; set; }
        public int? Sp { get; set; }
        public int? Ls { get; set; }
        public int? Volleys { get; set; }
        public int? Pen { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

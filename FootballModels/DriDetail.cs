using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class DriDetail
    {
        public int Id { get; set; }
        public int? Agility { get; set; }
        public int? Balance { get; set; }
        public int? React { get; set; }
        public int? Control { get; set; }
        public int? Drib { get; set; }
        public int? Composure { get; set; }
        public int Player { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

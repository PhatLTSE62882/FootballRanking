using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class Team
    {
        public Team()
        {
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string League { get; set; }

        public virtual ICollection<Player> Player { get; set; }
    }
}

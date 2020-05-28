using System;
using System.Collections.Generic;

namespace FootballModels
{
    public partial class TradingMarket
    {
        public int Id { get; set; }
        public double? Xprice { get; set; }
        public double? Ps4price { get; set; }
        public int Player { get; set; }
        public string Xtime { get; set; }
        public string Ps4time { get; set; }

        public virtual Player PlayerNavigation { get; set; }
    }
}

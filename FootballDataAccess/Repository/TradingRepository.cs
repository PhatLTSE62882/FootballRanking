using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class TradingRepository : Repository<TradingMarket>,ITradingRepository
    {
        private readonly FootballContext _db;
        public TradingRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

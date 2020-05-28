using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class PositionRepository :Repository<Position>,IPositionRepository
    {
        private readonly FootballContext _db;
        public PositionRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

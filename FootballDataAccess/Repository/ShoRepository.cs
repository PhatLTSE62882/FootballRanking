using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class ShoRepository :Repository<ShoDetail>,IShoRepository
    {
        private readonly FootballContext _db;
        public ShoRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

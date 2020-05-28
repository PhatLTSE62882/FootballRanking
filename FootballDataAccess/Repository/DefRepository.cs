using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class DefRepository :Repository<DefDetail>,IDefRepository
    {
        private readonly FootballContext _db;
        public DefRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

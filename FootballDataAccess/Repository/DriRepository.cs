using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class DriRepository :Repository<DriDetail>,IDriRepository
    {
        private readonly FootballContext _db;
        public DriRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class PasRepository : Repository<PasDetail>,IPasRepository
    {
        private readonly FootballContext _db;
        public PasRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
   public class PacRepository : Repository<PacDetail>,IPacRepository
    {
        private readonly FootballContext _db;
        public PacRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

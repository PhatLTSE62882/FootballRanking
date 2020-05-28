using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
  public class PhyRepository :Repository<PhyDetail>,IPhyRepository
    {
        private readonly FootballContext _db;
        public PhyRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

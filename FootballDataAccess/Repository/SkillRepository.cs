using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
    public class SkillRepository : Repository<SkillDetail>,ISkillRepository
    {
        private readonly FootballContext _db;
        public SkillRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
    }
}

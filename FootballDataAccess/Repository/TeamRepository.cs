using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballDataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly FootballContext _db;
        public TeamRepository(FootballContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<Team> GetAll()
        {
            return _db.Team.ToList();
        }

        public void Update(Team team)
        {
            var getTeam = _db.Team.FirstOrDefault(x => x.Id == team.Id);
            _db.SaveChanges();
        }
    }
}

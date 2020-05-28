using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository.IRepository
{
    public interface ITeamRepository:IRepository<Team>
    {
        IEnumerable<Team> GetAll();
        void Update(Team team);
    }
}

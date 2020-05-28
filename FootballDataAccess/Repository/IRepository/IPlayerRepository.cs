using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository.IRepository
{
   public interface IPlayerRepository : IRepository<Player>
    {
        IEnumerable<Player> GetAll();
        void Update(Player player);
    }
}

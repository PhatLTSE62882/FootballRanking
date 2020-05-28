using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballDataAccess.Repository
{
    public class PlayerRepositorry : Repository<Player>, IPlayerRepository
    {
        private readonly FootballContext _db;
        public PlayerRepositorry(FootballContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<Player> GetAll()
        {
            return _db.Player.ToList();
        }

        public void Update(Player player)
        {
            var getPlayer = _db.Player.FirstOrDefault(x => x.Id == player.Id);
            _db.SaveChanges();
        }
    }
}

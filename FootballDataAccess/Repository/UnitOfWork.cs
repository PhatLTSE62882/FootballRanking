using FootballDataAccess.Repository.IRepository;
using FootballModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FootballContext _db;
        public UnitOfWork(FootballContext db)
        {
            _db = db;
            Player = new  PlayerRepositorry(_db);
            Team = new TeamRepository(_db);
            SkillDetail = new SkillRepository(_db);
            DefDetail = new DefRepository(_db);
            DriDetail = new DriRepository(_db);
            ShoDetail = new ShoRepository(_db);
            TradingMarket = new TradingRepository(_db);
            PacDetail = new PacRepository(_db);
            PasDetail = new PasRepository(_db);
            PhyDetail = new PhyRepository(_db);
            Position = new PositionRepository(_db);
        }

        public IPlayerRepository Player { get; }

        public ITeamRepository Team { get; }

        public ISkillRepository SkillDetail { get; }

        public IPacRepository PacDetail { get; }

        public IPasRepository PasDetail { get; }

        public IShoRepository ShoDetail { get; }

        public ITradingRepository TradingMarket { get; }

        public IPhyRepository PhyDetail { get; }

        public IDriRepository DriDetail { get; }

        public IDefRepository DefDetail { get; }

        public IPositionRepository Position { get; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

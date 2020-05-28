using System;
using System.Collections.Generic;
using System.Text;

namespace FootballDataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPlayerRepository Player { get; }
        ITeamRepository Team { get; }
        ISkillRepository SkillDetail { get; }
        IPacRepository PacDetail { get; }
        IPasRepository PasDetail { get; }
        IShoRepository ShoDetail { get; }
        ITradingRepository TradingMarket { get; }
        IPhyRepository PhyDetail { get; }
        IDriRepository DriDetail { get; }
        IDefRepository DefDetail { get; }
        IPositionRepository Position { get; }

        public void Save();
    }
}

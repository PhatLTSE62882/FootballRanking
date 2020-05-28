using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballRanking.Controllers.Players
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public PlayerController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: api/<controller>
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.Player.GetAll(includeProperties: "TeamNavigation").Take(25));
        }
        [HttpGet("page")]
        public IActionResult GetByPageNumber([FromQuery] String page)
        {
            var convertPage = int.Parse(page);
            if (convertPage >= 2)
            {
                convertPage = convertPage - 1;
                return Ok(unitOfWork.Player.GetAll(includeProperties: "TeamNavigation").Skip(convertPage * 25).Take(25));
            }
            else
            {
                return Ok(unitOfWork.Player.GetAll(includeProperties: "TeamNavigation").Take(25));

            }

        }
        [HttpGet("detail")]
        public IActionResult GetDetail([FromQuery] int id)
        {
            return Ok(unitOfWork.Player.GetFirstOrDefault(x => x.Id == id,includeProperties: "TeamNavigation,DefDetail,DriDetail,PacDetail,PasDetail,Position,ShoDetail,SkillDetail,TradingMarket"));
        }
    }
}

using API.Models.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Repository.IRepository;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IStockRepository _stock;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> usermanag, IStockRepository stock, IPortfolioRepository portrepo)
        {
            _usermanager = usermanag;
            _stock = stock;
            _portfolioRepo = portrepo;
        }

        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GetUserPortfolio()
        {
            //user inherited from controllerbase
            var username = User.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not authenticated");
            }
            var appuser = await _usermanager.FindByNameAsync(username);
            if (appuser == null)
            {
                return NotFound("User not found");
            }
            var userPortfolio = await _portfolioRepo.GetUserPortfolios(appuser);

            if (userPortfolio == null || !userPortfolio.Any())
            {
                return NotFound("User has not associated portfolios");
            }

            return Ok(userPortfolio);

        }
    }
}

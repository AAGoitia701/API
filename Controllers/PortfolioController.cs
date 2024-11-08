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
        [Authorize]

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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string Symbol)
        {
            var username = User.GetUsername();
            var appuser = await _usermanager.FindByNameAsync(username);
            var stock = await _stock.GetBySymbolAsync(Symbol);

            if (stock == null) return BadRequest("Stock not found");
            var userPortfolio = await _portfolioRepo.GetUserPortfolios(appuser);

            if (userPortfolio.Any(r => r.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            //now, we'll create the object
            var portfoliomodel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appuser.Id
            };

            await _portfolioRepo.CreatePortfolioAsync(portfoliomodel);
            if (portfoliomodel == null)
            {     
                return StatusCode(500, "Could not create portfolio");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize] 
        public async Task<IActionResult> DeleteStockFromPortfolio(string Symbol)
        {
            var username = User.GetUsername();
            var appuser = await _usermanager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepo.GetUserPortfolios(appuser);

            var filteredStock = userPortfolio.Where(r => r.Symbol.ToLower() == Symbol.ToLower()).ToList();

            if(filteredStock.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appuser, Symbol);
            }
            else
            {
                return BadRequest("Stock not in portfolio");
            }

            return Ok();

            
        }
    }
}

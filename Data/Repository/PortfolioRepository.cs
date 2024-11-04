using API.DataAccess;
using API.Models;
using API.Models.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repository.IRepository;

namespace WebAPI.Data.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext cont)
        {
            _context = cont;
        }
        public async Task<List<Stock>> GetUserPortfolios(AppUser user)
        {
            return await _context.Portfolios.Where(r => r.AppUserId.Equals(user.Id))
                .Select(portfolio => new Stock
                {
                    Id = portfolio.StockId,
                    CompanyName = portfolio.Stock.CompanyName,
                    Symbol = portfolio.Stock.Symbol,
                    LastDiv = portfolio.Stock.LastDiv,
                    Purchase = portfolio.Stock.Purchase,
                    Industry = portfolio.Stock.Industry,
                    MarketCap = portfolio.Stock.MarketCap
                }).ToListAsync();
        }



         
}
}

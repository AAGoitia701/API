using API.Models;
using API.Models.Models;

namespace WebAPI.Data.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolios(AppUser user);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);

        Task<Portfolio> DeletePortfolio(AppUser appuser, string symbol);
    }
}

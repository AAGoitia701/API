using API.Models;
using API.Models.Models;

namespace WebAPI.Data.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolios(AppUser user);
    }
}

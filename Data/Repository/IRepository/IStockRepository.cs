using API.Dtos.Stock;
using API.Models;
using WebAPI.Helpers;

namespace API.Repository.IRepository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetOneByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stock);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExist(int id);
    }
}

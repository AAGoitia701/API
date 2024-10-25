using API.DataAccess;
using API.Dtos.Stock;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(re => re.Id == id);

            if (stockModel == null) 
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync(true);

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetOneByIdAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(r => r.Id == id);
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(r => r.Id == id);

            if (stockModel == null) {
                return null;
            }

            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.MarketCap = updateStockDto.MarketCap;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.LastDiv = updateStockDto.LastDiv;

            _context.SaveChangesAsync();
            return stockModel;
        }
    }
}

using API.Dtos.Stock;
using API.Models;

namespace API.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockmodel) //use this to conect the properties from Stock to StockDto
        {
            return new StockDto
            {
                Id = stockmodel.Id,
                CompanyName = stockmodel.CompanyName,
                Purchase = stockmodel.Purchase,
                LastDiv = stockmodel.LastDiv,
                Industry = stockmodel.Industry
            };
        }

        public static Stock FromStockToCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                Symbol = stockDto.Symbol,
            };
        }

    }
}

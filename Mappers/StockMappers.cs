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
    }
}

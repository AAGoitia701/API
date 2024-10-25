using API.Data;
using API.Dtos.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();

            var stocks2 = stocks
                .Select(r => r.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById([FromRoute]int id) //-> route from httpget, which is id 
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) //-> data will be sent in Json no url, so we need the fromBody to pass it through the body of the http 
        {
            var stockModel = stockDto.FromStockToCreateDto();
            await _context.Stocks.AddAsync(stockModel);    
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOneById), new {Id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(r => r.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            }
            
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.MarketCap = updateStockDto.MarketCap;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.LastDiv = updateStockDto.LastDiv;

            await _context.SaveChangesAsync(true);

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(re => re.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            }


            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync(true);

            return NoContent();
        }
    }
}

using API.Data;
using API.Dtos.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(r => r.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById([FromRoute]int id) //-> route from httpget, which is id 
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto) //-> data will be sent in Json no url, so we need the fromBody to pass it through the body of the http 
        {
            var stockModel = stockDto.FromStockToCreateDto();
            _context.Stocks.Add(stockModel);    
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOneById), new {Id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody]UpdateStockRequestDto updateStockDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(r => r.Id == id);
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
            
            _context.Stocks.Update(stockModel);
            _context.SaveChanges(true);

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id) 
        {
            var stockModel = _context.Stocks.FirstOrDefault(re => re.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            }


            _context.Stocks.Remove(stockModel);
            _context.SaveChanges(true);

            return NoContent();
        }
    }
}

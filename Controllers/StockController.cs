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

    }
}

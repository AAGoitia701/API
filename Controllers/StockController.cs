using API.Data;
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
        public ActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(r => r.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public ActionResult GetOneById([FromRoute]int id) //-> route from httpget, which is id 
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

    }
}

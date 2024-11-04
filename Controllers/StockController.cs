using API.DataAccess;
using API.Dtos.Stock;
using API.Mappers;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;

namespace API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);

            var stocks2 = stocks
                .Select(r => r.ToStockDto());

            return Ok(stocks2);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneById([FromRoute]int id) //-> route from httpget, which is id 
        {
            var stock = await _stockRepo.GetOneByIdAsync(id); 
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) //-> data will be sent in Json no url, so we need the fromBody to pass it through the body of the http 
        {
            var stockModel = stockDto.FromStockToCreateDto();

            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetOneById), new {Id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateStockRequestDto updateStockDto)
        {
            
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

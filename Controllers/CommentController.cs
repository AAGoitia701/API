using API.Mappers;
using API.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Comment;

namespace API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository comRepo, IStockRepository IStockRepo)
        {
            _commentRepo = comRepo;
            _stockRepo = IStockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _commentRepo.GetAllAsync();
            var all2 = all.Select(r => r.ToCommentDto());

            return Ok(all2);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var oneComment = await _commentRepo.GetOneByIdAsync(id);
            if (oneComment == null)
            {
                return NotFound();
            }

            return Ok(oneComment);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentRequestDto commentDto)
        {
            if(! await _stockRepo.StockExist(stockId))
            {
                return BadRequest("The stock doesn't exist");
            }

            var commentModel = commentDto.FromCreateToComment(stockId);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetOne), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return NoContent();

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateCommentRequestDto commentDto)
        {
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);
            if(commentModel == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.ToCommentDto());

        }
    }
}

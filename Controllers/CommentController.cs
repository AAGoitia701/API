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

        public CommentController(ICommentRepository comRepo)
        {
            _commentRepo = comRepo;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            var commentModel = commentDto.FromCreateToComment();

            await _commentRepo.CreateAsync(commentModel);

            return Ok(commentModel);
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
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());

        }
    }
}

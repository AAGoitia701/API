﻿using API.Mappers;
using API.Models;
using API.Models.Models;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Comment;
using WebAPI.Extensions;

namespace API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _manager;

        public CommentController(ICommentRepository comRepo, IStockRepository IStockRepo, UserManager<AppUser> manager)
        {
            _commentRepo = comRepo;
            _stockRepo = IStockRepo;
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) {  return BadRequest(ModelState); }

            var all = await _commentRepo.GetAllAsync();
            var all2 = all.Select(r => r.ToCommentDto());

            return Ok(all2);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var oneComment = await _commentRepo.GetOneByIdAsync(id);
            if (oneComment == null)
            {
                return NotFound();
            }

            return Ok(oneComment);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (! await _stockRepo.StockExist(stockId))
            {
                return BadRequest("The stock doesn't exist");
            }

            var username = User.GetUsername();
            var appuser = await _manager.FindByNameAsync(username);


            var commentModel = commentDto.FromCreateToComment(stockId);

            commentModel.AppUserId = appuser.Id;

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetOne), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("Comment not found");
            }
            return NoContent();

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);
            if(commentModel == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.ToCommentDto());

        }
    }
}

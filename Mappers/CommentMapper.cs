using API.Dtos.Comment;
using API.Models;
using System.Runtime.CompilerServices;
using WebAPI.Dtos.Comment;

namespace API.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id= commentModel.Id,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                Title = commentModel.Title,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId
                
            };
        }

        public static Comment FromCreateToComment(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
                StockId = stockId
            };
        }
    }
}

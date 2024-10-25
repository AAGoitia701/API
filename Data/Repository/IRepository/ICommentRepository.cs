using API.Dtos.Stock;
using API.Models;
using WebAPI.Dtos.Comment;

namespace API.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetOneByIdAsync(int id);

        Task<Comment> CreateAsync(Comment comment);

        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateComDto);

        Task<Comment?> DeleteAsync(int id);
    }
}

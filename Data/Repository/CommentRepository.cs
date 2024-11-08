using API.DataAccess;
using API.Dtos.Stock;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos.Comment;

namespace API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
           await _context.Comments.AddAsync(comment);
           await _context.SaveChangesAsync();

           return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) 
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(x => x.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetOneByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(x => x.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if(comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateComDto)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(r => r.Id == id);
            if (commentModel == null) 
            {
                return null;
            }

            commentModel.Title = updateComDto.Title;
            commentModel.Content = updateComDto.Content;

            await _context.SaveChangesAsync();

            return commentModel;
        }
    }
}

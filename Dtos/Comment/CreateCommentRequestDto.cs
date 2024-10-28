using API.Models;

namespace WebAPI.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}

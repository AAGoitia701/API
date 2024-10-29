using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must have at least 3 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must have at least 5 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}

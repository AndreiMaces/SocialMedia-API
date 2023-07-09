using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs.Comment;

public class CreateCommentDTO
{
    [Required]
    public string Content { get; set; }
}
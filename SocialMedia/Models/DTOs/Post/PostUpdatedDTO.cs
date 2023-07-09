using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class PostUpdatedDTO
{
    [Required]
    public string Content { get; set; }
}
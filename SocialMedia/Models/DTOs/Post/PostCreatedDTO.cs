using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class PostCreatedDTO
{
    [Required]
    public string Content { get; set; }
}
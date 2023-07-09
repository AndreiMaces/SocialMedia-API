using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class UserUpdatedDTO
{
    public int Id { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(36)]
    public string Name { get; set; }
}
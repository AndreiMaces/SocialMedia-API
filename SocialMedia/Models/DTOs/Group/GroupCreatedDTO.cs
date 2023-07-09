using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class GroupCreatedDTO
{
    [Required]
    [MinLength(6)]
    [MaxLength(36)]
    public string Name { get; set; }
    public string About { get; set; }
    public string ImageURL { get; set; }
}
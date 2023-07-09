using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class RegisterRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$")]
    public string Password { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(36)]
    public string Name { get; set; }
}
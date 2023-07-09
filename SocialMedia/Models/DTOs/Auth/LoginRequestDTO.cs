using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class LoginRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
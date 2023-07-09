using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class AuthResponseDTO
{
    public UserDTO User { get; set; }
    [Required]
    public string Token { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models.DTOs;

public class GroupDTO
{
    public int Id { get; set; }
    [Required]
    public ICollection<UserDTO> Participants { get; set; }
    public ICollection<PostDTO> Posts { get; set; }
    [Required]
    public string Name { get; set; }
    public string About { get; set; }
    public string ImageURL { get; set; }
}
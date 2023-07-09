using System.ComponentModel.DataAnnotations;
using SocialMedia.Models.DTOs.Comment;

namespace SocialMedia.Models.DTOs;

public class PostDTO
{
    public int Id { get; set; }
    [Required]
    public UserDTO Author { get; set; }
    public GroupDTO Group { get; set; }

    public ICollection<GetCommentsDTO> Comments { get; set; }

    [Required]
    public string Content { get; set; }
}
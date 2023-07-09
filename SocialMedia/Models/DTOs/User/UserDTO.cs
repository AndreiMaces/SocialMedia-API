namespace SocialMedia.Models.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public List<UserDTO> Friends { get; set; }
    public List<PostDTO> Posts { get; set; }

    public string Name { get; set; }
}
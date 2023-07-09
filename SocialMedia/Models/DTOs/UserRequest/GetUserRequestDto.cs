namespace SocialMedia.Models.DTOs.UserRequest;

public class GetUserRequestDto
{
    public int Id { get; set; }
    public BasicUserDto RequestedUser { get; set; }
}
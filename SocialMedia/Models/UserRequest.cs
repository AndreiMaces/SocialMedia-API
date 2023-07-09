namespace SocialMedia.Models;

public class UserRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
        
    public int RequestedUserId { get; set; }
    public User RequestedUser { get; set; }
}
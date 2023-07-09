namespace SocialMedia.Models;

public class User
{
    public int Id { get; set; }
    public List<UserRequest> Requests { get; set; } = new();
    public List<User> Friends { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
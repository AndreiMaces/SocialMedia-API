namespace SocialMedia.Models;

public class Post
{
    public int Id { get; set; }
    public User Author { get; set; }
    public List<Comment> Comments { get; set; } = new();
    public List<Reaction> Reactions { get; set; } = new();
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
namespace SocialMedia.Models;

public class Comment
{
    public int Id { get; set; }
    
    public User Author { get; set; }
    public Post Post { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
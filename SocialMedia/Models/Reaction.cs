using SocialMedia.Models.Enums;

namespace SocialMedia.Models;

public class Reaction
{
    public int Id { get; set; }
    public User Author { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }

    public ReactionType Content { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
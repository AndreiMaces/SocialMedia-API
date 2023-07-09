using SocialMedia.Models.Enums;

namespace SocialMedia.Models.DTOs.Reaction;

public class GetReactionDTO
{
    public int Id { get; set; }
    public User Author { get; set; }
    public ReactionType Content { get; set; }
}
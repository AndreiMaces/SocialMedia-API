using SocialMedia.Models.Enums;

namespace SocialMedia.Models.DTOs.Reaction;

public class CreateReactionDTO
{
    public ReactionType Content { get; set; }
}
using SocialMedia.Models;

namespace SocialMedia.Services.IServices;

public interface IReactionService
{
    Task<APIResponse> DeleteReaction(int id);
    Task<APIResponse> GetReaction(int id);
    Task<APIResponse> GetReactions();
}
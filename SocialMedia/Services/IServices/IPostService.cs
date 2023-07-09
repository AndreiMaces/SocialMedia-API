using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Models.DTOs.Reaction;

namespace SocialMedia.Services.IServices;

public interface IPostService
{ 
    Task<APIResponse> GetPosts(int pageSize = 10, int pageNumber = 1);
    Task<APIResponse> GetPost(int id);
    Task<APIResponse> AddComment(int id, CreateCommentDTO comment);
    Task<APIResponse> GetComments(int id);
    Task<APIResponse> CreatePost(PostCreatedDTO createDTO);
    Task<APIResponse> DeletePost(int id);
    Task<APIResponse> UpdatePost(int id, PostUpdatedDTO updateDTO);
    Task<APIResponse> AddReaction(int id, CreateReactionDTO reaction);
    Task<APIResponse> GetReactions(int id);

}
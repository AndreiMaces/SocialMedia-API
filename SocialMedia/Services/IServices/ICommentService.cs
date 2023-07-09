using SocialMedia.Models;
using SocialMedia.Models.DTOs.Comment;

namespace SocialMedia.Services.IServices;

public interface ICommentService
{ 
    Task<APIResponse> UpdateComment(int id, UpdateCommentDTO model);
    Task<APIResponse> DeleteComment(int id);

}
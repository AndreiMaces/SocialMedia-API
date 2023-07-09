using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Repository.IRepository;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;
    private readonly User _loggedInUser;
    
    public CommentService(IUnitOfWork db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _loggedInUser = httpContextAccessor.HttpContext?.Items["LoggedInUser"] as User;
    }
    
    public async Task<APIResponse> UpdateComment(int id, UpdateCommentDTO model)
    {
        var response = new APIResponse();
        var comment = await _db.Comments.GetAsync(x => x.Id == id, includeProprieties: "Author");
        if (comment == null)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Comment not found!");
            return response;
        }

        if (comment.Author.Id != _loggedInUser.Id)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.IsSuccess = false;
            response.ErrorMessages.Add("You are not the owner of this comment!");
            return response;
        }

        comment.Content = model.Content;
        await _db.Comments.SaveAsync();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<UpdateCommentDTO>(comment);
        return response;
    }

    public async Task<APIResponse> DeleteComment(int id)
    {
        var response = new APIResponse();
        var comment = await _db.Comments.GetAsync(x => x.Id == id, includeProprieties: "Author");
        if (comment == null)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Comment not found!");
            return response;
        }

        if (comment.Author.Id != _loggedInUser.Id)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.IsSuccess = false;
            response.ErrorMessages.Add("You are not the owner of this comment!");
            return response;
        }

        await _db.Comments.RemoveAsync(comment);
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<UpdateCommentDTO>(comment);
        return response;
    }

}
using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Models.DTOs.Reaction;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _db;
    private readonly User _loggedInUser;
    
    public PostService(IUnitOfWork db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _db = db;
        _loggedInUser = httpContextAccessor.HttpContext?.Items["LoggedInUser"] as User;
    }
    
    public async Task<APIResponse> GetPosts(int pageSize = 10, int pageNumber = 1)
    {
        var response = new APIResponse();
        var posts = await _db.Posts.GetAllAsync(includeProprieties: "Comments", pageNumber: pageNumber,
            pageSize: pageSize);
        response.Result = _mapper.Map<List<PostDTO>>(posts);
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetPost(int id)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(p => p.Id == id, includeProprieties: "Comments");
        if (post == null)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Result = _mapper.Map<PostDTO>(post);
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> AddComment(int id, CreateCommentDTO comment)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(u => u.Id == id, includeProprieties: "Comments");
        if (post == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Post not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        var model = _mapper.Map<Comment>(comment);
        model.Author = _loggedInUser;
        model.Post = post;
        await _db.Comments.CreateAsync(model);
        post.Comments.Add(model);
        await _db.Posts.SaveAsync();
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetComments(int id)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(u => u.Id == id, includeProprieties: "Comments");
        if (post == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Post not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Result = _mapper.Map<List<GetCommentsDTO>>(post.Comments);
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> CreatePost(PostCreatedDTO createDTO)
    {
        var response = new APIResponse();
        if (createDTO == null)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.Add("Invalid payload!");
            return response;
        }
        var model = _mapper.Map<Post>(createDTO);
        model.Author = _loggedInUser;
        await _db.Posts.CreateAsync(model);
        _loggedInUser.Posts.Add(model);
        await _db.Users.SaveAsync();
        response.Result = _mapper.Map<PostDTO>(model);
        response.StatusCode = HttpStatusCode.Created;
        return response;
    }

    public async Task<APIResponse> DeletePost(int id)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(p => p.Id == id, includeProprieties: "Author");
        if (post == null)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        if (post.Author.Id != _loggedInUser.Id)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.Unauthorized;
            return response;
        }

        _loggedInUser.Posts.Remove(post);
        await _db.Users.SaveAsync();
        await _db.Posts.RemoveAsync(post);
        response.Result = _mapper.Map<PostDTO>(post);
        response.StatusCode = HttpStatusCode.NoContent;
        return response;
    }

    public async Task<APIResponse> UpdatePost(int id, PostUpdatedDTO updateDTO)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(p => p.Id == id, includeProprieties: "Author");
        if (post == null)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        if (post.Author.Id != _loggedInUser.Id)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.Unauthorized;
            return response;
        }

        post.Content = updateDTO.Content;
        await _db.Posts.UpdateAsync(post);
        response.Result = _mapper.Map<PostDTO>(post);
        response.StatusCode = HttpStatusCode.NoContent;
        return response;
    }

    public async Task<APIResponse> AddReaction(int id, CreateReactionDTO reaction)
    {
        var response = new APIResponse();
        var post = await _db.Posts.GetAsync(u => u.Id == id, includeProprieties: "Reactions");
        if (post == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Post not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        var model = _mapper.Map<Reaction>(reaction);
        model.Author = _loggedInUser;
        model.Post = post;
        await _db.Reactions.CreateAsync(model);
        post.Reactions.Add(model);
        await _db.Posts.SaveAsync();
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetReactions(int id)
    {
        var response = new APIResponse();
        List<Reaction> reactions =
            await _db.Posts.GetReactionsAsync(id);
        response.Result = _mapper.Map<List<GetReactionDTO>>(reactions);
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }
}
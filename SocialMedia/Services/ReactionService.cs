using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs.Reaction;
using SocialMedia.Repository.IRepository;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class ReactionService : IReactionService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;
    private readonly User _loggedInUser;

    public ReactionService(IHttpContextAccessor httpContextAccessor, IMapper mapper,
        IUnitOfWork db)
    {
        _db = db;
        _mapper = mapper;
        _loggedInUser = httpContextAccessor.HttpContext?.Items["LoggedInUser"] as User;
    }
    
    public async Task<APIResponse> DeleteReaction(int id)
    {
        var response = new APIResponse();
        var reaction = await _db.Reactions.GetAsync(x => x.Id == id, includeProprieties: "Author");
        if (reaction == null)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Reaction not found!");
            return response;
        }

        if (reaction.Author.Id != _loggedInUser.Id)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.IsSuccess = false;
            response.ErrorMessages.Add("You are not the owner of this reaction!");
            return response;
        }

        await _db.Reactions.RemoveAsync(reaction);
        await _db.Reactions.SaveAsync();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<GetReactionDTO>(reaction);
        return response;
    }

    public async Task<APIResponse> GetReaction(int id)
    {
        var response = new APIResponse();
        var reaction = await _db.Reactions.GetAsync(x => x.Id == id, includeProprieties: "Author");
        if (reaction == null)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Reaction not found!");
            return response;
        }

        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<GetReactionDTO>(reaction);
        return response;
    }

    public async Task<APIResponse> GetReactions()
    {
        var response = new APIResponse();
        var reactions = await _db.Reactions.GetAllAsync(includeProprieties: "Author");
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<IEnumerable<GetReactionDTO>>(reactions);
        return response;
    }
}
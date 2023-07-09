using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Models.DTOs.UserRequest;
using SocialMedia.Repository.IRepository;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _db;
    private readonly IMapper _mapper;
    private readonly User _loggedInUser;
    
    public ProfileService(IUnitOfWork db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _db = db;
        _loggedInUser = httpContextAccessor.HttpContext?.Items["LoggedInUser"] as User;
    }
    
    public async Task<APIResponse> GetFeed(int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var feed = await _db.Users.GetFeed(_loggedInUser.Id, pageSize: pageSize, pageNumber: pageNumber);
        var feedDTO = _mapper.Map<List<PostDTO>>(feed);
        response.Result = feedDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> AddFriend(int id)
    {
        var response = new APIResponse();
        if (_loggedInUser.Id == id)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }

        var friend = await _db.Users.GetAsync(u => u.Id == id, includeProprieties: "Requests");
        if (friend == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("User not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        if (friend.Requests.Any(r => r.RequestedUserId == _loggedInUser.Id))
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Friend request already sent!");
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }

        var request = new UserRequest
        {
            UserId = _loggedInUser.Id,
            RequestedUserId = friend.Id
        };

        await _db.UserRequests.CreateAsync(request);
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> AcceptRequest(int id)
    {
        var response = new APIResponse();
        var request = await _db.UserRequests.GetAsync(r => r.Id == id);
        if (request == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Request not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        if (request.RequestedUserId != _loggedInUser.Id)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("You are not the requested user!");
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }

        var friend = await _db.Users.GetAsync(u => u.Id == request.UserId, includeProprieties: "Requests,Friends");
        if (friend == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("User not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        var friendRequest = _db.UserRequests.GetAsync(r => r.Id == id);

        if (friendRequest == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Request not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        friend.Friends.Add(_loggedInUser);
        _loggedInUser.Friends.Add(friend);
        await _db.Users.SaveAsync();

        await _db.UserRequests.RemoveAsync(request);

        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetFriends(int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var friends = await _db.Users.GetFriends(_loggedInUser.Id, pageSize: pageSize, pageNumber: pageNumber);
        var friendsDTO = _mapper.Map<List<UserDTO>>(friends);
        response.Result = friendsDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetRequests(int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var requests = await _db.Users.GetFriendRequests(_loggedInUser.Id, pageSize: pageSize, pageNumber: pageNumber);
        var requestsDTO = _mapper.Map<List<GetUserRequestDto>>(requests);
        response.Result = requestsDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> DeclineRequest(int id)
    {
        var response = new APIResponse();
        var request = _loggedInUser.Requests.FirstOrDefault(r => r.Id == id);
        if (request == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Friend request not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        _loggedInUser.Requests.Remove(request);
        await _db.Users.SaveAsync();

        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> RemoveFriend(int id)
    {
        var response = new APIResponse();
        if (_loggedInUser.Id == id)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }

        var friend = await _db.Users.GetAsync(u => u.Id == id, includeProprieties: "Friends");

        if (friend == null)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add("Friend not found!");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        friend.Friends.Remove(_loggedInUser);
        _loggedInUser.Friends.Remove(friend);
        await _db.Users.SaveAsync();

        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }
}
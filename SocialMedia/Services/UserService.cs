using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class UserService : IUserService
{
    private IMapper _mapper;
    private IUnitOfWork _db;

    public UserService(IUnitOfWork db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }
    
    public async Task<APIResponse> GetAll(int pageSize = 24, int pageNumber = 1, string q = "")
    {
        var response = new APIResponse();
        var users = await _db.Users.GetAllAsync(u => u.Name.ToLower().Contains(q.ToLower()), pageSize: pageSize,
            pageNumber: pageNumber);
        var usersDTO = _mapper.Map<List<UserDTO>>(users);
        response.Result = usersDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetFeed(int id, int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var feed = await _db.Users.GetFeed(id, pageSize: pageSize, pageNumber: pageNumber);
        var feedDTO = _mapper.Map<List<PostDTO>>(feed);
        response.Result = feedDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse> GetFriends(int id, int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var friends = await _db.Users.GetFriends(id, pageSize: pageSize, pageNumber: pageNumber);
        var friendsDTO = _mapper.Map<List<UserDTO>>(friends);
        response.Result = friendsDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }
}
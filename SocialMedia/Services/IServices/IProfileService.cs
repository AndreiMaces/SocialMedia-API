using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Services.IServices;

public interface IProfileService
{
    Task<APIResponse> GetFeed(int pageSize = 24, int pageNumber = 1);
    Task<APIResponse> AddFriend(int id);
    Task<APIResponse> AcceptRequest(int id);
    Task<APIResponse> GetFriends(int pageSize = 24, int pageNumber = 1);
    Task<APIResponse> GetRequests(int pageSize = 24, int pageNumber = 1);
    Task<APIResponse> DeclineRequest(int id);
    Task<APIResponse> RemoveFriend(int id);
}
using SocialMedia.Models;

namespace SocialMedia.Services.IServices;

public interface IUserService
{
    Task<APIResponse> GetAll( int pageSize = 24, int pageNumber = 1, string q = "");

    Task<APIResponse> GetFeed(int id,  int pageSize = 24, int pageNumber = 1);

    Task<APIResponse> GetFriends(int id, int pageSize = 24, int pageNumber = 1);
}
using SocialMedia.Models;

namespace SocialMedia.Services.IServices;

public interface IRequestService
{
    Task<APIResponse> GetRequests(int pageSize = 24, int pageNumber = 1);
}
using SocialMedia.Models;
using SocialMedia.Models.DTOs;

namespace SocialMedia.Services.IServices;

public interface IAuthService
{
    Task<APIResponse> Login(LoginRequestDTO model);
    Task<APIResponse> Register(RegisterRequestDTO model);

}
using SocialMedia.Models;

namespace SocialMedia.Services.IServices;

public interface IJWTService
{
    public string GenerateJWT(User user);
}
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SocialMedia.Repository.IRepository;

namespace SocialMedia.Middleware;

public class OwnerAuthorize
{
    private readonly RequestDelegate _next;

    public OwnerAuthorize(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IUserRepository _dbUser)
    {
        try
        {
            var accessToken =
                await httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "MyUserId");
            if (userIdClaim != null)
            {
                var loggedInUserId = int.Parse(userIdClaim.Value);
                var loggedInUser = await _dbUser.GetAsync(u => u.Id == loggedInUserId,
                    includeProprieties: "Requests,Friends");
                httpContext.Items["LoggedInUser"] = loggedInUser;
            }

            await _next(httpContext);
        } 
        catch (Exception e)
        {
            await _next(httpContext);
        }
    }
}
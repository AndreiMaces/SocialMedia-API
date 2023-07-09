using System.Net;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _db;

    public AuthService(IUnitOfWork db)
    {
        _db = db;
    }

    public async Task<APIResponse> Login(LoginRequestDTO model)
    {
        var response = new APIResponse();
        var loginResponse = await _db.Users.Login(model);
        if (loginResponse.User is null || string.IsNullOrEmpty(loginResponse.Token))
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Email or password is incorrect!");
            return response;
        }

        response.StatusCode = HttpStatusCode.OK;
        response.Result = loginResponse;
        return response;
    }

    public async Task<APIResponse> Register(RegisterRequestDTO model)
    {
        var response = new APIResponse();
        var isEmailUnique = _db.Users.IsEmailUnique(model.Email);
        if (isEmailUnique)
        {
            var registerResponse = await _db.Users.Register(model);
            response.StatusCode = HttpStatusCode.OK;
            response.Result = registerResponse;
            return response;
        }

        response.StatusCode = HttpStatusCode.BadRequest;
        response.IsSuccess = false;
        response.ErrorMessages.Add("Email already used!");
        return response;
    }
}
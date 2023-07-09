using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private APIResponse _response;
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _response = new APIResponse();
        _authService = authService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO model)
    {
        _response = await _authService.Login(model);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterRequestDTO model)
    {
        _response = await _authService.Register(model);
        return StatusCode((int)_response.StatusCode, _response);
    }
}
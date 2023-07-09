using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private APIResponse _response;
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _response = new APIResponse();
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetAll([FromQuery] int pageSize = 24, int pageNumber = 1, string q = "")
    {
        _response = await _userService.GetAll(pageSize, pageNumber, q);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpGet("{id:int}/feed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetFeed(int id, [FromQuery] int pageSize = 24, int pageNumber = 1)
    {
        _response = await _userService.GetFeed(id, pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpGet("{id:int}/friends", Name = "GetFriends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetFriends(int id, [FromQuery] int pageSize = 24,
        int pageNumber = 1)
    {
        _response = await _userService.GetFriends(id, pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }
}
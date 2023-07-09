using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private APIResponse _response;
    private readonly IProfileService _profileService;
        
    public ProfileController(IProfileService profileService)
    {
        _response = new APIResponse();
        _profileService = profileService;
    }

    [Authorize]
    [HttpGet("feed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetFeed([FromQuery] int pageSize = 24, int pageNumber = 1)
    {
        _response = await _profileService.GetFeed(pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [Authorize]
    [HttpPost("friends/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> AddFriend(int id)
    {
        _response = await _profileService.AddFriend(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [Authorize]
    [HttpPost("requests/{id:int}/accept")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> AcceptRequest(int id)
    {
        _response = await _profileService.AcceptRequest(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [Authorize]
    [HttpGet("friends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetFriends([FromQuery] int pageSize = 24, int pageNumber = 1)
    {
        _response = await _profileService.GetFriends(pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [Authorize]
    [HttpGet("requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetRequests([FromQuery] int pageSize = 24, int pageNumber = 1)
    {
        _response = await _profileService.GetRequests(pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [Authorize]
    [HttpPost("requests/{id:int}/decline")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeclineRequest(int id)
    {
        _response = await _profileService.DeclineRequest(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [Authorize]
    [HttpDelete("friends/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> RemoveFriend(int id)
    {
        _response = await _profileService.RemoveFriend(id);
        return StatusCode((int)_response.StatusCode, _response);
    }
}
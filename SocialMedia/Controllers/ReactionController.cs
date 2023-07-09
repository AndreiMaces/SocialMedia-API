using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("reactions")]
[ApiController]
public class ReactionController : ControllerBase
{
    private readonly IReactionService _reactionService;
    private APIResponse _response;

    public ReactionController(IReactionService reactionService)
    {
        _reactionService = reactionService;
        _response = new APIResponse();
    }

    [HttpDelete("{id:int}", Name = "DeleteReaction")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> DeleteReaction(int id)
    {
        _response = await _reactionService.DeleteReaction(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpGet("{id:int}", Name = "GetReaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetReaction(int id)
    {
        _response = await _reactionService.GetReaction(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpGet(Name = "GetReactions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetReactions()
    {
        _response = await _reactionService.GetReactions();
        return StatusCode((int)_response.StatusCode, _response);
    }
}
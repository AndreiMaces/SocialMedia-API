using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("comments")]
[ApiController]
public class CommentController : ControllerBase
{
    protected APIResponse _response;
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _response = new APIResponse();
        _commentService = commentService;
    }

    [HttpPut("{id:int}", Name = "UpdateComment")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateComment(int id, [FromBody] UpdateCommentDTO model)
    {
        _response = await _commentService.UpdateComment(id, model);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpDelete("{id:int}", Name = "DeleteComment")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> DeleteComment(int id)
    {
        _response = await _commentService.DeleteComment(id);
        return StatusCode((int)_response.StatusCode, _response);
    }
}
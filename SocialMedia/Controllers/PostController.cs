using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Models.DTOs.Reaction;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[Route("posts")]
[ApiController]
public class PostController : ControllerBase
{
    protected APIResponse _response;
    private readonly IPostService _postService;
    
    public PostController(IPostService postService)
    {
        _response = new APIResponse();
        _postService = postService;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetPosts(int pageSize = 10, int pageNumber = 1)
    {
        _response = await _postService.GetPosts(pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpGet("{id}", Name = "GetPost")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetPost(int id)
    {
        _response = await _postService.GetPost(id);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpPost("{id}/comments")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> AddComment(int id, [FromBody] CreateCommentDTO comment)
    {
        _response = await _postService.AddComment(id, comment);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpGet("{id}/comments")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetComments(int id)
    {
        _response = await _postService.GetComments(id);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreatePost([FromBody] PostCreatedDTO createDTO)
    {
        _response = await _postService.CreatePost(createDTO);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeletePost(int id)
    {
        _response = await _postService.DeletePost(id);
        return StatusCode((int)_response.StatusCode, _response);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdatePost(int id, [FromBody] PostUpdatedDTO updateDTO)
    {
        _response = await _postService.UpdatePost(id, updateDTO);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpPost("{id}/reactions")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> AddReaction(int id, [FromBody] CreateReactionDTO reaction)
    {
        _response = await _postService.AddReaction(id, reaction);
        return StatusCode((int)_response.StatusCode, _response);
    }
        
    [HttpGet("{id}/reactions")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetReactions(int id)
    {
        _response = await _postService.GetReactions(id);
        return StatusCode((int)_response.StatusCode, _response);
    }
}
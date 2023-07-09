using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Services.IServices;

namespace SocialMedia.Controllers;

[ApiController]
[Authorize]
[Route("/requests")]
public class RequestController : ControllerBase
{
    private IRequestService _requestService;
    private APIResponse _response;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
        _response = new APIResponse();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetRequests(int pageSize = 24, int pageNumber = 1)
    {
        _response = await _requestService.GetRequests(pageSize, pageNumber);
        return StatusCode((int)_response.StatusCode, _response);
    }

}
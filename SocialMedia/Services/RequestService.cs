using System.Net;
using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Services.IServices;
using SocialMedia.Utilities.IUtilities;

namespace SocialMedia.Services;

public class RequestService : IRequestService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _db;

    public RequestService(IMapper mapper, IUnitOfWork db)
    {
        _mapper = mapper;
        _db = db;
    }
    
    public async Task<APIResponse> GetRequests(int pageSize = 24, int pageNumber = 1)
    {
        var response = new APIResponse();
        var requests = await _db.UserRequests.GetAllAsync(pageNumber: pageNumber, pageSize: pageSize);
        var requestsDTO = _mapper.Map<List<UserRequest>>(requests);
        response.Result = requestsDTO;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }
}
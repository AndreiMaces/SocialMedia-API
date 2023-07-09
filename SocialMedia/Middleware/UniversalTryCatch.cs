using System.Net;
using SocialMedia.Models;
namespace SocialMedia.Middleware;

public class UniversalTryCatch
{
    private readonly RequestDelegate _next;

    public UniversalTryCatch(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var response = new APIResponse();
            response.ErrorMessages.Add(ex.Message);
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.IsSuccess = false;
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
using ExHandler.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactManager.Middleware;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;
        if (exception is BaseException e)
        {
            httpContext.Response.StatusCode = (int)e.StatusCode;
            problemDetails.Title = e.Message + e.Initiator;
        }
        else
        {
            problemDetails.Title = exception.Message;
        }
        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
        problemDetails.Status = httpContext.Response.StatusCode;
        httpContext.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(problemDetails);
        await httpContext.Response.WriteAsync(json, cancellationToken).ConfigureAwait(false);

        return true;
    }
}
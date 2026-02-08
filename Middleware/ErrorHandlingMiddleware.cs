using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> _logger)
    {
        logger = _logger;
        _next = next;
    }




    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
            "Unhandled exception for request {Path}",
            context.Request.Path);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(
                new { message = "Internal Server Error" });
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        // Custom exception types
        if (exception is ArgumentException) statusCode = HttpStatusCode.BadRequest;
        if (exception is KeyNotFoundException) statusCode = HttpStatusCode.NotFound;

        var response = new
        {
            error = exception.Message,
            statusCode = (int)statusCode
        };

        var payload = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(payload);
    }
}

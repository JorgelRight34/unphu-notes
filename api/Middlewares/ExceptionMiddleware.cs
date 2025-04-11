using System;
using System.Net;
using System.Text.Json;
using api.Errors;

namespace api.Middlewares;

public class ExceptionMiddleware(
    RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IConfiguration config
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, config.GetConnectionString("DefaultConnection"), ex.StackTrace)
                : new ApiException(context.Response.StatusCode, config.GetConnectionString("DefaultConnection"), "Internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}

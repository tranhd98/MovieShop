using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

using Serilog.Events;

namespace MovieShopMVC.infra;

public class MovieShopExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MovieShopExceptionMiddleware> _logger;

    public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        Log.Information("Inside exception Middleware");

        try
        {
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            Log.Fatal("Exception happened, handle here");
            await HandleExceptionLogic(httpContext, ex);
        }
    }

    private async Task HandleExceptionLogic(HttpContext httpContext, Exception exception)
    {
        Log.Fatal("Something went wrong");

        var exceptionDetails = new
        {
            ExceptionMessage = exception.Message,
            ExceptionStackTrace = exception.StackTrace,
            ExceptionType = exception.GetType(),
            ExceptionDetails = exception.InnerException?.Message,
            ExceptionDateTime = DateTime.UtcNow,
            Path = httpContext.Request.Path,
            HttpMethod = httpContext.Request.Method,
            User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null,
        };
        Log.Fatal(exceptionDetails.ExceptionMessage);
        httpContext.Response.Redirect("/home/error");
        await Task.CompletedTask;
    }
}
public static class MovieShopExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MovieShopExceptionMiddleware>();
    }
}
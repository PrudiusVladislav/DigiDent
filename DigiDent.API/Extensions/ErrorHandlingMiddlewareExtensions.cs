using DigiDent.API.Middleware;

namespace DigiDent.API.Extensions;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IServiceCollection AddErrorHandlingMiddleware(this IServiceCollection services)
    {
        return services.AddTransient<ErrorHandlingMiddleware>();
    }
    
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
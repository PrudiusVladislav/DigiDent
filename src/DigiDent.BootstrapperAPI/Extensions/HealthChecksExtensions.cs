using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace DigiDent.BootstrapperAPI.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddAppHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration
                .GetConnectionString("SqlServer")!);
        
        return services;
    }
    
    public static IApplicationBuilder UseAppHealthChecks(
        this IApplicationBuilder app)
    {
        app.UseHealthChecks("/_health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        return app;
    }
}
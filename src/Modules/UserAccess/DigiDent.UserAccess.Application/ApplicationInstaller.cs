using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => 
            options.Register(
                typeof(ApplicationInstaller).Assembly));
        
        return services;
    }
}
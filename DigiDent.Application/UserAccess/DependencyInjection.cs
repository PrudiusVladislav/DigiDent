using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Application.UserAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => 
            options.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));
        return services;
    }
}
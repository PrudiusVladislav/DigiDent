using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Domain;

public static class DomainAssembly
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<UsersDomainService>();
        return services;
    }
}
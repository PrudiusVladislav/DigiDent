using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Domain.UserAccessContext;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessDomain(this IServiceCollection services)
    {
        services.AddScoped<UsersDomainService>();
        return services;
    }
}
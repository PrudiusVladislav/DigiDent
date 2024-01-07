using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Application.UserAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessApplication(this IServiceCollection services)
    {
        services.AddMediator();
        return services;
    }
}
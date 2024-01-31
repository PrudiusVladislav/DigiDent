using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.EFCorePersistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}
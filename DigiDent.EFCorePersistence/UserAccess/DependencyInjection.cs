using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.EFCorePersistence.UserAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddDbContext<UserAccessDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(UserAccessDbContext).Assembly.FullName);
            });
            options.AddInterceptors(sp
                .GetRequiredService<PublishDomainEventsInterceptor>());
        });
        
        services.AddTransient<IUsersRepository, UsersRepository>(); 
        return services;
    }
}
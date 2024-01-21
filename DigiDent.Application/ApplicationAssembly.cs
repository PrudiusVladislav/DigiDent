using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Application;

public static class ApplicationAssembly
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => 
            options.RegisterServicesFromAssembly(
                typeof(ApplicationAssembly).Assembly));
        
        services.AddAutoMapper(typeof(ApplicationAssembly).Assembly);
        
        return services;
    }
}
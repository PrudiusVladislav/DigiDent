using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => 
            options.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));
        
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}
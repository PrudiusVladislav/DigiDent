using DigiDent.InventoryManagement.Domain.Items;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.InventoryManagement.Domain;

public static class DomainInstaller
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddScoped<InventoryItemsDomainService>();
        return services;
    }
}
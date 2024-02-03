using Microsoft.EntityFrameworkCore;

namespace DigiDent.BootstrapperAPI.Extensions;

public static class MigrationExtensions
{
    /// <summary>
    /// Applies migrations for the specified DbContext
    /// </summary>
    /// <param name="app"> The application builder </param>
    /// <typeparam name="T"> The DbContext type </typeparam>
    public static void ApplyMigrations<T>(this IApplicationBuilder app)
        where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        using T context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.Migrate();
    }
}
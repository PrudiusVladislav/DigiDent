using DigiDent.BootstrapperAPI.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddMessageBroker(builder.Configuration);
    
    builder.Services.AddModulesDependencies(
        builder.Configuration);

    builder.Services.AddErrorHandlingMiddleware();
    builder.Services.AddHttpContextAccessor();
    
    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("SqlServer")!);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.ApplyDatabaseMigrations();
    }
    
    app.UseHttpsRedirection();
    
    app.UseHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("/api")
        .MapModulesEndpoints();
}

app.Run();
using System.Text.Json.Serialization;
using DigiDent.BootstrapperAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddMessageBroker(builder.Configuration);
    builder.Services.AddModulesDependencies(builder.Configuration);

    builder.Services.AddErrorHandlingMiddleware();
    builder.Services.AddHttpContextAccessor();
    
    builder.Services.AddAppHealthChecks(builder.Configuration);

    builder.Services.ConfigureJsonSerialization();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.ApplyDatabaseMigrations();
    }
    
    app.UseHttpsRedirection();
    
    app.UseAppHealthChecks();
    
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("/api")
        .MapModulesEndpoints();
}

app.Run();
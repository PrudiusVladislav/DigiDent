using DigiDent.BootstrapperAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddMessageBroker(builder.Configuration);
    
    builder.Services.AddModulesDependencies(
        builder.Configuration);

    builder.Services.AddErrorHandlingMiddleware();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.ApplyDatabaseMigrations();
    }
    
    app.UseHttpsRedirection();
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("/api")
        .MapModulesEndpoints();
}

app.Run();
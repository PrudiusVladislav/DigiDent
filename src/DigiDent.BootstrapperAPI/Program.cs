using DigiDent.BootstrapperAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddModulesDependencies(
        builder.Configuration);

    builder.Services.AddErrorHandlingMiddleware();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("/api")
        .MapModulesEndpoints();
}

app.Run();
using DigiDent.API.Endpoints.UserAccess;
using DigiDent.API.Extensions;
using DigiDent.Application;
using DigiDent.Domain.UserAccessContext;
using DigiDent.EFCorePersistence;
using DigiDent.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddUserAccessDomain()
        .AddUserAccessInfrastructure(builder.Configuration
            .GetSection("Authentication:Jwt"));

    builder.Services
        .AddEFCorePersistence(builder.Configuration)
        .AddApplication();

    builder.Services.AddErrorHandlingMiddleware();
}
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapGroup("/api")
        .MapUserAccessEndpoints();
}

app.Run();

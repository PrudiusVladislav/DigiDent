using DigiDent.API.Endpoints.UserAccess;
using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess;
using DigiDent.Domain.UserAccessContext;
using DigiDent.EFCorePersistence.UserAccess;
using DigiDent.Infrastructure.UserAccess;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddUserAccessDomain()
        .AddUserAccessApplication()
        .AddUserAccessPersistence(builder.Configuration)
        .AddUserAccessInfrastructure(builder.Configuration);

    builder.Services.AddErrorHandlingMiddleware();
}
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapUserAccessEndpoints();
}

app.Run();

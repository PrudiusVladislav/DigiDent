using DigiDent.API.Endpoints.UserAccess;
using DigiDent.API.Extensions;
using DigiDent.Application;
using DigiDent.Application.UserAccess;
using DigiDent.Domain.UserAccessContext;
using DigiDent.EFCorePersistence.ClinicCore;
using DigiDent.EFCorePersistence.UserAccess;
using DigiDent.Infrastructure.ClinicCore;
using DigiDent.Infrastructure.UserAccess;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddUserAccessDomain()
        .AddUserAccessPersistence(builder.Configuration)
        .AddUserAccessInfrastructure(builder.Configuration
            .GetSection("Authentication:Jwt"));

    builder.Services
        .AddClinicCorePersistence(builder.Configuration)
        .AddClinicCoreInfrastructure();

    builder.Services.AddApplication();

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

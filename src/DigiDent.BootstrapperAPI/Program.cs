using DigiDent.BootstrapperAPI.Extensions;
using DigiDent.ClinicManagement.EFCorePersistence;
using DigiDent.UserAccess.EFCorePersistence;

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
        app.ApplyMigrations<UserAccessDbContext>();
        app.ApplyMigrations<ClinicManagementDbContext>();
    }
    
    app.UseHttpsRedirection();
    app.UseErrorHandlingMiddleware();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("/api")
        .MapModulesEndpoints();
}

app.Run();
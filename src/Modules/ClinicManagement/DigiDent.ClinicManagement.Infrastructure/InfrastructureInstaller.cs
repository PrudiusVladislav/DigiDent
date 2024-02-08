using Amazon;
using Amazon.S3;
using DigiDent.ClinicManagement.Application.Abstractions;
using DigiDent.ClinicManagement.Infrastructure.Services;
using DigiDent.Shared.Infrastructure.Time;
using DigiDent.Shared.Kernel.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;

namespace DigiDent.ClinicManagement.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddFactories()
            .ConfigurePDFLicense()
            .AddServices();
    }
    
    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<IPersonFactory, PersonFactory>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    
    private static IServiceCollection ConfigurePDFLicense(
        this IServiceCollection services)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonS3, AmazonS3Client>(options => 
            new AmazonS3Client(RegionEndpoint.EUNorth1));
        
        services.AddSingleton<IMediaFilesS3Service, MediaFilesS3Service>();
        return services;
    }
}
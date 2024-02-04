using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Infrastructure.Services;
using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Notification.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.SetDefaultSettings();
            
            client.UseBasicAuthentication(
                configuration["MailJet:ApiKey"],
                configuration["MailJet:ApiSecret"]);
        });
        
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<IEmailContentFactory, EmailContentFactory>();
        return services;
    }
}
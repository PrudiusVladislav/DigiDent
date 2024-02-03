using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.UserAccess.IntegrationEvents;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace DigiDent.BootstrapperAPI.Extensions;

public static class MessageBrokerInstaller
{
    public static void AddMessageBroker(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRebus(rebus => rebus
                .Routing(r => r
                    .TypeBased()
                    .MapAssemblyOf<UserSignedUpIntegrationEvent>("notification-queue")
                    .MapAssemblyOf<AppointmentCreatedIntegrationEvent>("notification-queue"))
                .Transport(t => t
                    .UseRabbitMq(
                        configuration.GetConnectionString("RabbitMq"),
                        inputQueueName: "notification-queue"))
                .Timeouts(t => t
                    .StoreInSqlServer(
                        configuration.GetConnectionString("SqlServer"),
                        tableName: "Timeouts")),
            onCreated: async bus =>
            {
                await bus.Subscribe<UserActivatedIntegrationEvent>();
                await bus.Subscribe<AppointmentCreatedIntegrationEvent>();
            });
    }
}
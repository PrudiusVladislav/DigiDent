using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace DigiDent.BootstrapperAPI.Extensions;

public static class JsonSerializationExtensions
{
    public static IServiceCollection ConfigureJsonSerialization(
        this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            var enumConverter = new JsonStringEnumConverter();
            options.SerializerOptions.Converters.Add(enumConverter);
        });

        return services;
    }
}
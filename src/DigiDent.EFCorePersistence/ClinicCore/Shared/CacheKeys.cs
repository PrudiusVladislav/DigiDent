using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared;

public static class CacheKeys
{
    public const string ProvidedServicesAll = "provided-services-all";
    
    public static readonly Func<ProvidedServiceId, string> ProvidedServiceById = 
        id => $"provided-service-{id.Value}";
}
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class Service: IEntity<ServiceId, int>
{
    public ServiceId Id { get; init; }
    
    public string Name { get; init; }
    public string? Description { get; init; }
    
    public Money Price { get; init; }
}
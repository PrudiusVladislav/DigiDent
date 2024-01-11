using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects.Ids;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Domain.VisitsScheduleContext.Visits;

public class Service: IEntity<ServiceId, int>
{
    public ServiceId Id { get; init; }
    
    public string Name { get; init; }
    public string? Description { get; init; }
    
    public Money Price { get; init; }
}
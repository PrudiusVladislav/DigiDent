using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class DentalProcedure: IEntity<DentalProcedureId, int>
{
    public DentalProcedureId Id { get; init; }
    
    public string Name { get; init; }
    public string? Description { get; init; }
    
    public Money Price { get; init; }
}
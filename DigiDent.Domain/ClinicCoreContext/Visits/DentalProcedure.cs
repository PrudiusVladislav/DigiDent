using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class DentalProcedure: IEntity<DentalProcedureId, Guid>
{
    public DentalProcedureId Id { get; init; }
    public DentalProcedureDetails Details { get; private set; }
    public TimeDuration UsualDuration { get; private set; }
    public Money Price { get; private set; }
    
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    
    // for EF Core
    internal DentalProcedure() { }
    
    internal DentalProcedure(
        DentalProcedureId id,
        DentalProcedureDetails details,
        TimeDuration usualDuration,
        Money price)
    {
        Id = id;
        Details = details;
        UsualDuration = usualDuration;
        Price = price;
    }
    
    public static DentalProcedure Create(
        DentalProcedureDetails details,
        TimeDuration usualDuration,
        Money price)
    {
        var dentalProcedureId = TypedId.New<DentalProcedureId>();
        return new DentalProcedure(
            dentalProcedureId,
            details,
            usualDuration,
            price);
    }
}
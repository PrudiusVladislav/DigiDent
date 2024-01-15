using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

public class WorkingDay: IEntity<WorkingDayId, Guid>
{
    public WorkingDayId Id { get; init; }
    public DateOnly Date { get; private set; }
    public StartEndTime StartEndTime { get; private set; }
    
    public IEmployeeId<Guid> EmployeeId { get; init; }
    
    internal WorkingDay(
        WorkingDayId id,
        DateOnly date,
        StartEndTime startEndTime,
        IEmployeeId<Guid> employeeId)
    {
        Id = id;
        Date = date;
        StartEndTime = startEndTime;
        EmployeeId = employeeId;
    }
    
    public static Result<WorkingDay> Create(
        DateOnly date,
        StartEndTime startEndTime,
        IEmployeeId<Guid> employeeId)
    {
        var workingDayId = TypedId.New<WorkingDayId>();
        return Result.Ok(new WorkingDay(
            workingDayId, 
            date,
            startEndTime,
            employeeId));
    }
}
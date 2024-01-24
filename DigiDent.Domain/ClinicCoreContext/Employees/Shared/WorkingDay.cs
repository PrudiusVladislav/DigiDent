using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

public class WorkingDay: IEntity<WorkingDayId, Guid>
{
    public WorkingDayId Id { get; init; }
    public DateOnly Date { get; private set; }
    public StartEndTime StartEndTime { get; private set; }
    
    public EmployeeId EmployeeId { get; init; }
    public Employee Employee { get; init; } = null!;
    
    internal WorkingDay(
        WorkingDayId id,
        DateOnly date,
        StartEndTime startEndTime,
        EmployeeId employeeId)
    {
        Id = id;
        Date = date;
        StartEndTime = startEndTime;
        EmployeeId = employeeId;
    }
    
    public static Result<WorkingDay> Create(
        DateOnly date,
        StartEndTime startEndTime,
        EmployeeId employeeId)
    {
        var workingDayId = TypedId.New<WorkingDayId>();
        return Result.Ok(new WorkingDay(
            workingDayId, 
            date,
            startEndTime,
            employeeId));
    }
}
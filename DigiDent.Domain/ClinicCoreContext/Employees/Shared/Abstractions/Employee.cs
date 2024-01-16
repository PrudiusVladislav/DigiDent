using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public abstract class Employee: 
    AggregateRoot,
    IEmployee<EmployeeId, Guid>
{
    public EmployeeId Id { get; init; }
    public Email Email { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set;}
    public FullName FullName { get; protected set; }
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; protected set;}
    public EmployeeStatus Status { get; protected set;}
    
    public ICollection<WorkingDay> WorkingDays { get; set; }
        = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; set; }
        = new List<SchedulePreference>();
}
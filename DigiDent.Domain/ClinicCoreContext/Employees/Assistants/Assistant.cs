using DigiDent.Domain.ClinicCoreContext.Employees.Assistants.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Assistants;

public class Assistant: 
    AggregateRoot,
    IEntity<AssistantId, Guid>,
    IPerson,
    IEmployee
{
    public AssistantId Id { get; init; }
    public Email Email { get; private set; }
    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; private set; }
    
    public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; set; } 
        = new List<SchedulePreference>();

    internal Assistant(
        AssistantId id,
        Email email,
        PhoneNumber phoneNumber,
        FullName fullName)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
        FullName = fullName;
    }
    
    public static Assistant Create(
        Email email,
        PhoneNumber phoneNumber,
        FullName fullName)
    {
        var assistantId = TypedId.New<AssistantId>();
        return new Assistant(
            assistantId,
            email,
            phoneNumber,
            fullName);
    }
        
}
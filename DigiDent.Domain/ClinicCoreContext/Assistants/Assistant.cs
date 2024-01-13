using DigiDent.Domain.ClinicCoreContext.Assistants.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Assistants;

public class Assistant: 
    AggregateRoot,
    IEntity<AssistantId, Guid>,
    IPerson,
    IEmployee
{
    public AssistantId Id { get; init; }

    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    public FullName FullName { get; private set; }
    public Gender Gender { get; }
    public DateTime DateOfBirth { get; private set; }
    
    public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();
}
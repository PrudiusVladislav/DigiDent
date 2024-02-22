using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.IntegrationEvents;

public class EmployeeAddedIntegrationEvent: IIntegrationEvent
{
    public DateTime TimeOfOccurrence { get; init; }
    public Guid Id { get; init; }
    public FullName FullName { get; init; } = null!;
    public Email Email { get; init; } = null!;
    public PhoneNumber PhoneNumber { get; init; } = null!;
    public Role Role { get; init; }
}
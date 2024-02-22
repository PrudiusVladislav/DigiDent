using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Events;

public record EmployeeAddedDomainEvent(
    DateTime TimeOfOccurrence,
    Employee AddedEmployee) : IDomainEvent;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Assistants.ValueObjects;

public record AssistantId(Guid Value): TypedId<Guid>(Value);
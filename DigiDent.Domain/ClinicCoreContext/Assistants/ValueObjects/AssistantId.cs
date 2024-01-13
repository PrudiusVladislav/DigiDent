using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Assistants.ValueObjects;

public record AssistantId(Guid Value): TypedId<Guid>(Value);
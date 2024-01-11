using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record UserId(Guid Value): TypedId<Guid>(Value);
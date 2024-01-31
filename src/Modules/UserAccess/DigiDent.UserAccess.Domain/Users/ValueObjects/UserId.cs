using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record UserId(Guid Value): TypedId<Guid>(Value);
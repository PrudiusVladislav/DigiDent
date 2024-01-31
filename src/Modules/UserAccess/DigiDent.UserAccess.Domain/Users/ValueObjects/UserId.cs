using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.ValueObjects;

public record UserId(Guid Value): TypedId<Guid>(Value);
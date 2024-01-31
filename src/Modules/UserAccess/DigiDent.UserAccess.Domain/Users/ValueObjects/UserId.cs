using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.ValueObjects;

public record UserId(Guid Value): TypedId<Guid>(Value);
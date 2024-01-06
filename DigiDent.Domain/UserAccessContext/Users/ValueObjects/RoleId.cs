using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record RoleId(int Value): TypedId<int>(Value);
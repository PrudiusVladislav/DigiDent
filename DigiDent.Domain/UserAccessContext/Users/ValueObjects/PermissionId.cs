using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Users.ValueObjects;

public record PermissionId(int Value): TypedId<int>(Value);
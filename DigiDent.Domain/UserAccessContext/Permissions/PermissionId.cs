using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Roles;

public record PermissionId(int Value): TypedId<int>(Value);
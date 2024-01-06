using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Permissions;

public record PermissionId(int Value): TypedId<int>(Value);
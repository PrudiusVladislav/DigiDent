using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Roles;

public record RoleId(int Value): TypedId<int>(Value);
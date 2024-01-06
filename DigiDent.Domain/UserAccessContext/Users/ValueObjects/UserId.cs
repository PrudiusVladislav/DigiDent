using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Roles;

public record UserId(Guid Value): TypedId<Guid>(Value);
using DigiDent.Domain.UserAccessContext.Users;

namespace DigiDent.Application.UserAccess.Abstractions;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}
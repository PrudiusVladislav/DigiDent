using DigiDent.Domain.UserAccessContext.Users;

namespace DigiDent.Application.UserAccess;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}
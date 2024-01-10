using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.Infrastructure.UserAccess.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string Secret { get; init; }
    public TimeSpan TokenLifetime { get; init; }
    public TimeSpan RefreshTokenLifetime { get; init; }
    public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(Secret));
}
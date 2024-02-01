using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.UserAccess.Infrastructure.Authentication;

public record JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Secret { get; init; } = string.Empty;
    public TimeSpan TokenLifetime { get; init; }
    public TimeSpan RefreshTokenLifetime { get; init; }
    public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(Secret));
}
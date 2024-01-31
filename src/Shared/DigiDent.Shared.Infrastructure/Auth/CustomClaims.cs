using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DigiDent.Shared.Infrastructure.Auth;

public class CustomClaims
{
    public const string NameIdentifier = ClaimTypes.NameIdentifier;
    public const string Jti = JwtRegisteredClaimNames.Jti;
    public const string Email = JwtRegisteredClaimNames.Email;
    public const string Role = "role";
    public const string Exp = JwtRegisteredClaimNames.Exp;
}
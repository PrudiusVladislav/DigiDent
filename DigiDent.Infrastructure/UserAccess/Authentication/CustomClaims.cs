using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DigiDent.Infrastructure.UserAccess.Authentication;

public class CustomClaims
{
    public const string NameIdentifier = ClaimTypes.NameIdentifier;
    public const string Jti = JwtRegisteredClaimNames.Jti;
    public const string Email = JwtRegisteredClaimNames.Email;
    public const string Role = "role";
}
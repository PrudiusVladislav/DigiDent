﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.UserAccess.Infrastructure.Authentication;

public record JwtOptions(
    string Issuer,
    string Audience,
    string Secret,
    TimeSpan TokenLifetime,
    TimeSpan RefreshTokenLifetime)
{
    public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(Secret));
}
﻿using DigiDent.UserAccess.Application.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.UserAccess.EFCorePersistence.RefreshTokens;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Token);
        builder.HasOne(rt => rt.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(rt => rt.UserId);
    }
}
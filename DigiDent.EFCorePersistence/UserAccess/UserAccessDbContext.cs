﻿using System.Reflection;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

public class UserAccessDbContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Consider dividing the bounded contexts into separate schemas
        //modelBuilder.HasDefaultSchema()
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type
                .GetCustomAttributes(typeof(UserAccessEntityConfigurationAttribute), true)
                .Any());
    }
    
}
﻿using System.Reflection;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

public class UserAccessDbContext: DbContext
{
    internal const string UserAccessSchema = "User_Access";
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(UserAccessSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type
                .GetCustomAttributes(typeof(UserAccessEntityConfigurationAttribute), true)
                .Any());
    }
    
}
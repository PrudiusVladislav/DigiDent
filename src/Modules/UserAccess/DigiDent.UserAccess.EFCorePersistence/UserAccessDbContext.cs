using DigiDent.UserAccess.Application.Tokens;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.EFCorePersistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.UserAccess.EFCorePersistence;

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
        modelBuilder.HasDefaultSchema(
            ConfigurationConstants.UserAccessSchemaName);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UserAccessDbContext).Assembly);
    }
    
}
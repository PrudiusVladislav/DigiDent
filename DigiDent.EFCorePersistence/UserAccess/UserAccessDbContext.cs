using DigiDent.Domain.UserAccessContext.Permissions;
using DigiDent.Domain.UserAccessContext.Roles;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

public class UserAccessDbContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;

    public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);
    }
    
}
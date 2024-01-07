using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

public class UserAccessDbContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessDbContext).Assembly);
    }
    
}
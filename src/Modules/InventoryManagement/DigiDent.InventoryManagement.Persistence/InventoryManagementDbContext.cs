using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.InventoryManagement.Persistence;

public class InventoryManagementDbContext: DbContext
{
    public DbSet<InventoryItem> Items { get; set; } = null!;
    public DbSet<InventoryAction> Actions { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    public InventoryManagementDbContext(
        DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(
            ConfigurationConstants.InventoryManagementSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InventoryManagementDbContext).Assembly);
    }
}
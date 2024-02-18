using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.InventoryManagement.Persistence.Actions;

public class InventoryActionsConfiguration
    : AggregateRootConfiguration<InventoryAction, InventoryActionId, Guid>
{
    protected override void ConfigureEntity(
        EntityTypeBuilder<InventoryAction> builder)
    {
        builder
            .HasOne(action => action.ActionPerformer)
            .WithMany(employee => employee.Actions)
            .HasForeignKey(action => action.ActionPerformerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(action => action.InventoryItem)
            .WithMany(item => item.InventoryActions)
            .HasForeignKey(action => action.InventoryItemId);
        
        builder
            .Property(action => action.Quantity)
            .HasConversion(SharedConverters.QuantityConverter);
    }

    protected override void ConfigureAggregateRoot(
        EntityTypeBuilder<InventoryAction> builder)
    {
    }
}
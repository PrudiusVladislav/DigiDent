﻿using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsConfiguration
    : AggregateRootConfiguration<InventoryItem, InventoryItemId, Guid>
{
    protected override void ConfigureEntity(
        EntityTypeBuilder<InventoryItem> builder)
    {
        builder
            .Property(item => item.Name)
            .HasConversion(
                name => name.Value,
                value => new ItemName(value));

        builder
            .HasIndex(item => item.Name)
            .IsUnique();

        builder
            .Property(item => item.Quantity)
            .HasConversion(SharedConverters.QuantityConverter);

        builder
            .HasMany(item => item.InventoryActions)
            .WithOne(action => action.InventoryItem)
            .HasForeignKey(action => action.InventoryItemId);
    }

    protected override void ConfigureAggregateRoot(
        EntityTypeBuilder<InventoryItem> builder)
    {
    }
}
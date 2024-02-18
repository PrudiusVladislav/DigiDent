using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.InventoryManagement.Persistence.Requests;

public class RequestsConfiguration
    : AggregateRootConfiguration<Request, RequestId, Guid>
{
    protected override void ConfigureEntity(
        EntityTypeBuilder<Request> builder)
    {
        builder
            .Property(request => request.RequestedQuantity)
            .HasConversion(SharedConverters.QuantityConverter);
        
        builder
            .HasOne(request => request.RequestedItem)
            .WithMany(item => item.Requests)
            .HasForeignKey(request => request.RequestedItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(request => request.Requester)
            .WithMany(employee => employee.Requests)
            .HasForeignKey(request => request.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    protected override void ConfigureAggregateRoot(
        EntityTypeBuilder<Request> builder)
    {
    }
}
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiDent.InventoryManagement.Persistence.Shared;

public class SharedConverters
{
    public static ValueConverter<Quantity, int> QuantityConverter =>
        new(
            quantity => quantity.Value,
            value => new Quantity(value));
}
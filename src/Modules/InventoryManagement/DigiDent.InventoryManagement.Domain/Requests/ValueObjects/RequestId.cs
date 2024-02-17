using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Requests.ValueObjects;

public record RequestId(Guid Value): ITypedId<Guid>;
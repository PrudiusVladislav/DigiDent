namespace DigiDent.Domain.SharedKernel;

public abstract class Entity<TId, TIdValue> where TId : TypedId<TIdValue>
{
    public TId Id { get; protected set; } = default!;
}
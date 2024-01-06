namespace DigiDent.Domain.SharedKernel;

public interface IEntity<TId, TIdValue> where TId : TypedId<TIdValue>
{
    TId Id { get; init; }
}
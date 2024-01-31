namespace DigiDent.Shared.Domain.Abstractions;

/// <summary>
/// Interface that all entities implement. Ensures that all entities have an Id.
/// </summary>
/// <typeparam name="TId">The type of the Id.</typeparam>
/// <typeparam name="TIdValue">The type of the TId's primitive value.</typeparam>
public interface IEntity<TId, TIdValue> where TId : ITypedId<TIdValue>
{
    TId Id { get; init; }
}
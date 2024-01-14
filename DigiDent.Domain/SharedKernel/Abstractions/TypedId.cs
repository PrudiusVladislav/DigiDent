namespace DigiDent.Domain.SharedKernel.Abstractions;

/// <summary>
/// Represents a strongly-typed identifier for entities.
/// </summary>
/// <param name="Value">The primitive value of the identifier.</param>
/// <typeparam name="T">The type of the identifier.</typeparam>
public abstract record TypedId<T>(T Value);

/// <summary>
/// Class that contains factory methods for creating <see cref="TypedId{TId}"/>s.
/// </summary>
public static class TypedId
{
    /// <summary>
    /// Creates a new <see cref="TypedId{TId}"/> with a <see cref="Guid"/> value.
    /// </summary>
    /// <typeparam name="TId">The type of the <see cref="TypedId{TId}"/>.</typeparam>
    /// <returns></returns>
    public static TId New<TId>() where TId : TypedId<Guid>
    {
        return Create<Guid, TId>(Guid.NewGuid());
    }
    
    /// <summary>
    /// Creates a new <see cref="TypedId{TId}"/> with the specified value.
    /// </summary>
    /// <param name="value">The value of the <see cref="TypedId{TId}"/>.</param>
    /// <typeparam name="TIdValue">The primitive type of the value behind the id.</typeparam>
    /// <typeparam name="TId">The type of the <see cref="TypedId{TId}"/>.</typeparam>
    /// <returns></returns>
    public static TId Create<TIdValue, TId>(TIdValue value) 
        where TId : TypedId<TIdValue>
    {
        return (TId)Activator.CreateInstance(typeof(TId), value)!;
    }
}
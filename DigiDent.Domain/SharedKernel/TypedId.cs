namespace DigiDent.Domain.SharedKernel;

/// <summary>
/// Represents a strongly-typed identifier for entities.
/// </summary>
/// <param name="Value">
/// The primitive value of the identifier.
/// </param>
/// <typeparam name="T">
///  The type of the identifier.
/// </typeparam>
public abstract record TypedId<T>(T Value)
{
    /// <summary>
    ///  Creates a new <see cref="TypedId{TId}"/> with a new <see cref="Guid"/> value.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <returns></returns>
    public static TId NewGuid<TId>() where TId : TypedId<Guid>
    {
        return TypedId<Guid>.Create<TId>(Guid.NewGuid());
    }
    
    /// <summary>
    /// Creates a new <see cref="TypedId{TId}"/> with the specified value.
    /// </summary>
    /// <param name="value">
    /// The value of the <see cref="TypedId{TId}"/>.
    /// </param>
    /// <typeparam name="TId">
    /// The type of the <see cref="TypedId{TId}"/>.
    /// </typeparam>
    /// <returns></returns>
    public static TId Create<TId>(T value) where TId : TypedId<T>
    {
        return (TId)Activator.CreateInstance(typeof(TId), value)!;
    }
}
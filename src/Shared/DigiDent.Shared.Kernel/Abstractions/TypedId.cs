namespace DigiDent.Shared.Kernel.Abstractions;

/// <summary>
/// Class that contains factory methods for creating <see cref="ITypedId{TId}"/>s.
/// </summary>
public static class TypedId
{
    /// <summary>
    /// Creates a new <see cref="ITypedId{TId}"/> with a <see cref="Guid"/> value.
    /// </summary>
    /// <typeparam name="TId">The type of the <see cref="ITypedId{TId}"/>.</typeparam>
    /// <returns></returns>
    public static TId New<TId>() where TId : ITypedId<Guid>
    {
        return Create<TId, Guid>(Guid.NewGuid());
    }
    
    /// <summary>
    /// Creates a new <see cref="ITypedId{TId}"/> with the specified value.
    /// </summary>
    /// <param name="value">The value of the <see cref="ITypedId{TId}"/>.</param>
    /// <typeparam name="TIdValue">The primitive type of the value behind the id.</typeparam>
    /// <typeparam name="TId">The type of the <see cref="ITypedId{TId}"/>.</typeparam>
    /// <returns></returns>
    public static TId Create<TId, TIdValue>(TIdValue value) 
        where TId : ITypedId<TIdValue>
    {
        return (TId)Activator.CreateInstance(typeof(TId), value)!;
    }
    

}
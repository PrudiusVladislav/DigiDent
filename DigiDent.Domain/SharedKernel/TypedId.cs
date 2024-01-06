namespace DigiDent.Domain.SharedKernel;

public abstract record TypedId<T>(T Value)
{
    public static TId NewGuid<TId>() where TId : TypedId<TId>
    {
        return TypedId<Guid>.Create<TId>(Guid.NewGuid());
    }
    
    public static TId Create<TId>(T value) where TId : TypedId<TId>
    {
        return (TId)Activator.CreateInstance(typeof(TId), value)!;
    }
}
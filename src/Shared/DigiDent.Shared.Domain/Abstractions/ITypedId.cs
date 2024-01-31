namespace DigiDent.Shared.Domain.Abstractions;

public interface ITypedId<out T>
{
    T Value { get; }
}
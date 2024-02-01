namespace DigiDent.Shared.Kernel.Abstractions;

public interface ITypedId<out T>
{
    T Value { get; }
}
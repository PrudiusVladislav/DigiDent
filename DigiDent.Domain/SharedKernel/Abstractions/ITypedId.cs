namespace DigiDent.Domain.SharedKernel.Abstractions;

public interface ITypedId<T>
{
    T Value { get; }
}
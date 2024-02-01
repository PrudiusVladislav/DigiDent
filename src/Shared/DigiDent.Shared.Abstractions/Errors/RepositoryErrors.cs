using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.Shared.Abstractions.Errors;

public static class RepositoryErrors
{
    public static Error EntityNotFound<TEntity>(Guid id)
        where TEntity : class
    {
        return new Error(
            ErrorType.NotFound,
            $"{typeof(TEntity).Name}Repository",
            $"Entity {typeof(TEntity).Name} with id '{id}' not found");
    }
}
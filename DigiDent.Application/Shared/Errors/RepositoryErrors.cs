using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.Shared.Errors;

public static class RepositoryErrors
{
    public static Error EntityNotFound<TEntity>(Guid id)
        where TEntity : class
    {
        return new Error(
            ErrorType.NotFound,
            $"{typeof(TEntity).Name}Repository",
            $"Entity {typeof(TEntity).Name} with id {id} not found");
    }
}
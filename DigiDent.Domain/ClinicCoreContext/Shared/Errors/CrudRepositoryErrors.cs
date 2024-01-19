using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Errors;

public static class CrudRepositoryErrors
{
    public static Error EntityNotFound(string entityName, Guid id)
     => new (
         ErrorType.NotFound, 
         entityName, 
         $"{entityName} with id {id} not found");
    
}
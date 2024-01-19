using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Assistants;

public interface IAssistantRepository: ICrudRepository<Assistant, EmployeeId, Guid>
{
    
}
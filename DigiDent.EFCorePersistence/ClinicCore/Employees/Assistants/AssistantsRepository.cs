using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Assistants;

public class AssistantsRepository :
    EmployeesRepository<Assistant>,
    IAssistantsRepository
{
    public AssistantsRepository(ClinicCoreDbContext context)
        : base(context)
    {
    }
}
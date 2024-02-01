using DigiDent.ClinicManagement.Domain.Employees.Assistants;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Assistants;

public class AssistantsRepository :
    EmployeesRepository<Assistant>,
    IAssistantsRepository
{
    public AssistantsRepository(ClinicManagementDbContext context)
        : base(context)
    {
    }
}
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Application.ClinicCore.Abstractions;

public interface IPersonFactory
{
    Type GetPersonTypeFromRole(Role role);
    TPerson CreatePerson<TPerson>(PersonCreationArgs args)
        where TPerson : class, IPerson<IPersonId>;
}
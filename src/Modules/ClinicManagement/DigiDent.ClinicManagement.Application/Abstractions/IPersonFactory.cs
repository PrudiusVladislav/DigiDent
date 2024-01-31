using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Application.ClinicCore.Abstractions;

/// <summary>
/// Defines a factory for creating <see cref="IPerson{TPersonId}"/> objects.
/// </summary>
public interface IPersonFactory
{
    (Type, Type) GetPersonTypesFromRole(Role role);

    TPerson CreatePerson<TPerson, TId>(PersonCreationArgs args)
        where TPerson : class, IPerson<TId>
        where TId : IPersonId;
}
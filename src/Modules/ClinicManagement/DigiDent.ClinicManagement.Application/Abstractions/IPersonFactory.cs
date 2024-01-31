using DigiDent.ClinicManagement.Domain.Shared.Abstractions;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Application.Abstractions;

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
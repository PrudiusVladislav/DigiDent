using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

public record PersonCreationArgs(
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber);
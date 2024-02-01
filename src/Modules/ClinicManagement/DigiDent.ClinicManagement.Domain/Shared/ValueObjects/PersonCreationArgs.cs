using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Shared.ValueObjects;

public record PersonCreationArgs(
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber);
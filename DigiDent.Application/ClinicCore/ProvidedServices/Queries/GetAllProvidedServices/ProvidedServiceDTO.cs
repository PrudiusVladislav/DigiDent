namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;

public sealed class ProvidedServiceDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public TimeSpan UsualDuration { get; init; }
}
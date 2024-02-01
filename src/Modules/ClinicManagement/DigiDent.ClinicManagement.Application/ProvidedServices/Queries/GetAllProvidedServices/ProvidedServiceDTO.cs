namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetAllProvidedServices;

public class ProvidedServiceDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public TimeSpan UsualDuration { get; init; }
}
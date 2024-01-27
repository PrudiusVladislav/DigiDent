namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;

public sealed record UpdateProvidedServiceRequest(
    decimal? Price,
    int? Duration);


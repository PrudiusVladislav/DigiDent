namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.UpdateService;

public sealed record UpdateProvidedServiceRequest(
    decimal? Price,
    int? Duration);


namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;

public record UpdateProvidedServiceRequest(
    decimal? Price,
    int? Duration);


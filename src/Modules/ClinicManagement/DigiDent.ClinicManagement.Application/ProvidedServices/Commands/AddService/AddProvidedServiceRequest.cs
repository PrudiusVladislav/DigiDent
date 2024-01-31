namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;

public sealed record AddProvidedServiceRequest(
    string Name,
    string Description,
    int UsualDuration,
    decimal Price);
namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.AddService;

public sealed record AddProvidedServiceRequest(
    string Name,
    string Description,
    int UsualDuration,
    decimal Price);
using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;

public record GetAllProvidedServicesQuery
    : IQuery<IReadOnlyCollection<ProvidedServiceDTO>>;
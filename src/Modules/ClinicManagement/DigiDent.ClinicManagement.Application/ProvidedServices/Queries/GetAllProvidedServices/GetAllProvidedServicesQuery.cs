using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetAllProvidedServices;

public sealed record GetAllProvidedServicesQuery
    : IQuery<IReadOnlyCollection<ProvidedServiceDTO>>;
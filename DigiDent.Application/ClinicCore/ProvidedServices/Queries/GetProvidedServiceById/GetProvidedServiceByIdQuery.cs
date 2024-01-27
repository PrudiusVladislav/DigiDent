using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;

public sealed record GetProvidedServiceByIdQuery(Guid Id)
    : IQuery<SpecificProvidedServiceDTO?>;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetProvidedServiceById;

public sealed record GetProvidedServiceByIdQuery(Guid Id)
    : IQuery<SpecificProvidedServiceDTO?>;
using AutoMapper;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetProvidedServiceById;

public sealed class GetProvidedServiceByIdQueryHandler
    : IQueryHandler<GetProvidedServiceByIdQuery, SpecificProvidedServiceDTO?>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IMapper _mapper;

    public GetProvidedServiceByIdQueryHandler(
        IProvidedServicesRepository providedServicesRepository,
        IMapper mapper)
    {
        _providedServicesRepository = providedServicesRepository;
        _mapper = mapper;
    }

    public async Task<SpecificProvidedServiceDTO?> Handle(
        GetProvidedServiceByIdQuery query, CancellationToken cancellationToken)
    {
        ProvidedServiceId serviceId = new(query.Id);
        ProvidedService? providedService = await _providedServicesRepository
            .GetByIdAsync(serviceId, cancellationToken);
        
        return _mapper.Map<SpecificProvidedServiceDTO?>(providedService);
    }
}
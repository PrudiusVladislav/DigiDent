using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;

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
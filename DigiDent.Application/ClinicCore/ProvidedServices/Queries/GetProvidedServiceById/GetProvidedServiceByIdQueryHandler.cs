using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;

public class GetProvidedServiceByIdQueryHandler
    : IQueryHandler<GetProvidedServiceByIdQuery, SpecificProvidedServiceDTO?>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IMapper _mapper;

    public GetProvidedServiceByIdQueryHandler(IProvidedServicesRepository providedServicesRepository, IMapper mapper)
    {
        _providedServicesRepository = providedServicesRepository;
        _mapper = mapper;
    }

    public async Task<SpecificProvidedServiceDTO?> Handle(
        GetProvidedServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceId = new ProvidedServiceId(request.Id);
        ProvidedService? providedService = await _providedServicesRepository
            .GetByIdAsync(serviceId, cancellationToken);
        
        return _mapper.Map<SpecificProvidedServiceDTO?>(providedService);
    }
}
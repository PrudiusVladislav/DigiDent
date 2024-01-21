using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;

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

    public Task<SpecificProvidedServiceDTO?> Handle(GetProvidedServiceByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
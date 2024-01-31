using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;

public sealed class GetAllProvidedServicesQueryHandler
    : IQueryHandler<GetAllProvidedServicesQuery, IReadOnlyCollection<ProvidedServiceDTO>>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IMapper _mapper;

    public GetAllProvidedServicesQueryHandler(
        IProvidedServicesRepository providedServicesRepository,
        IMapper mapper)
    {
        _providedServicesRepository = providedServicesRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ProvidedServiceDTO>> Handle(
        GetAllProvidedServicesQuery query, CancellationToken cancellationToken)
    {
        return (await _providedServicesRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<ProvidedServiceDTO>)
            .ToList()
            .AsReadOnly();
    }
}
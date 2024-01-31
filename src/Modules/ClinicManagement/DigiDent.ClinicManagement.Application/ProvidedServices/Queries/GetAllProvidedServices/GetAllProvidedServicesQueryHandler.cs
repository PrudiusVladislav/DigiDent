using AutoMapper;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetAllProvidedServices;

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
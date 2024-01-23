using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetAllPatients;

public class GetAllPatientsQueryHandler
    : IQueryHandler<GetAllPatientsQuery, IReadOnlyCollection<PatientDTO>>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IMapper _mapper;

    public GetAllPatientsQueryHandler(
        IPatientsRepository patientsRepository, IMapper mapper)
    {
        _patientsRepository = patientsRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<PatientDTO>> Handle(
        GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        return (await _patientsRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<PatientDTO>)
            .ToList()
            .AsReadOnly();
    }
}
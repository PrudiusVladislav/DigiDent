using AutoMapper;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetAllPatients;

public sealed class GetAllPatientsQueryHandler
    : IQueryHandler<GetAllPatientsQuery, IReadOnlyCollection<PatientDTO>>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IMapper _mapper;

    public GetAllPatientsQueryHandler(
        IPatientsRepository patientsRepository,
        IMapper mapper)
    {
        _patientsRepository = patientsRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<PatientDTO>> Handle(
        GetAllPatientsQuery query, CancellationToken cancellationToken)
    {
        return (await _patientsRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<PatientDTO>)
            .ToList()
            .AsReadOnly();
    }
}
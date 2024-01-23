using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientProfile;

public class GetPatientProfileQueryHandler
    : IQueryHandler<GetPatientProfileQuery, PatientProfileDTO?>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IMapper _mapper;

    public GetPatientProfileQueryHandler(IPatientsRepository patientsRepository, IMapper mapper)
    {
        _patientsRepository = patientsRepository;
        _mapper = mapper;
    }

    public async Task<PatientProfileDTO?> Handle(
        GetPatientProfileQuery request, CancellationToken cancellationToken)
    {
        var patientId = new PatientId(request.Id);
        
        Patient? patient = await _patientsRepository.GetByIdAsync(
            patientId, cancellationToken);

        return _mapper.Map<PatientProfileDTO?>(patient);
    }
}
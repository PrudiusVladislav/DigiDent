using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryHandler
    : IQueryHandler<GetPatientByIdQuery, PatientProfileDTO?>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IMapper _mapper;

    public GetPatientByIdQueryHandler(IPatientsRepository patientsRepository, IMapper mapper)
    {
        _patientsRepository = patientsRepository;
        _mapper = mapper;
    }

    public async Task<PatientProfileDTO?> Handle(
        GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patientId = new PatientId(request.Id);
        
        Patient? patient = await _patientsRepository.GetByIdAsync(
            patientId, cancellationToken);

        return _mapper.Map<PatientProfileDTO?>(patient);
    }
}
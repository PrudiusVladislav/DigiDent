using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryHandler
    : IQueryHandler<GetPatientByIdQuery, Result<PatientProfileDTO>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientByIdQueryHandler(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<Result<PatientProfileDTO>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patientId = new PatientId(request.Id);
        Patient? patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is null)
            return Result.Fail<PatientProfileDTO>(CrudRepositoryErrors
                .EntityNotFound(nameof(Patient), patientId.Value));

        var patientDto = _mapper.Map<PatientProfileDTO>(patient);

        return Result.Ok(patientDto);
    }
}
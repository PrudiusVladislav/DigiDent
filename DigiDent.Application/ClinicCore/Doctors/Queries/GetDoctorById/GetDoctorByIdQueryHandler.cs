using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler
    : IQueryHandler<GetDoctorByIdQuery, Result<DoctorProfileDTO>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<Result<DoctorProfileDTO>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctorId = new EmployeeId(request.Id);
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is null)
            return Result.Fail<DoctorProfileDTO>(CrudRepositoryErrors
                .EntityNotFound(nameof(Doctor), doctorId.Value));

        return Result.Ok(_mapper.Map<DoctorProfileDTO>(doctor));
    }
}
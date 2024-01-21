using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler
    : IQueryHandler<GetDoctorByIdQuery, DoctorProfileDTO?>
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(IDoctorsRepository doctorsRepository, IMapper mapper)
    {
        _doctorsRepository = doctorsRepository;
        _mapper = mapper;
    }

    public async Task<DoctorProfileDTO?> Handle(
        GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctorId = new EmployeeId(request.Id);
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
            doctorId, cancellationToken);
        
        return _mapper.Map<DoctorProfileDTO?>(doctor);
    }
}
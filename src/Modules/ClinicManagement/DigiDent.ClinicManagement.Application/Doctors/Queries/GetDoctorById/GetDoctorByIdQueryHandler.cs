using AutoMapper;
using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetDoctorById;

public sealed class GetDoctorByIdQueryHandler
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
        GetDoctorByIdQuery query, CancellationToken cancellationToken)
    {
        EmployeeId doctorId = new(query.Id);
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
            doctorId, cancellationToken);
        
        return _mapper.Map<DoctorProfileDTO?>(doctor);
    }
}
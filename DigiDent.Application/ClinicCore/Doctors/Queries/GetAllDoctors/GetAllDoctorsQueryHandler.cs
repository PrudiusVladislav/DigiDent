using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public class GetAllDoctorsQueryHandler  
    : IQueryHandler<GetAllDoctorsQuery, IReadOnlyCollection<DoctorDTO>>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task<IReadOnlyCollection<DoctorDTO>> Handle(
        GetAllDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        return (await _doctorRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<DoctorDTO>)
            .ToList()
            .AsReadOnly();
    }
}
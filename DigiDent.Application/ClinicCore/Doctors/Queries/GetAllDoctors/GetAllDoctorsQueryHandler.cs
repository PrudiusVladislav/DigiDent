using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public class GetAllDoctorsQueryHandler  
    : IQueryHandler<GetAllDoctorsQuery, IReadOnlyCollection<DoctorDTO>>
{
    private readonly IMapper _mapper;
    private readonly IDoctorsRepository _doctorsRepository;

    public GetAllDoctorsQueryHandler(IMapper mapper, IDoctorsRepository doctorsRepository)
    {
        _mapper = mapper;
        _doctorsRepository = doctorsRepository;
    }

    public async Task<IReadOnlyCollection<DoctorDTO>> Handle(
        GetAllDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        return (await _doctorsRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<DoctorDTO>)
            .ToList()
            .AsReadOnly();
    }
}
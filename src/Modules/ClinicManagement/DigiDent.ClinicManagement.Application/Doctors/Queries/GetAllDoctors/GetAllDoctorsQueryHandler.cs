using AutoMapper;
using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetAllDoctors;

public sealed class GetAllDoctorsQueryHandler  
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
        GetAllDoctorsQuery query, CancellationToken cancellationToken)
    {
        return (await _doctorsRepository.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<DoctorDTO>)
            .ToList()
            .AsReadOnly();
    }
}
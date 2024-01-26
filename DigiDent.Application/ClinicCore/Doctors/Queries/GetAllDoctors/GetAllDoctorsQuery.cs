using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public sealed record GetAllDoctorsQuery :
    IQuery<IReadOnlyCollection<DoctorDTO>>;
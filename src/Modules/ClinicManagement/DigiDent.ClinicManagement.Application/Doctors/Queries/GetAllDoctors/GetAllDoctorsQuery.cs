using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetAllDoctors;

public sealed record GetAllDoctorsQuery :
    IQuery<IReadOnlyCollection<DoctorDTO>>;
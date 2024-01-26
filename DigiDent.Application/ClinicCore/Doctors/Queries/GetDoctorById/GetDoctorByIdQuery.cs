using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public sealed record GetDoctorByIdQuery(Guid Id) :
    IQuery<DoctorProfileDTO?>;
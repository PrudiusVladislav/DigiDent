using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetDoctorById;

public sealed record GetDoctorByIdQuery(Guid Id) :
    IQuery<DoctorProfileDTO?>;
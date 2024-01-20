using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public record GetDoctorByIdQuery(Guid Id): IQuery<DoctorProfileDTO?>;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public record GetPatientByIdQuery(Guid Id) : IQuery<PatientProfileDTO?>;
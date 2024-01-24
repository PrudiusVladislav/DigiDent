using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientProfile;

public sealed record GetPatientProfileQuery(Guid Id)
    : IQuery<PatientProfileDTO?>;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public sealed record GetPatientProfileQuery(Guid Id)
    : IQuery<PatientProfileDTO?>;
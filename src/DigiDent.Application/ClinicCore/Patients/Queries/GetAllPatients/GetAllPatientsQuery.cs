using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Patients.Queries.GetAllPatients;

public sealed record GetAllPatientsQuery
    : IQuery<IReadOnlyCollection<PatientDTO>>;
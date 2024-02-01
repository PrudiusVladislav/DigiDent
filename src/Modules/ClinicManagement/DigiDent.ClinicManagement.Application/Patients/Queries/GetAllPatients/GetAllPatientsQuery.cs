using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetAllPatients;

public sealed record GetAllPatientsQuery
    : IQuery<IReadOnlyCollection<PatientDTO>>;
using DigiDent.ClinicManagement.Domain.Shared.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Patients.ValueObjects;

public record PatientId(Guid Value) : IPersonId;
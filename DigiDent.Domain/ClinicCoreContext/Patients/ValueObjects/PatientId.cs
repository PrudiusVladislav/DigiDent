using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;

public record PatientId(Guid Value) : IPersonId<Guid>;
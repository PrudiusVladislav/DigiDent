using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Patients.ValueObjects;

public record TreatmentPlanId(Guid Value): ITypedId<Guid>;
using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Patients.ValueObjects;

public record PatientId(Guid Value): TypedId<Guid>(Value);
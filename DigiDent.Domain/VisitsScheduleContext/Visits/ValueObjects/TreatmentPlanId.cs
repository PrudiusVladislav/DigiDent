using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects;

public record TreatmentPlanId(Guid Value): TypedId<Guid>(Value);
using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects.Ids;

public record TreatmentPlanId(Guid Value): TypedId<Guid>(Value);